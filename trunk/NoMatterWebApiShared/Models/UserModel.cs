using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class UserModel
	{
		[EmailAddress(ErrorMessage = "invalid")]
		[Required(ErrorMessage = "required")]
		[Display(Name = "Email *")]
		public string Email { get; set; }

		[Required(ErrorMessage = "required")]
		[Display(Name = "Full name *")]
		public string Fullname { get; set; }

		[Display(Name = "Phone Number")]
		public string ContactNumber { get; set; }

		[Required(ErrorMessage = "required")]
		[Display(Name = "Address *")]
		public string Address { get; set; }

		[Required(ErrorMessage = "required")]
		[Display(Name = "Suburb *")]
		public string Suburb { get; set; }

		[Required(ErrorMessage = "required")]
		[Display(Name = "City *")]
		public string City { get; set; }

		[Required(ErrorMessage = "required")]
		[Display(Name = "Province *")]
		public string Province { get; set; }

		[Required(ErrorMessage = "required")]
		[Display(Name = "Country *")]
		public string Country { get; set; }

		[Required(ErrorMessage = "required")]
		[Display(Name = "Postal Code *")]
		public string PostalCode { get; set; }

	}
}
