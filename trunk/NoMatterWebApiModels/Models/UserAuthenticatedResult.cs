using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApiModels.Models
{
	public class UserAuthenticatedResult
	{
		public string Id { get; set; }

		public string Email { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public TokenDetails TokenDetails { get; set; }

		public string FacebookToken { get; set; }


	}
}