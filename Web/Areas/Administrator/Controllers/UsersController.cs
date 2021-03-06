﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Application.Dto.Users;
using Domain.Application.Entities;
using Domain.Application.IRepositories;
using Domain.Common.Security;
using Domain.Shop.Entities;
using Domain.Shop.IRepositories;
using Infrastructure.Common;
using Infrastructure.Database.DynamicLinq;
using Infrastructure.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace Web.Areas.Administrator.Controllers
{
	[Area("Administrator")]
	[BaseAuthorization]
	public class UsersController : BaseController
	{
		private readonly IUserRepository userRepository;
		IUserRoleRepository userRoleRepository;
		IRoleRepository roleRepository;
		UserInfoCache userInfoCache;
        private readonly IAccountRepository accountRepository;
        ILogger<UsersController> _logger;
		public UsersController(ILogger<UsersController> logger, IUserRepository userRepository,
			IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, UserInfoCache userInfoCache,
			IAccountRepository accountRepository)
		{
			_logger = logger;
			this.userRepository = userRepository;
			this.userRoleRepository = userRoleRepository;
			this.roleRepository = roleRepository;
			this.userInfoCache = userInfoCache;
            this.accountRepository = accountRepository;
        }
		public ActionResult Index()
		{
			ViewBag.roles = roleRepository.All.Select(p => new SelectListItem
			{
				Text = p.RoleName,
				Value = p.Id
			}).ToList();
			return View();
		}

		[HttpPost]
		public ActionResult GetData([FromBody] DatatableRequest request)
		{
			if (request.columns != null)
			{
				foreach (var column in request.columns)
				{
					if (column.search == null)
						continue;
					switch (column.name)
					{
						case "Roles":
							column.search.field = "Roles.Select(r=>r.RoleId)";
							column.search.Operator = FilterOperator.Contains;
							break;
						default:
							column.search.Operator = FilterOperator.Contains;
							break;
					}
				}
			}
			DatatableResult<UserGridViewModel> users = userRepository.GetUserViewModels(request);
			return Json(users);
		}
		public ActionResult Create()
		{
			ViewBag.roles = roleRepository.All.Select(p => new SelectListItem
			{
				Text = p.RoleName,
				Value = p.Id
			}).ToList();
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(UserViewModel model)
		{
			if (ModelState.IsValid && Validate(model))
			{
				try
				{
					model.Id = Guid.NewGuid().ToString();
					User user = new User();
					PropertyCopy.Copy(model, user);
					user.Password = Security.EncryptPassword(model.Password);
					userRepository.Add(user);
					if (model.Roles != null)
					{
						var addUserRole = model.Roles.Select(p => new UserRole()
						{
							Id = Guid.NewGuid().ToString(),
							RoleId = p,
							UserId = model.Id
						}).ToList();
						userRoleRepository.Add(addUserRole);
					}
					//add customer
					Customer customer = new Customer() {
						Id = model.Id,
						Email = model.Email
					};

					accountRepository.Add(customer);
					userRepository.Save(requestContext);
					accountRepository.Save(requestContext);
					
					_logger.LogInformation("Create User {0} - ID: {1}", user.UserName, user.Id);
					return RedirectToAction("Index");
				}
				catch (Exception e)
				{
					_logger.LogError(e, "Update user {0} failed", model.UserName);
					return View();
					throw;
				}
			}
			return View();
		}
		[HttpPost]
		public bool Delete(string id)
		{
			try
			{
                User user = userRepository.Single(p => p.Id == id, null, include => include.Include(q => q.UserRole));
                userRoleRepository.Delete(user.UserRole);
                userRepository.Delete(user);
                userRepository.Save(requestContext);
                //delete customer
                Customer customer = accountRepository.All.Where(c => c.Id == id).Include(c => c.ProductReviews).Include(c => c.CustomerFeedbacks).FirstOrDefault();
				accountRepository.Delete(customer.CustomerFeedbacks);
				accountRepository.Delete(customer.CustomerFeedbacks);
				accountRepository.Delete(customer);
				accountRepository.Save(requestContext);
				userInfoCache.RemoveUser(id);
				_logger.LogInformation("Delete User {0} - ID: {1}", user.UserName, user.Id);
				return true;
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Update user {0} failed", id);
				return false;
			}

		}
		public ActionResult Update(string id)
		{
			ViewBag.roles = roleRepository.All.Select(p => new SelectListItem
			{
				Text = p.RoleName,
				Value = p.Id
			}).ToList();
			return View(userRepository.GetUserViewModel(id));
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Update(UserViewModel model)
		{
			if (ModelState.IsValid && Validate(model))
			{
				try
				{
					User user = userRepository.Single(p => p.Id == model.Id, null, include => include.Include(q => q.UserRole));
					List<string> excludeProperties = new List<string>() { "Id", "Password" };
					PropertyCopy.Copy(model, user, excludeProperties);
					if (model.ChangePassword)
					{
						user.Password = Security.EncryptPassword(model.Password);
					}
					var deleteUserRole = user.UserRole.Where(p => model.Roles == null || !model.Roles.Contains(p.RoleId));
					userRoleRepository.Delete(deleteUserRole);
					if (model.Roles != null)
					{
						var addUserRole = model.Roles.Where(p => !user.UserRole.Any(q => q.RoleId == p)).Select(p => new UserRole()
						{
							Id = Guid.NewGuid().ToString(),
							RoleId = p,
							UserId = user.Id
						}).ToList();
						userRoleRepository.Add(addUserRole);
					}

					userRepository.Update(user);
					userRepository.Save(requestContext);
					_logger.LogInformation("Update User {0} - ID: {1}", user.UserName, user.Id);
				}
				catch (Exception e)
				{
					_logger.LogError(e, "Update user {0} failed", model.Id);
					return View();
				}
				return RedirectToAction("Index");
			}
			return View(model);
		}

		private bool Validate(UserViewModel user)
		{
			bool result = true;
			var dicError = userRepository.Validate(user);
			foreach (var item in dicError)
			{
				ModelState.AddModelError(item.Key, item.Value);
				result = false;
			}

			if (user.ChangePassword)
			{
				if (string.IsNullOrEmpty(user.Password))
				{
					ModelState.AddModelError("Password", "Mật khẩu không được để trống");
					result = false;
				}
				if (user.Password != user.ConfirmPassword)
				{
					ModelState.AddModelError("ConfirmPassword", "Mật khẩu không khớp nhau");
					result = false;
				}
			}
			return result;
		}
	}
}
