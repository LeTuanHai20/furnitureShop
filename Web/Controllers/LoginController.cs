﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Application.Dto.Login;
using Domain.Application.Entities;
using Domain.Application.IRepositories;
using Domain.Application.Services;
using Domain.Common.Security;
using Domain.Shop.Entities;
using Domain.Shop.IRepositories;
using Infrastructure.Common;
using Infrastructure.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
	
	public class LoginController : Controller
	{
		AuthService authService;
        private readonly IUserRepository userRepository;
        private readonly IMailerRepository mailer;
        private readonly ConfigurationCache configuration;
        private readonly IRoleRepository roleRepository;
        private readonly IAccountRepository accountRepository;

        public LoginController(ILogger<LoginController> logger, AuthService authService, IUserRepository userRepository, IMailerRepository mailer,
			ConfigurationCache configuration ,IRoleRepository roleRepository, IAccountRepository accountRepository)
		{
			this.authService = authService;
            this.userRepository = userRepository;
            this.mailer = mailer;
            this.configuration = configuration;
            this.roleRepository = roleRepository;
            this.accountRepository = accountRepository;
        }
		[HttpGet]
		public IActionResult Index(string returnUrl)
		{
			LoginViewModel model = new LoginViewModel()
			{
				returnUrl  = returnUrl
			};
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model,string returnUrl)
		{
			if (ModelState.IsValid)
			{
				model.Password = Security.EncryptPassword(model.Password);
				var profile = authService.CheckLogin(model.Username, model.Password);
				if (profile == null)
				{
					return RedirectToAction("Index", new { error = true });
				}
				string token = SecurityManager.GenerateToken(profile.id, profile.username, Request.Headers["User-Agent"].ToString());
				ControllerContext.HttpContext.Response.Cookies.Append(SecurityManager._securityToken, token);
				var claims = new List<Claim>()
				{
					new Claim(ClaimTypes.Name, model.Username)
				};
				var claimsIdentity = new ClaimsIdentity(claims, "Login");
				
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
				var exists = false;
				foreach (var item in userRepository.GetUserViewModel(profile.id).Roles)
                {
					if(roleRepository.GetRoleViewModel(item).RoleName == "Quản trị hệ thống") 
                    {
						exists = true;
						break;
					}
                }
				
				if(returnUrl != null)
                {
					return LocalRedirect(returnUrl);
                } 
				else if (exists)
                {
					return RedirectToAction("Index", "Default", new { Area = "Administrator" });
				}
				return RedirectToAction("Index", "Home");
			}
			return View("Index");
		}
		#region Forgot password
		[HttpGet]
		public IActionResult ForgotPassword()
		{
			return View();
		}
		[HttpPost]
		public IActionResult ForgotPassword(ForgotPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = userRepository.GetUserByEmail(model.Email);
				if (user == null)
				{
					ModelState.AddModelError("", "Email not exists !");
					return View();
				}
				else
				{
					string token = SecurityManager.GenerateToken(user.Id, user.UserName, Request.Headers["User-Agent"].ToString());
					var passwordResetLink = Url.Action("ResetPassword", "Login", new { email = user.Email, token = token }, Request.Scheme);
					mailer.SendEmail(passwordResetLink, user.Email, "Reset Password", "Please click this link to comfirmation to " +
						"Reset your password: ", configuration.GetConfiguration().MailSetting);
					return View("_confirmEmail");
				}
			}
			return View();
		}
		[HttpGet]
		public IActionResult ResetPassword([FromQuery] string email, [FromQuery] string token)
		{
			if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
			{
				return RedirectToAction("Login");
			}
			else
			{
				var user = userRepository.GetUserByEmail(email);
				if (user != null)
				{
					if (SecurityManager.getUserId(token) == user.Id && (SecurityManager.getUserName(token) == user.UserName))
					{
						ResetPasswordViewModel reset = new ResetPasswordViewModel()
						{
							Email = email,
							Token = token
						};
						return View(reset);
					}

				}
			}
			return RedirectToAction("Login");
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				await userRepository.ResetPassword(model);
				return RedirectToAction("Index");
			}
			else
			{
				ModelState.AddModelError("", "Your password not allowed");
				return View();
			}
		}
		#endregion
		#region Register
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					model.Id = Guid.NewGuid().ToString();
					model.UserName = model.FullName;
					User user = new User();
					PropertyCopy.Copy(model, user);
					user.Password = Security.EncryptPassword(model.Password);
					Customer customer = new Customer()
					{
						Id = model.Id,
						Email = model.Email
					};

					accountRepository.Add(customer);
					userRepository.Add(user);
					userRepository.Save();
					accountRepository.Save();
					return RedirectToAction("Index");
				}
				catch (Exception)
				{

					throw;
				}

			}
			return View();
		}
		#endregion
		public async Task<RedirectToActionResult> Logout()
        {
			await HttpContext.SignOutAsync();
			return RedirectToAction("Index","Home");
        }
	}
}


 