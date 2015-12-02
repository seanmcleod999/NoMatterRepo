using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{

	public class RegisterUserModel
	{

		[Display(Name = "Email")]
		[EmailAddress(ErrorMessage = "invalid")]
		[Required(ErrorMessage = "required")]
		[StringLength(150, ErrorMessage = "too long")]
		public string Email { get; set; }

		[Display(Name = "Full name")]
		[Required(ErrorMessage = "required")]
		[StringLength(45, ErrorMessage = "too long")]
		public string FullName { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		[Required(ErrorMessage = "required")]
		public string Password { get; set; }
	}
}
