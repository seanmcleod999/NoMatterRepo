using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Helpers
{
	public interface IWebApiGlobalSettings
	{
		string FacebookAppId { get; }
		string FacebookAppSecret { get; }
		string ImagesBaseUrl  { get; }
		string SiteClientId { get; }
	}

	public class WebApiGlobalSettings : IWebApiGlobalSettings
	{
		public string FacebookAppId { get { return ConfigurationManager.AppSettings["FacebookAppId"]; } }

		public string FacebookAppSecret { get { return ConfigurationManager.AppSettings["FacebookAppSecret"]; } }

		public string ImagesBaseUrl { get { return ConfigurationManager.AppSettings["ApiBaseAddress"] + "images/"; } }

		public string SiteClientId { get { return ConfigurationManager.AppSettings["SiteClientId"]; } }
	}
}