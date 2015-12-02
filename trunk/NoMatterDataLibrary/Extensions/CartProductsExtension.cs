using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace NoMatterDataLibrary.Extensions
{
	public static class CartProductsExtension
	{
		public static CartProduct ToDomainCartProduct(this   NoMatterDatabaseModel.CartProduct cartProduct)
		{
			if (cartProduct == null) return null;

			return new CartProduct()
			{
				CartProductId = cartProduct.CartProductId,
				CartId = cartProduct.CartId,
				Product = cartProduct.Product.ToDomainProduct()
			};
		}

		public static NoMatterDatabaseModel.CartProduct ToDatabaseCartProduct(this  CartProduct cartProduct)
		{
			return new NoMatterDatabaseModel.CartProduct
			{
				CartId = cartProduct.CartId,
				ProductId = cartProduct.Product.ProductId
			};
		}
	}
}