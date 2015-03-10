using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class Product
	{
		public string ProductId { get; set; }

		public string CategoryId { get; set; }

		[Required(ErrorMessage = "required")]
		[StringLength(50, ErrorMessage = "Must be under 50 characters")]
		public string Title { get; set; }

		public string Description { get; set; }

		public string Size { get; set; }

		[Required(ErrorMessage = "required")]
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

		public string ProductShortUrl { get; set; }

		public string AdminNotes { get; set; }

		public string Keywords { get; set; }

		[Display(Name = "Date Created")]
		public DateTime? DateCreated { get; set; }

		[Display(Name = "Date Sold")]
		public DateTime? DateSold { get; set; }

		[Required(ErrorMessage = "required")]
		[Display(Name = "Release Date")]
		public DateTime ReleaseDate { get; set; }

		public string ViewProductUrl { get; set; }

		public DiscountDetails DiscountDetails { get; set; }

		public RelatedProductDetails RelatedProductDetails { get; set; }
	}
}
