using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterDatabaseModel;
using NoMatterWebApiModels.Models;
using Order = NoMatterWebApiModels.Models.Order;
using Product = NoMatterWebApiModels.Models.Product;
using ProductType = NoMatterWebApiModels.Models.ProductType;

namespace NoMatterDataLibrary.Extensions
{
	public static class ProductTypeExtension
	{
		public static ProductType ToDomainProductType(this   NoMatterDatabaseModel.ProductType productType)
		{
			return new ProductType()
			{
				ProductTypeId = productType.ProductTypeId,
				Description = productType.Description
			};
		}
	}
}