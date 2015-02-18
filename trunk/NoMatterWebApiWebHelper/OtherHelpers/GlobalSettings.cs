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
		string BaseAddress { get; }
		string FacebookAppId { get; }
		string FacebookAppSecret { get; }

		//string SiteName { get; }
		//string SiteUrl { get; }
		//string SEOKeywords { get; }
		//string SiteNameFriendly { get; }
		//string SiteDescription { get; }
		//string SiteMetaDescription  { get; }
		//string SiteHomePageTitle { get; }
		//string SiteLogoPath { get; }

		//string ShopItemPath { get; }
		//string OgShopImagesPath { get; }
		string ShopImagesPath { get; }
		string SliderImagesPath { get; }
		string CategoryImagesPath { get; }

		int ShopItemImageMaxSize { get; }
		int CategoryImageMaxSize { get; }
		int SliderImageMaxSize { get; }

		//int LatestItemsCount { get; }
		//int RelatedItemsCount { get; }
		//BankDetails BankDetails { get; }

		//string EmailAddressSales { get; }
		//string EmailAddressInfo { get; }

		//string PayfastPaymentMode { get; }
		//string PayfastMerchantId { get; }
		//string PayfastMerchantKey { get; }
		//string PayfastReturnUrl { get; }
		//string PayfastCancelUrl { get; }
		//string PayfastNotifyUrl { get; }
		
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
		int PictureThumbnailSize { get; }

		string DefaultClientId { get; }

	}

	public class GlobalSettings : IGlobalSettings
	{
		public string BaseAddress { get { return ConfigurationManager.AppSettings["BaseAddress"]; } }
		public string FacebookAppId { get { return ConfigurationManager.AppSettings["FacebookAppId"]; } }
		public string FacebookAppSecret { get { return ConfigurationManager.AppSettings["FacebookAppSecret"]; } }

		public string ShopImagesPath { get { return ConfigurationManager.AppSettings["ShopImagesPath"]; } }
		public string SliderImagesPath { get { return ConfigurationManager.AppSettings["SliderImagesPath"]; } }
		public string CategoryImagesPath { get { return ConfigurationManager.AppSettings["CategoryImagesPath"]; } }

		public int ShopItemImageMaxSize { get { return Convert.ToInt32(ConfigurationManager.AppSettings["ShopItemImageMaxSize"]); } }
		public int CategoryImageMaxSize { get { return Convert.ToInt32(ConfigurationManager.AppSettings["CategoryImageMaxSize"]); } }
		public int SliderImageMaxSize { get { return Convert.ToInt32(ConfigurationManager.AppSettings["SliderImageMaxSize"]); } }

		public int PictureThumbnailSize { get { return Convert.ToInt32(ConfigurationManager.AppSettings["PictureThumbnailSize"]); } }

		public string DefaultClientId { get { return ConfigurationManager.AppSettings["DefaultClientId"]; } }
	}
}
