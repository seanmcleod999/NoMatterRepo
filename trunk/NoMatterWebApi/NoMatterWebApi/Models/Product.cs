using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApi.Models
{
	public class Product
	{
		public string ProductId { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string Size { get; set; }

		public decimal Price { get; set; }

		public bool Reserved { get; set; }

		public bool Sold { get; set; }

		public bool Hidden { get; set; }

		public string Picture1 { get; set; }

		public string Picture2 { get; set; }

		public string Picture3 { get; set; }

		public string Picture4 { get; set; }

		public string Picture5 { get; set; }

		public string PictureOther { get; set; }

		public string FacebookPostId { get; set; }

		public string TwitterPostId { get; set; }

		public DateTime? DateSold { get; set; }

		public DateTime? ReleaseDate { get; set; }

		public DiscountDetails DiscountDetails { get; set; }
	}
}