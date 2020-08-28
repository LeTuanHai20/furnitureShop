using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Application.Dto.Menus;
using Domain.Application.Entities;
using Domain.Application.IRepositories;
using Infrastructure.Common;
using Infrastructure.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Web.Areas.Administrator.Controllers
{
	[Area("Administrator")]
	[BaseAuthorization]
	public class MenusController : BaseController
	{
		private ILogger logger;
		private IMenuRepository menuRepository;
		private IRoleRepository roleRepository;
		private IMenuRoleRepository menuRoleRepository;
		private UserInfoCache userInfoCache;
		public MenusController(ILogger<MenusController> logger, IMenuRepository menuRepository, IRoleRepository roleRepository, IMenuRoleRepository menuRoleRepository, UserInfoCache userInfoCache)
		{
			this.logger = logger;
			this.menuRepository = menuRepository;
			this.roleRepository = roleRepository;
			this.menuRoleRepository = menuRoleRepository;
			this.userInfoCache = userInfoCache;
		}
		public IActionResult Index()
		{
			IEnumerable<MenuViewModel> menus = menuRepository.GetMenuViewModels();
			menus = MenuViewModel.GetTreeMenuViewModels(menus);
			return View(menus);
		}

		private void SetComboData(string HierarchyCode = null)
		{
			var menuQuery = menuRepository.All;
			if (!string.IsNullOrEmpty(HierarchyCode))
			{
				menuQuery = menuQuery.Where(p => !p.HierarchyCode.StartsWith(HierarchyCode)); 
			}
			ViewBag.menus = menuQuery.OrderBy(p => p.HierarchyCode).Select(p => new SelectListItem
			{
				Text = p.Name,
				Value = p.HierarchyCode
			}).ToList();
			ViewBag.roles = roleRepository.All.Select(p => new SelectListItem
			{
				Text = p.RoleName,
				Value = p.Id
			}).ToList();

		}

		public ActionResult Create()
		{
			SetComboData();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(MenuViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					model.Id = Guid.NewGuid().ToString();
					Menu menu = new Menu();
					PropertyCopy.Copy(model, menu);
					menu.HierarchyCode = menuRepository.GenerateHierarchyCode(model.ParentHierarchyCode);
					if (model.Roles != null)
					{
						menu.MenuRoles = new List<MenuRole>();
						foreach (string role in model.Roles)
						{
							menu.MenuRoles.Add(new MenuRole()
							{
								Id = Guid.NewGuid().ToString(),
								MenuId = menu.Id,
								RoleId = role
							});
						} 
					}
					menuRepository.Add(menu);
					roleRepository.Save(requestContext);
					userInfoCache.RemoveMenuCaches();
					logger.LogInformation("Create Role {0}", model.Name);
					return RedirectToAction("Index");
				}
				catch (Exception e)
				{
					logger.LogError(e, "Update role {0} failed", model.Name);
					SetComboData();
					return View();
					throw;
				}
			}
			SetComboData();
			return View();
		}

		[HttpPost]
		public bool Delete(string id)
		{
			try
			{
				Menu menu = menuRepository.Single(p => p.Id == id, null, include => include.Include(q => q.MenuRoles));
				if (menuRepository.CanDeleteMenu(menu.HierarchyCode))
				{
					menuRepository.Delete(menu);
					menuRoleRepository.Delete(menu.MenuRoles);
					menuRepository.Save(requestContext);
					userInfoCache.RemoveMenuCaches();
					logger.LogInformation("Delete Role {0}", menu.Name);
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception e)
			{
				logger.LogError(e, "Delete role {0} failed", id);
				return false;
			}

		}

		public ActionResult Update(string id)
		{
			var model = menuRepository.GetMenuViewModel(id);
			SetComboData(model.HierarchyCode);
			model.ParentHierarchyCode = model.HierarchyCode.Substring(0, model.HierarchyCode.Length - Domain.Common.Consts.Infrastructure.HierarchyCodeLength);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Update(MenuViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					Menu menu = menuRepository.Single(p => p.Id == model.Id, null, include => include.Include(q => q.MenuRoles));
					List<string> excludeProperty = new List<string>() { "Id", "HierarchyCode" };
					PropertyCopy.Copy(model, menu, excludeProperty);
					string CurrentHierarchyParent = menu.HierarchyCode.Substring(0, menu.HierarchyCode.Length - Domain.Common.Consts.Infrastructure.HierarchyCodeLength);
					if (string.IsNullOrEmpty(model.ParentHierarchyCode) ? !string.IsNullOrEmpty(CurrentHierarchyParent) : model.ParentHierarchyCode != CurrentHierarchyParent)
					{
						var HierarchyCode = menuRepository.GenerateHierarchyCode(model.ParentHierarchyCode);
						var ChildMenus = menuRepository.GetChildMenus(menu.HierarchyCode);
						foreach (Menu item in ChildMenus)
						{
							item.HierarchyCode = model.ParentHierarchyCode + item.HierarchyCode.Substring(HierarchyCode.Length);
						}
						menu.HierarchyCode = HierarchyCode;
					}

					var deleteUserRole = menu.MenuRoles.Where(p => model.Roles == null || !model.Roles.Contains(p.RoleId));
					menuRoleRepository.Delete(deleteUserRole);
					if (model.Roles != null)
					{
						var addUserRole = model.Roles.Where(p => !menu.MenuRoles.Any(q => q.RoleId == p)).Select(p => new MenuRole()
						{
							Id = Guid.NewGuid().ToString(),
							MenuId = menu.Id,
							RoleId = p
						}).ToList();
						menuRoleRepository.Add(addUserRole);
					}
					menuRepository.Update(menu);
					menuRepository.Save(requestContext);
					userInfoCache.RemoveMenuCaches();
					logger.LogInformation("Update Menu {0}", model.Name);
				}
				catch (Exception e)
				{
					logger.LogError(e, "Update Menu {0} failed", model.Name);
					SetComboData(model.HierarchyCode);
					return View();
				}
				return RedirectToAction("Index");
			}
			SetComboData(model.HierarchyCode);
			return View();
		}
	}
}
