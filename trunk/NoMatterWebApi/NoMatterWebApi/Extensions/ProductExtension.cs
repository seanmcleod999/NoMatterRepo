using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterDatabaseModel;
using NoMatterWebApi.Enums;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApi.Extensions
{
	public static class ProductExtension
	{
		public static NoMatterWebApiModels.Models.Product ToDomainProduct(this NoMatterDatabaseModel.Product product)
		{
			return new NoMatterWebApiModels.Models.Product()
			{
				ProductId = product.ProductUUID.ToString(),
				CategoryId = product.Category.CategoryUUID.ToString(),
				Title = product.Title,
				Description = product.Description,
				Size = product.Size,
				Price = product.Price,
				Reserved = product.Reserved,			
				Hidden = product.Hidden,
				Sold = product.Sold,
				DateSold = product.DateSold,
				DateCreated = product.DateCreated,
				ReleaseDate = product.ReleaseDate,
				Picture1 = product.Picture1,
				Picture2 = product.Picture2,
				Picture3 = product.Picture3,
				Picture4 = product.Picture4,
				Picture5 = product.Picture5,
				PictureOther = product.PictureOther,
				FacebookPostId = product.FacebookPostId,
				TwitterPostId = product.TwitterPostId,
				ProductShortUrl = product.ProductShortUrl,
				AdminNotes=product.AdminNotes,
				Keywords = string.Join(",", product.ProductKeywords.Select(x => x.Keyword)), 
				DiscountDetails = GetDiscountDetails(product.DiscountProducts, product.Price),
				//RelatedProductDetails = GetRelatedProductDetails(product.ProductKeywords.ToList(), product.ProductUUID.ToString())
			};
		}

		

		internal static DiscountDetails GetDiscountDetails(IEnumerable<DiscountProduct> discountProducts, decimal shopItemPrice)
		{
			var discountDetails = new DiscountDetails { Discounted = false, DiscountedPrice = shopItemPrice };

			if (discountProducts == null) return discountDetails;

			//Check for any discount for this product
			var fromDate = DateTime.Now.Date;
			var toDate = DateTime.Now.Date;

			var activeDiscounts = discountProducts.Where(
				x => x.Discount.StartDate <= fromDate && x.Discount.EndDate >= toDate &&
					 (x.Discount.DiscountTypeId == 2 || x.Discount.DiscountTypeId == 3))
										   .OrderByDescending(x => x.DiscountId)
										   .Take(1)
										   .SingleOrDefault();

			if (activeDiscounts == null) return discountDetails;

			discountDetails.Discounted = true;
			discountDetails.DiscountTypeId = activeDiscounts.Discount.DiscountTypeId;
			discountDetails.DiscountAmount = activeDiscounts.Discount.DiscountAmount;

			switch (activeDiscounts.Discount.DiscountTypeId)
			{
				case (byte)DiscountTypeEnum.FixedAmount:
					discountDetails.DiscountedPrice = shopItemPrice - activeDiscounts.Discount.DiscountAmount;
					break;
				case (byte)DiscountTypeEnum.Percentage:
					discountDetails.DiscountedPrice = decimal.Ceiling(shopItemPrice - ((shopItemPrice / 100) * activeDiscounts.Discount.DiscountAmount));
					break;
			}

			return discountDetails;
		}

		//private static RelatedProductDetails GetRelatedProductDetails(IEnumerable<ProductKeyword> keywords, string productId)
		//{
		//	var relatedProductDetails = new RelatedProductDetails();

		//	if (keywords == null) return relatedProductDetails;

		//	using (var mainDb = new DatabaseModelEntities())
		//	{
		//		var keywordsList = keywords.Select(x => x.keyword).ToList();

		//		//Create a Employer list for the dropdown restricted to the employers that the user can see
		//		var allRelatedShopItemsIds = (from e in mainDb.shopitemkeywords
		//									  where (keywordsList.Contains(e.keyword) && e.ShopItemId != shopItemId)
		//									  select e.ShopItemId).Distinct().ToList();

		//		var shuffledAndRestrictedIds = allRelatedShopItemsIds.OrderBy(a => Guid.NewGuid()).ToList();

		//		var relatedShopItemsDb = (from e in mainDb.shopitems
		//								  where
		//									  (shuffledAndRestrictedIds.Contains(e.ShopItemId) && !e.Hidden && !e.Reserved && !e.Sold)
		//								  select e).Take(_globalSettings.RelatedItemsCount).ToList();

		//		relatedProductDetails.RelatedShopItemIds = relatedShopItemsDb.Select(x => x.ShopItemId).ToList();

		//		var relatedShopItemList = relatedShopItemsDb.ToList().Select(shopItemDb => new ShopItemDisplay(shopItemDb)).ToList();

		//		relatedProductDetails.RelatedShopItems = relatedShopItemList;

		//		return relatedProductDetails;

		//	}
		//}
	}
}