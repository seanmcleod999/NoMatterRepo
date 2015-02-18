using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoMatterWebApiModels.Models
{
	public class NewProduct
	{
		[Required(ErrorMessage = "required")]
		[StringLength(50, ErrorMessage = "Must be under 50 characters")]
		public string Title { get; set; }

		public string Description { get; set; }

		public string Size { get; set; }

		[Required(ErrorMessage = "required")]
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

		[Required(ErrorMessage = "required")]
		public string ReleaseDate { get; set; }

	}
}