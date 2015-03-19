
using System.ComponentModel.DataAnnotations;
using System.Web;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApiModels.ViewModels
{
	public class AddEditProductVm
	{
		public Section Section { get; set; }

		public Category Category { get; set; }

		public Product Product { get; set; }

		[Display(Name = "Picture 1")]
		public HttpPostedFileBase Picture1 { get; set; }

		[Display(Name = "Picture 2")]
		public HttpPostedFileBase Picture2 { get; set; }

		[Display(Name = "Picture 3")]
		public HttpPostedFileBase Picture3 { get; set; }

		[Display(Name = "Picture 4")]
		public HttpPostedFileBase Picture4 { get; set; }

		[Display(Name = "Picture 5")]
		public HttpPostedFileBase Picture5 { get; set; }

		[Display(Name = "Other Picture")]
		public HttpPostedFileBase PictureOther { get; set; }
	}
}