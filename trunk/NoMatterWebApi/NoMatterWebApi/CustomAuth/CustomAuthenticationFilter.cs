using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using NoMatterDatabaseModel;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Models;

namespace NoMatterWebApi.CustomAuth
{
	public class CustomAuthenticationFilter : System.Web.Http.Filters.IAuthenticationFilter
	{
		public async Task AuthenticateAsync(System.Web.Http.Filters.HttpAuthenticationContext context,
		                                    System.Threading.CancellationToken cancellationToken)
		{
			HttpRequestMessage request = context.Request;

			List<ClaimsIdentity> claimsIdentities = null;

			AuthenticationHeaderValue authorization = request.Headers.Authorization;
			if (authorization != null)
			{
				if (authorization.Scheme == "Bearer")
				{
					var token = authorization.Parameter;
					if (String.IsNullOrEmpty(token))
					{
						context.ErrorResult = new AuthenticationFailureResult("Missing bearer token", request);
						return;
					}

					var userRepository = new UserRepository(new DatabaseEntities());

					var user = await userRepository.GetClientUserByTokenAsync(token);

					if (user == null)
					{
						context.ErrorResult = new AuthenticationFailureResult("Invalid token", request);
						return;
					}

					var identity = new ClaimsIdentity("Bearer", ClaimTypes.NameIdentifier, ClaimTypes.Role);
					//identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, token));
					identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserUUID.ToString()));
					identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FullName));
					identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
					identity.AddClaim(user.Client != null
						                  ? new Claim(CustomAuthentication.ClientId, user.Client.ClientUUID.ToString())
						                  : new Claim(CustomAuthentication.ClientId, ""));
					identity.AddClaim(new Claim(ClaimTypes.Hash, token));

					if (claimsIdentities == null)
						claimsIdentities = new List<ClaimsIdentity>();
					claimsIdentities.Add(identity);
				}
			}




			if (claimsIdentities != null && claimsIdentities.Any())
				context.Principal = new ClaimsPrincipal(claimsIdentities);
		}



		public Task ChallengeAsync(System.Web.Http.Filters.HttpAuthenticationChallengeContext context,
		                           System.Threading.CancellationToken cancellationToken)
		{
			//var challenge = new AuthenticationHeaderValue("Bearer");
			//context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);
			return Task.FromResult(0);
		}

		public bool AllowMultiple
		{
			get { return false; }
		}
	}
}