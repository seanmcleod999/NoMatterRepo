using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.ViewModels
{
	public class ContactUsVm
	{
		public string PageText { get; set; }

		[Required(ErrorMessage = "required")]
		[StringLength(50, ErrorMessage = "Max length 50 characters")]
		[Display(Name = "Your Name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "required")]
		[StringLength(50, ErrorMessage = "Max length 50 characters")]
		[Display(Name = "Your Email Address")]
		public string EmailAddress { get; set; }

		[StringLength(50, ErrorMessage = "Max length 50 characters")]
		[Display(Name = "Your Contact Number")]
		public string ContactNumber { get; set; }

		[Required(ErrorMessage = "required")]
		[Display(Name = "Your Message")]
		public string Message { get; set; }


	}
}
