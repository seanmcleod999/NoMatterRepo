using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApi.Models
{
	public class CartProduct
	{
		public string CartProductId { get; set; }

		public string CartId { get; set; }

		public Product Product { get; set; }

	}
}