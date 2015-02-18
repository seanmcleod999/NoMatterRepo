using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Models
{
	public class FacebookUser
	{
		public string Id { get; set; }

		public string First_Name { get; set; }

		public string Last_Name { get; set; }

		public string Username { get; set; }

		public string Email { get; set; }
	}
}