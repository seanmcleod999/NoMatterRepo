using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Owin;

namespace CustomAuthLib
{

	public interface ICustomAuthentication
	{
		TimeSpan AccessTokenExpiry { get; set; }
		CustomAuthentication.CustomAccessToken CreateAccessToken(string userId, string clientId);
		string CreateRefreshToken(string userId, string refreshTicketId);
		AuthenticationTicket UnpackAccessToken(string accessToken);
		RefreshTicket UnpackRefreshToken(string refreshToken);
	}
}
