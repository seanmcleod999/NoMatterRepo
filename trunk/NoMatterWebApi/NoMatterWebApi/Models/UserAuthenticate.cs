using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Models
{
	public class UserAuthenticate
	{
		public string Email { get; set; }

		public string Password { get; set; }

		public string FacebookToken { get; set; }
	}
}