using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace NoMatterWebApiModels.Models
{
	public class CustomPrincipal : IPrincipal
	{

		private GenericIdentity _identity;
		
		private string _profileId;
		private string _clientId;
		private string _token;
		private string _userRoles;

		public CustomPrincipal(GenericIdentity identity, string profileId, string clientId, string token, string userRoles)
		{
			_identity = identity;			
			_profileId = profileId;
			_clientId = clientId;
			_token = token;
			_userRoles = userRoles;
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

		public string ClientId
		{
			get { return _clientId; }
		}

		public bool IsInRole(string role)
		{
			var roles = _userRoles.Split(',');
		    return roles.Any(item => item == role);
		}

	}
}