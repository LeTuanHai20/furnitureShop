using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Web
{
	public class AppSettings
	{
		private readonly IConfiguration Configuration;

		public AppSettings(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public string TempFolderUpload
		{
			get
			{
				return Configuration["TempFolderUpload"];
			}
		}

		public string DataFolderUpload
		{
			get
			{
				return Configuration["DataFolderUpload"];
			}
		}
	}
}
