using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiWebHelper.OtherHelpers
{
	public static class StaticGlobalSettings
	{

		public static string ImagesBaseAddress { get { return ConfigurationManager.AppSettings["ApiBaseAddress"] + "images/"; } }
		public static string NoImageImage { get { return ConfigurationManager.AppSettings["NoImageImage"] ; } }

		public static string SiteName { get { return ClientSettingsStaticCache.GetStringSetting("SiteName"); } }
		public static string SiteUrl { get { return ClientSettingsStaticCache.GetStringSetting("SiteUrl"); } }
		public static string SEOKeywords { get { return ClientSettingsStaticCache.GetStringSetting("SEOKeywords"); } }
		public static string SiteNameFriendly { get { return ClientSettingsStaticCache.GetStringSetting("SiteNameFriendly"); } }
		public static string SiteDescription { get { return ClientSettingsStaticCache.GetStringSetting("SiteDescription"); } }
		public static string SiteMetaDescription { get { return ClientSettingsStaticCache.GetStringSetting("SiteMetaDescription"); } }
		public static string SiteHomePageTitle { get { return ClientSettingsStaticCache.GetStringSetting("SiteHomePageTitle"); } }
		public static string SiteLogoPath { get { return ClientSettingsStaticCache.GetStringSetting("SiteLogoPath"); } }

		public static string FacebookAppId { get { return ConfigurationManager.AppSettings["FacebookAppId"]; } }



	}
}
