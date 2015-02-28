using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterWebApi.Models;

namespace NoMatterWebApi.Extensions
{
	public static class CartProductsExtension
	{
		public static CartProduct ToDomainCartProduct(this   NoMatterDatabaseModel.CartProduct cartProduct)
		{
			return new CartProduct()
			{
				CartProductId = cartProduct.CartProductId.ToString(),
				CartId = cartProduct.CartId,
				Product = cartProduct.Product.ToDomainProduct()
			};
		}
	}
}