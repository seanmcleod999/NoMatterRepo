using System;
using System.Configuration;
using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Owin;

namespace CustomAuthLib
{

	public class CustomAuthentication : ICustomAuthentication
	{
		public static readonly string ClientId = "ClientId";

		public static CustomAuthentication Instance { get; private set; }

		static CustomAuthentication()
		{
			Instance = new CustomAuthentication();
		}

		private TicketDataFormat AccessTokenDataFormat { get; set; }
		private RefreshTokenDataFormat RefreshTokenDataFormat { get; set; }
		public TimeSpan AccessTokenExpiry { get; set; }

		public CustomAuthentication()
		{
			AccessTokenExpiry = TimeSpan.FromMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["AccessTokenExpiry"]));
		}

		public void Initialize(IAppBuilder app)
		{
			AccessTokenDataFormat = new TicketDataFormat(
				app.CreateDataProtector(
					typeof (CustomAuthentication).Namespace,
					"PitchInModerator_Access_Token", "v1"));

			RefreshTokenDataFormat = new RefreshTokenDataFormat(
				app.CreateDataProtector(
					typeof(CustomAuthentication).Namespace,
					"PitchInModerator_Refresh_Token", "v1"));
		}

		public CustomAccessToken CreateAccessToken(string userId, string clientId)
		{
			var identity = new ClaimsIdentity("Bearer", ClaimTypes.NameIdentifier, ClaimTypes.Role);
			identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
			identity.AddClaim(new Claim(CustomAuthentication.ClientId, clientId));
			
			var currentUtc = new Microsoft.Owin.Infrastructure.SystemClock().UtcNow;
			var properties = new AuthenticationProperties {
				IssuedUtc = currentUtc,
				ExpiresUtc = currentUtc.Add(AccessTokenExpiry)
			};
			var ticket = new AuthenticationTicket(identity, properties);

			var accesstoken = AccessTokenDataFormat.Protect(ticket);

			return new CustomAccessToken() {
				Token = accesstoken,
				Expires = properties.ExpiresUtc.Value,
			};
		}

		public class CustomAccessToken
		{
			public string Token { get; set; }
			public DateTimeOffset Expires { get; set; }
		}
		
		public string CreateRefreshToken(string profileId, string refreshTicketId)
		{
			var ticket = new RefreshTicket() {
				ProfileId = profileId,
				RefreshTicketId = refreshTicketId,
				//IssueDate = new Microsoft.Owin.Infrastructure.SystemClock().UtcNow,
			};

			return RefreshTokenDataFormat.Protect(ticket);
		}

		public AuthenticationTicket UnpackAccessToken(string accessToken)
		{
			return AccessTokenDataFormat.Unprotect(accessToken);
		}

		public RefreshTicket UnpackRefreshToken(string refreshToken)
		{
			return RefreshTokenDataFormat.Unprotect(refreshToken);
		}
	}
}