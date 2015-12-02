using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterDatabaseModel;
using NoMatterWebApiModels.Models;
using Order = NoMatterWebApiModels.Models.Order;
using Product = NoMatterWebApiModels.Models.Product;
using Supplier = NoMatterWebApiModels.Models.Supplier;

namespace NoMatterDataLibrary.Extensions
{
	public static class SupplierExtension
	{
		public static Supplier ToDomainSupplier(this   NoMatterDatabaseModel.Supplier supplier)
		{
			return new Supplier()
			{
				SupplierId = supplier.SupplierId,
				Name = supplier.Name,
				ContactNumber = supplier.ContactNumber,
				Email = supplier.Email			
			};
		}
	}
}