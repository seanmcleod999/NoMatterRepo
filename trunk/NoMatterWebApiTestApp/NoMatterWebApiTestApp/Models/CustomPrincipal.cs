using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebApplication7.Models
{
	public class CustomPrincipal : IPrincipal
	{

		private GenericIdentity _identity;
		
		private string _profileId;
		private string _token;



		public CustomPrincipal(GenericIdentity identity, string profileId, string token)
		{
			_identity = identity;			
			_profileId = profileId;
			_token = token;
		}

		public IIdentity Identity
		{
			get { return _identity; }
		}

		public string Token
		{
			get { return _token; }
		}

		public string ProfileId
		{
			get { return _profileId; }
		}

		

		public bool IsInRole(string role)
		{
			//var roles = Roles.Split(',');
			//return roles.Any(item => item == role);

			return true;
		}

	}
}