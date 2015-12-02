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
		
		//General 
		public static string SiteClientId { get { return ConfigurationManager.AppSettings["SiteClientId"]; } }

		//TODO : do we need this.. surely the api can pass all these details?
		public static string ApiBaseAddress { get { return ConfigurationManager.AppSettings["ApiBaseAddress"]; } }
		public static string ImagesBaseAddress { get { return ApiBaseAddress + "images/"; } }

		public static string NoImageImage { get { return ConfigurationManager.AppSettings["NoImageImage"] ; } }
		
		//TODO: do we need then
		public static string SiteHomePageTitle { get { return ClientSettingsStaticCache.GetStringSetting("SiteHomePageTitle"); } }
		
		//Client details
		public static string ClientName { get { return ClientStaticCache.Client().ClientName; } }
		public static string SiteUrl { get { return ClientStaticCache.Client().SiteUrl; } }
		public static string ClientLogo { get { return ClientStaticCache.Client().Logo; } }
		public static string FacebookAppId { get { return ConfigurationManager.AppSettings["FacebookPageAppId"]; } }
		public static string FacebookAppSecret { get { return ConfigurationManager.AppSettings["FacebookPageAppSecret"]; } }

		//SEO
		public static string SEOKeywords { get { return ClientSettingsStaticCache.GetStringSetting("SEOKeywords"); } }
		public static string SiteDescription { get { return ClientSettingsStaticCache.GetStringSetting("SiteDescription"); } }
		public static string SiteMetaDescription { get { return ClientSettingsStaticCache.GetStringSetting("SiteMetaDescription"); } }
			
		//Google maps
		public static string MapLongitude { get { return ClientSettingsStaticCache.GetStringSetting("MapLongitude"); } }
		public static string MapLatitude { get { return ClientSettingsStaticCache.GetStringSetting("MapLatitude"); } }

		//Social Media
		public static string FacebookPageUrl { get { return ClientSettingsStaticCache.GetStringSetting("FacebookPageUrl"); } }
		public static string TwitterUrl { get { return ClientSettingsStaticCache.GetStringSetting("TwitterUrl"); } }
		public static string InstagramUrl { get { return ClientSettingsStaticCache.GetStringSetting("InstagramUrl"); } }
		public static string PintrestUrl { get { return ClientSettingsStaticCache.GetStringSetting("PintrestUrl"); } }

		//Email addresses
		public static string EmailAddressSales { get { return ClientSettingsStaticCache.GetStringSetting("EmailAddressSales"); } }
		public static string EmailAddressInfo { get { return ClientSettingsStaticCache.GetStringSetting("EmailAddressInfo"); } }

		//Bank Details
		public static string BankName { get { return ClientSettingsStaticCache.GetStringSetting("BankName"); } }
		public static string AccountName { get { return ClientSettingsStaticCache.GetStringSetting("AccountName"); } }
		public static string AccountNumber { get { return ClientSettingsStaticCache.GetStringSetting("AccountNumber"); } }
		public static string BranchName { get { return ClientSettingsStaticCache.GetStringSetting("BranchName"); } }
		public static string BranchNumber { get { return ClientSettingsStaticCache.GetStringSetting("BranchNumber"); } }

	}
}
