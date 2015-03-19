using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApiModels.ViewModels
{
	public class UserLoginVm
	{
		public List<Client> Clients { get; set; }

		public string SelectedClientId { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }
	}
}