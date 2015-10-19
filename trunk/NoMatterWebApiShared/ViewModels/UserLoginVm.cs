using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApiModels.ViewModels
{
	public class UserLoginVm
	{
		public List<Client> Clients { get; set; }

		[Display(Name = "Client")]
		public string SelectedClientId { get; set; }

		[DataType(DataType.EmailAddress, ErrorMessage = "not valid")]
		[Required(ErrorMessage = "required")]
		[StringLength(50, ErrorMessage = "Must be under 50 characters")]
		public string Email { get; set; }

		[Required(ErrorMessage = "required")]
		[StringLength(50, ErrorMessage = "Must be under 50 characters")]
		public string Password { get; set; }

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }
	}
}