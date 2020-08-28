using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Application.Dto.Login;
using Domain.Application.Entities;
using Domain.Application.IRepositories;
using Domain.Application.Services;
using Domain.Common.Security;
using Domain.Shop.IRepositories;
using Infrastructure.Common;
using Infrastructure.Web;
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

        public LoginController(ILogger<LoginController> logger, AuthService authService, IUserRepository userRepository, IMailerRepository mailer, ConfigurationCache configuration )
		{
			this.authService = authService;
            this.userRepository = userRepository;
            this.mailer = mailer;
            this.configuration = configuration;
        }
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginViewModel model)
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
				return RedirectToAction("Index", "Default", new { Area = "Administrator" });
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
					userRepository.Add(user);
					userRepository.Save();
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
	}
}


 