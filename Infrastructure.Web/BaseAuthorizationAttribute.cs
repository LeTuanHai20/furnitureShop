using Domain.Application.Dto.Menus;
using Domain.Application.IRepositories;
using Domain.Application.Services;
using Domain.Common;
using Domain.Common.Security;
using Infrastructure.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Web
{
	public class BaseAuthorizationAttribute : Attribute, IAuthorizationFilter
	{
		private UserInfo userInfo;
		private List<CacheMenu> cacheMenu;
		private bool Authorize(AuthorizationFilterContext actionContext)
		{
			try
			{
				var request = actionContext.HttpContext.Request;
				string token = request.Cookies[SecurityManager._securityToken];
				//Kiểm tra token có hợp lệ hay không
				bool tokenValid = SecurityManager.IsTokenValid(token, request.Headers["User-Agent"]);
				if (!tokenValid)
					return tokenValid;
				UserInfoCache userInfoCache = (UserInfoCache)actionContext.HttpContext.RequestServices.GetService(typeof(UserInfoCache));
				string UserId = SecurityManager.getUserId(token);
				userInfo = userInfoCache.GetUser(UserId);
				if (userInfo == null)
				{
					IUserRepository userRepository = (IUserRepository)actionContext.HttpContext.RequestServices.GetService(typeof(IUserRepository));
					userInfo = (from obj in userRepository.All
								where obj.Id == UserId
								select new UserInfo()
								{
									Id = obj.Id,
									DayOfBirth = obj.DayOfBirth,
									Email = obj.Email,
									FullName = obj.FullName,
									Gender = obj.Gender,
									PhoneNo = obj.PhoneNo,
									UserName = obj.UserName,
									RoleInfo = obj.UserRole.Select(p => new RoleInfo()
									{
										Id = p.RoleId,
										RoleCode = p.Role.RoleCode,
										RoleName = p.Role.RoleName
									})
								}).FirstOrDefault();
					if (userInfo != null)
						userInfoCache.SetUser(userInfo);
				}

				cacheMenu = userInfoCache.GetMenuCaches();
				if (cacheMenu == null)
				{
					IMenuRepository menuRepository = (IMenuRepository)actionContext.HttpContext.RequestServices.GetService(typeof(IMenuRepository));
					cacheMenu = menuRepository.All.Select(p => new CacheMenu
					{
						Order = p.Order,
						Name = p.Name,
						DisplayName = p.DisplayName,
						HierarchyCode = p.HierarchyCode,
						Icon = p.Icon,
						Controller = p.Controller,
						Roles = p.MenuRoles.Select(r => r.RoleId).ToList(),
					}).OrderBy(p => p.HierarchyCode).ToList();
					userInfoCache.UpdateMenuCaches(cacheMenu);
				}

				////Kiểm tra thông tin user trong cache, Nếu không tồn tại thì return false
				return userInfo != null;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool ControllerAuthorize(AuthorizationFilterContext context)
		{
			string CurrentController = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName;
			if (CurrentController == "Default")
				return true;
			var menu = cacheMenu.Where(p => p.Controller == CurrentController).FirstOrDefault();
			if (menu != null)
			{
				return userInfo.RoleInfo.Select(p => p.Id).Intersect(menu.Roles).Any();
			}
			return false;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (!Authorize(context))
			{
				//throw new HttpResponseMessage(HttpStatusCode.Unauthorized);
				context.Result = new RedirectToActionResult("Index", "Login", new { area = "" });
				return;
			}
			if (!ControllerAuthorize(context))
			{
				context.Result = new RedirectToActionResult("Index", "Default", new { area = "Administrator" });
				return;
			}	
		}
	}
}
