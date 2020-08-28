using Domain.Application.Services;
using Domain.Common;
using Domain.Common.Security;
using Infrastructure.Common;
using Infrastructure.Database.Models;
using Infrastructure.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Web
{
    public class BaseController : Controller, IActionFilter
    {
        private UserInfo _user;
        public UserInfo CurrentUser
        {
            get { return _user != null ? _user : GetUser(); }
            set { _user = value; }
        }
        protected PageHeaderViewModel PageHeader;
        protected RequestContext requestContext;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewData["UserInfo"] = CurrentUser;
            UserInfoCache userInfoCache = (UserInfoCache)HttpContext.RequestServices.GetService(typeof(UserInfoCache));
            ViewData["CacheMenu"] = userInfoCache.GetMenuCaches();
            ViewData["CurrentController"] = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName;
            SetBreadcrumb(context);
            requestContext = new RequestContext()
            {
                RequestTime = DateTime.Now,
                UserId = CurrentUser.Id
            };
        }

        private void SetBreadcrumb(ActionExecutingContext context)
		{
            UserInfoCache userInfoCache = (UserInfoCache)context.HttpContext.RequestServices.GetService(typeof(UserInfoCache));
            string CurrentController = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName;
            var cacheMenu = userInfoCache.GetMenuCaches();
            if (cacheMenu == null)
                return;
            var currentMenu = cacheMenu.Where(prop => prop.Controller == CurrentController).FirstOrDefault();
            if (currentMenu == null)
			{
                //this.PageHeader = new PageHeaderViewModel() {};
                return;
            }                
            this.PageHeader = new PageHeaderViewModel()
            {
                Title = currentMenu.DisplayName,
                Path = new List<PageHeaderPath>()
            };
            if (string.IsNullOrEmpty(currentMenu.HierarchyCode))
                return;
            var Paths = cacheMenu.Where(p => currentMenu.HierarchyCode.StartsWith(p.HierarchyCode)).OrderBy(p => p.HierarchyCode);
			foreach (var item in Paths)
			{
                PageHeader.Path.Add(new PageHeaderPath()
                {
                    Name = item.Name,
                    Controller = item.Controller
                });
			}

            ViewBag.Title = Paths.Select(p => p.DisplayName).LastOrDefault();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            ViewBag.PageHeaderModel = PageHeader;
        }

        private UserInfo GetUser()
        {
            try
            {
                string token = ControllerContext.HttpContext.Request.Cookies[SecurityManager._securityToken].ToString();
                ICacheBase cacheBase = (ICacheBase)HttpContext.RequestServices.GetService(typeof(ICacheBase));
                UserInfoCache userInfoCache = new UserInfoCache(cacheBase);
                //Kiểm tra thông tin user trong cache, Nếu không tồn tại thì return false
                _user = userInfoCache.GetUser(SecurityManager.getUserId(token));
                return _user;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
