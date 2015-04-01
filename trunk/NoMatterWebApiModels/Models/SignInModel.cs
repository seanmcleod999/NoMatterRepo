using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class SignInModel
	{
		[EmailAddress(ErrorMessage = "invalid"), Required(ErrorMessage = "required")]
		[Display(Name = "Email address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "required")]
		public string Password { get; set; }

		
		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }
	}
}
