using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Helpers
{
	public static class StaticGlobalSettings
	{
		public static string ApiBaseAddress { get { return ConfigurationManager.AppSettings["ApiBaseAddress"]; } }
		//public static string SiteClientId { get { return ConfigurationManager.AppSettings["SiteClientId"]; } }

		public static string ImagesBaseAddress { get { return ApiBaseAddress + "images/"; } }
		public static string NoImageImage { get { return ConfigurationManager.AppSettings["NoImageImage"]; } }

	}
}