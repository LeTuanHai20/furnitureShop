using Domain.Application.Dto.Configuration;
using Domain.Application.IRepositories;
using Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Web
{
	public class ConfigurationCache
	{
		ISiteSettingRepository siteSettingRepository;
		IMailSettingRepository mailSettingRepository;
		ISSOSettingRepository SSOSettingRepository;
		ICacheBase cache;
		private string _ConfigurationKey = "_PageConfiguration";
		public ConfigurationCache(
			ISiteSettingRepository siteSettingRepository,
			IMailSettingRepository mailSettingRepository,
			ISSOSettingRepository SSOSettingRepository,
			ICacheBase _cache
			)
		{
			this.siteSettingRepository = siteSettingRepository;
			this.mailSettingRepository = mailSettingRepository;
			this.SSOSettingRepository = SSOSettingRepository;
			cache = _cache;
		}
		public ConfigurationViewModel GetConfiguration()
		{
			return cache.Get<ConfigurationViewModel>(_ConfigurationKey);
		}

		public void SetConfiguration()
		{
			var siteSetting = siteSettingRepository.Single();
			var mailSetting = mailSettingRepository.Single();
			var SSOSetting = SSOSettingRepository.Single();

			var configuration = new ConfigurationViewModel()
			{
				MailSetting = new MailSettingViewModel(),
				SSOSetting = new SSOSettingViewModel(),
				SiteSetting = new SiteSettingViewModel()
			};

			if (siteSetting != null)
			{
				PropertyCopy.Copy(siteSetting, configuration.SiteSetting);
			}
			if (mailSetting != null)
			{
				PropertyCopy.Copy(mailSetting, configuration.MailSetting);
			}
			if (SSOSetting != null)
			{
				PropertyCopy.Copy(SSOSetting, configuration.SSOSetting);
			}
			SetConfiguration(configuration);
		}

		public void SetConfiguration(ConfigurationViewModel configuration)
		{
			cache.Set<ConfigurationViewModel>(_ConfigurationKey, configuration);
		}
	}
}
