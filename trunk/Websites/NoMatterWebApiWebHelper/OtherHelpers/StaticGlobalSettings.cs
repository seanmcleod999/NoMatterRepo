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
		public static string ApiBaseAddress { get { return ConfigurationManager.AppSettings["ApiBaseAddress"]; } }
		public static string SiteClientId { get { return ConfigurationManager.AppSettings["SiteClientId"]; } }

		public static string ImagesBaseAddress { get { return ApiBaseAddress + "images/"; } }
		public static string NoImageImage { get { return ConfigurationManager.AppSettings["NoImageImage"] ; } }
		public static string FacebookAppId { get { return ConfigurationManager.AppSettings["FacebookAppId"]; } }

		public static string SiteName { get { return ClientSettingsStaticCache.GetStringSetting("SiteName"); } }
		public static string SiteUrl { get { return ClientSettingsStaticCache.GetStringSetting("SiteUrl"); } }
		public static string SEOKeywords { get { return ClientSettingsStaticCache.GetStringSetting("SEOKeywords"); } }
		public static string SiteNameFriendly { get { return ClientSettingsStaticCache.GetStringSetting("SiteNameFriendly"); } }
		public static string SiteDescription { get { return ClientSettingsStaticCache.GetStringSetting("SiteDescription"); } }
		public static string SiteMetaDescription { get { return ClientSettingsStaticCache.GetStringSetting("SiteMetaDescription"); } }
		public static string SiteHomePageTitle { get { return ClientSettingsStaticCache.GetStringSetting("SiteHomePageTitle"); } }
		public static string SiteLogoPath { get { return ClientSettingsStaticCache.GetStringSetting("SiteLogoPath"); } }	

		public static string MapLongitude { get { return ClientSettingsStaticCache.GetStringSetting("MapLongitude"); } }
		public static string MapLatitude { get { return ClientSettingsStaticCache.GetStringSetting("MapLatitude"); } }

		public static string FacebookPageUrl { get { return ClientSettingsStaticCache.GetStringSetting("FacebookPageUrl"); } }
		public static string TwitterUrl { get { return ClientSettingsStaticCache.GetStringSetting("TwitterUrl"); } }
		public static string InstagramUrl { get { return ClientSettingsStaticCache.GetStringSetting("InstagramUrl"); } }
		public static string PintrestUrl { get { return ClientSettingsStaticCache.GetStringSetting("PintrestUrl"); } }

		public static string EmailAddressSales { get { return ClientSettingsStaticCache.GetStringSetting("EmailAddressSales"); } }
		public static string EmailAddressInfo { get { return ClientSettingsStaticCache.GetStringSetting("EmailAddressInfo"); } }

		public static string BankName { get { return ClientSettingsStaticCache.GetStringSetting("BankName"); } }
		public static string AccountName { get { return ClientSettingsStaticCache.GetStringSetting("AccountName"); } }
		public static string AccountNumber { get { return ClientSettingsStaticCache.GetStringSetting("AccountNumber"); } }
		public static string BranchName { get { return ClientSettingsStaticCache.GetStringSetting("BranchName"); } }
		public static string BranchNumber { get { return ClientSettingsStaticCache.GetStringSetting("BranchNumber"); } }

	}
}
