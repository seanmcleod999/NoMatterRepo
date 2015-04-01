using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApi.Helpers
{
	public class ProductHelper
	{

		public static NoMatterDatabaseModel.Product GenerateProductDbModel(Product product, int categoryId, IGeneralHelper generalHelper)
		{
			var productUuid = Guid.NewGuid();
	
			var productDb = new NoMatterDatabaseModel.Product
			{
				ProductUUID = productUuid,
				CategoryId = categoryId,
				Title = product.Title,
				Description = product.Description,
				Size = product.Size,
				Price = product.Price,
				Reserved = product.Reserved,
				Hidden = product.Hidden,
				AdminNotes = product.AdminNotes,
				Picture1 = product.Picture1,
				Picture2 = product.Picture2,
				Picture3 = product.Picture3,
				Picture4 = product.Picture4,
				Picture5 = product.Picture5,
				PictureOther = product.PictureOther,
				ReleaseDate = Convert.ToDateTime(product.ReleaseDate),

			};

			//Handle the keywords
			if (product.Keywords != null)
			{
				var keywords = product.Keywords.Split(',');

				foreach (var productKeyword in keywords.Select(keyword => new NoMatterDatabaseModel.ProductKeyword
				{
					Product = productDb,
					Keyword = keyword.Trim().ToLower()
				}))
				{
					productDb.ProductKeywords.Add(productKeyword);
				}
			}

			//Generate the short url
			productDb.ProductShortUrl = generalHelper.MakeGoogleShortUrl(product.ViewProductUrl + productUuid);

			return productDb;
		}

		public static NoMatterDatabaseModel.Product UpdateProductDbModel(NoMatterDatabaseModel.Product productDb, Product product)
		{
			//productDb.CategoryId = model.CategoryId,
			productDb.Title = product.Title;
			productDb.Description = product.Description;
			productDb.Size = product.Size;
			productDb.Price = product.Price;
			productDb.Reserved = product.Reserved;
			productDb.Hidden = product.Hidden;
			productDb.AdminNotes = product.AdminNotes;
			productDb.Picture1 = product.Picture1;
			productDb.Picture2 = product.Picture2;
			productDb.Picture3 = product.Picture3;
			productDb.Picture4 = product.Picture4;
			productDb.Picture5 = product.Picture5;
			productDb.PictureOther = product.PictureOther;
			productDb.ReleaseDate = Convert.ToDateTime(product.ReleaseDate);

			productDb.Sold = product.DateSold != null;
			productDb.DateSold = product.DateSold;

			//Remove all previous keyword
			//foreach (var productKeyword in productDb.ProductKeywords)
			//{
			//	productDb.ProductKeywords.Remove(productKeyword);
			//}

			////Add all the keywords if there are any
			//if (!string.IsNullOrEmpty(model.Keywords))
			//{
			//	var keywords = model.Keywords.Split(',');

			//	foreach (var keyword in keywords)
			//	{
			//		var shopitemkeyword = productDb.ProductKeywords.Create();

			//		shopitemkeyword.ShopItemId = shopItem.ShopItemId;
			//		shopitemkeyword.keyword = keyword.Trim().ToLower();

			//		mainDb.shopitemkeywords.Add(shopitemkeyword);
			//	}
			//}

			return productDb;
		}

		public static void DeleteProductPictures(NoMatterDatabaseModel.Product product, IGeneralHelper generalHelper)
		{
			var imagesPath = System.Web.Hosting.HostingEnvironment.MapPath("~\\images") + "/";

			if (!string.IsNullOrEmpty(product.Picture1)) generalHelper.DeleteImage(imagesPath + product.Picture1);
			if (!string.IsNullOrEmpty(product.Picture2)) generalHelper.DeleteImage(imagesPath + product.Picture2);
			if (!string.IsNullOrEmpty(product.Picture3)) generalHelper.DeleteImage(imagesPath + product.Picture3);
			if (!string.IsNullOrEmpty(product.Picture4)) generalHelper.DeleteImage(imagesPath + product.Picture4);
			if (!string.IsNullOrEmpty(product.Picture5)) generalHelper.DeleteImage(imagesPath + product.Picture5);
			if (!string.IsNullOrEmpty(product.PictureOther)) generalHelper.DeleteImage(imagesPath + product.PictureOther);
		}
	}
}