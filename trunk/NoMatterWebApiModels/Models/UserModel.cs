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
		[Required(ErrorMessage = "required")]
		public string Email { get; set; }

		[Required(ErrorMessage = "required")]
		[Display(Name = "Full name")]
		public string Fullname { get; set; }

		[Display(Name = "Phone Number")]
		public string ContactNumber { get; set; }

		[Required(ErrorMessage = "required")]
		public string Address { get; set; }

		[Required(ErrorMessage = "required")]
		public string Suburb { get; set; }

		[Required(ErrorMessage = "required")]
		public string City { get; set; }

		[Required(ErrorMessage = "required")]
		public string Province { get; set; }

		[Required(ErrorMessage = "required")]
		public string Country { get; set; }

		[Required(ErrorMessage = "required")]
		[Display(Name = "Postal Code")]
		public string PostalCode { get; set; }

	}
}
