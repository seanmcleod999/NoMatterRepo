using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Models
{
	public class NewUser
	{
		

		public string Email { get; set; }

		public string Password { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		//public string FacebookUserId { get; set; }

		public string FacebookToken { get; set; }


	}
}