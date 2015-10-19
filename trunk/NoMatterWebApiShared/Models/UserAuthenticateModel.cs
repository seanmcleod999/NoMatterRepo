using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApiModels.Models
{
	public class UserAuthenticateModel
	{
		public string Email { get; set; }

		public string Password { get; set; }

		public string FacebookToken { get; set; }
	}
}