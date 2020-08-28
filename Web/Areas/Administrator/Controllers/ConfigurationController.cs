using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Web;
using Microsoft.AspNetCore.Mvc;
using Domain.Application.Dto.Configuration;
using Domain.Application.IRepositories;
using Infrastructure.Common;

namespace Web.Areas.Administrator.Controllers
{
	[Area("Administrator")]
	[BaseAuthorization]
	public class ConfigurationController : BaseController
	{
		ISiteSettingRepository siteSettingRepository;
		IMailSettingRepository mailSettingRepository;
		ISSOSettingRepository SSOSettingRepository;
		ConfigurationCache configuration;
		public ConfigurationController(
			ISiteSettingRepository siteSettingRepository,
			IMailSettingRepository mailSettingRepository,
			ISSOSettingRepository SSOSettingRepository,
			ConfigurationCache configuration
			)
		{
			this.siteSettingRepository = siteSettingRepository;
			this.mailSettingRepository = mailSettingRepository;
			this.SSOSettingRepository = SSOSettingRepository;
			this.configuration = configuration;
		}
		public IActionResult Index()
		{
			var model = configuration.GetConfiguration();
			if (model == null)
			{
				configuration.SetConfiguration();
				model = configuration.GetConfiguration();
			}	

			return View(model);
		}

		[HttpPost]
		public IActionResult Update(SiteSettingViewModel siteSetting, MailSettingViewModel mailSetting, SSOSettingViewModel SSOSetting)
		{
			var CurrentSiteSetting = siteSettingRepository.Single();
			var CurrentMailSetting = mailSettingRepository.Single();
			var CurrentSSOSetting = SSOSettingRepository.Single();
			if (CurrentSiteSetting == null)
			{
				CurrentSiteSetting = new Domain.Application.Entities.SiteSetting()
				{
					Id = Guid.NewGuid().ToString()
				};
				siteSettingRepository.Add(CurrentSiteSetting);
			}
			else
				siteSettingRepository.Update(CurrentSiteSetting);
			if (CurrentMailSetting == null)
			{
				CurrentMailSetting = new Domain.Application.Entities.MailSetting()
				{
					Id = Guid.NewGuid().ToString()
				};
				mailSettingRepository.Add(CurrentMailSetting);
			}
			else
				mailSettingRepository.Update(CurrentMailSetting);
			if (CurrentSSOSetting == null)
			{
				CurrentSSOSetting = new Domain.Application.Entities.SSOSetting()
				{
					Id = Guid.NewGuid().ToString()
				};
				SSOSettingRepository.Add(CurrentSSOSetting);
			}
			else
				SSOSettingRepository.Update(CurrentSSOSetting);
			PropertyCopy.Copy(siteSetting, CurrentSiteSetting);
			PropertyCopy.Copy(mailSetting, CurrentMailSetting);
			PropertyCopy.Copy(SSOSetting, CurrentSSOSetting);
			siteSettingRepository.Save(requestContext);
			configuration.SetConfiguration();

			return RedirectToAction("Index");
		}
	}
}
