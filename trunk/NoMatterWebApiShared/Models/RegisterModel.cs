using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class RegisterModel
	{
		[Required, DataType(DataType.EmailAddress), MaxLength(256), Display(Name = "Email address")]
		public string Email { get; set; }

		[Required, MaxLength(100), Display(Name = "Full name")]
		public string FullName { get; set; }

		[Required, MaxLength(50), MinLength(6)]
		public string Password { get; set; }

		[Required, Compare("Password"), Display(Name = "Password (again)")]
		public string ConfirmPassword { get; set; }
	}
}
