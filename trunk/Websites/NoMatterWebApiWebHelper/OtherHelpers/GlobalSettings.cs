using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiWebHelper.OtherHelpers
{
	public interface IGlobalSettings
	{
		string ApiBaseAddress { get; }
		string FacebookAppId { get; }
		string FacebookAppSecret { get; }

		string SiteName { get; }
		string SiteUrl { get; }

		string ShopItemPath { get; }

		//int LatestItemsCount { get; }
		//int RelatedItemsCount { get; }
		//BankDetails BankDetails { get; }

		//string EmailAddressSales { get; }
		//string EmailAddressInfo { get; }

		string PayfastPaymentMode { get; }
		string PayfastMerchantId { get; }
		string PayfastMerchantKey { get; }
		string PayfastReturnUrl { get; }
		string PayfastCancelUrl { get; }
		string PayfastNotifyUrl { get; }
		
		//string PintrestUrl { get; }
		//string InstagramUrl { get; }
		//string GooglePlusUrl { get; }

		//string FacebookPageUrl { get; }
		//string FacebookAppId { get; }
		//string FacebookSecret { get; }
		//string FacebookPageName { get; }
		//string FacebookPageAccessToken { get; }
		//bool FacebookPostingEnabled { get; }
		//string FacebookItemPostMessage { get; }

		//string TwitterUrl { get; }
		//string TwitterKey { get; }
		//string TwitterSecret { get; }
		//string TwitterAccessToken { get; }
		//string TwitterOAuthToken { get; }
		//bool TwitterPostingEnabled { get; }
		//string TwitterItemPostMessage { get; }
		//string TwitterItemCustomerPostMessage { get; }
		//string TwitterSiteCustomerPostMessage { get; }

		//int ShippingAmount { get; }
		//int PictureThumbnailSize { get; }

		string SiteClientId { get; }

		string DefaultSectionName { get; }

	}

	public class GlobalSettings : IGlobalSettings
	{

		public string ApiBaseAddress { get { return ConfigurationManager.AppSettings["ApiBaseAddress"]; } }
		public string FacebookAppId { get { return ConfigurationManager.AppSettings["FacebookAppId"]; } }
		public string FacebookAppSecret { get { return ConfigurationManager.AppSettings["FacebookAppSecret"]; } }

		public string ShopItemPath { get { return SiteUrl + ConfigurationManager.AppSettings["ShopItemPath"]; } }

		public string SiteClientId { get { return ConfigurationManager.AppSettings["SiteClientId"]; } }
		public string DefaultSectionName { get { return ConfigurationManager.AppSettings["DefaultSectionName"]; } }

		//public string ClientName { get { return StaticGlobalSettings.ClientName; } }
		//public string SiteUrl { get { return StaticGlobalSettings.SiteUrl; } }

		public string SiteName { get { return StaticGlobalSettings.ClientName; } }
		public string SiteUrl { get { return StaticGlobalSettings.SiteUrl; } }

		public string PayfastPaymentMode { get { return ConfigurationManager.AppSettings["PayfastPaymentMode"]; } }
		public string PayfastMerchantId { get { return ConfigurationManager.AppSettings["PayfastMerchantId"]; } }
		public string PayfastMerchantKey { get { return ConfigurationManager.AppSettings["PayfastMerchantKey"]; } }
		public string PayfastReturnUrl { get { return SiteUrl + ConfigurationManager.AppSettings["PayfastReturnUrl"]; } }
		public string PayfastCancelUrl { get { return SiteUrl + ConfigurationManager.AppSettings["PayfastCancelUrl"]; } }

		public string PayfastNotifyUrl { get { return SiteUrl + ConfigurationManager.AppSettings["PayfastNotifyUrl"]; } }



		
	}
}
