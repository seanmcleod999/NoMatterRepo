using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Models
{
	public class NewProduct
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public string Size { get; set; }

		public decimal Price { get; set; }

		public string Keywords { get; set; }

		public string AdminNotes { get; set; }

		public bool Hidden { get; set; }

		public bool Reserved { get; set; }

		public string Picture1 { get; set; }

		public string Picture2 { get; set; }

		public string Picture3 { get; set; }

		public string Picture4 { get; set; }

		public string Picture5 { get; set; }

		public string PictureOther { get; set; }

		public string ReleaseDate { get; set; }

	}
}