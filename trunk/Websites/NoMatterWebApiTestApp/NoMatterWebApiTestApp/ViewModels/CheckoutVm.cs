using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoMatterWebApiModels.Models;

namespace WebApplication7.ViewModels
{
	public class CheckoutVm
	{
		//[Required(ErrorMessage = "required")]
		//public string Email { get; set; }

		//[Display(Name = "Phone Number")]
		//public string PhoneNumber { get; set; }

		//[Display(Name = "Full Name")]
		//[Required(ErrorMessage = "required")]
		//public string FullName { get; set; }

		//[Required(ErrorMessage = "required")]
		//public string Country { get; set; }

		//[Required(ErrorMessage = "required")]
		//public string Address { get; set; }

		//[Required(ErrorMessage = "required")]
		//public string Suburb { get; set; }

		public List<SelectListItem> Provinces { get; set; }

		//[Required(ErrorMessage = "required")]
		//public string City { get; set; }

		//[Required(ErrorMessage = "required")]
		//public string Province { get; set; }

		//[Display(Name = "Postal Code")]
		//[Required(ErrorMessage = "required")]
		//public string PostalCode { get; set; }

		public UserModel User { get; set; }

	}
}