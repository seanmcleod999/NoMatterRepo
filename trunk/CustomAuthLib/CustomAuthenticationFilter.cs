using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CustomAuthLib
{
    public class CustomAuthenticationFilter : System.Web.Http.Filters.IAuthenticationFilter
    {
		// refer to http://www.asp.net/web-api/overview/security/authentication-filters

        public async Task AuthenticateAsync(System.Web.Http.Filters.HttpAuthenticationContext context, System.Threading.CancellationToken cancellationToken)
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

					var ticket = CustomAuthentication.Instance.UnpackAccessToken(token);
					if (ticket == null)
					{
						context.ErrorResult = new AuthenticationFailureResult("Invalid bearer token", request);
						return;
					}

					// Validate expiration time if present
					DateTimeOffset currentUtc = new Microsoft.Owin.Infrastructure.SystemClock().UtcNow;
					if (ticket.Properties.ExpiresUtc.HasValue &&
						ticket.Properties.ExpiresUtc.Value < currentUtc)
					{
						context.ErrorResult = new AuthenticationFailureResult("Expired bearer token", request);
						return;
					}

			        if (claimsIdentities == null)
				        claimsIdentities = new List<ClaimsIdentity>();
					claimsIdentities.Add(ticket.Identity);
		        }
	        }


	        if (claimsIdentities != null && claimsIdentities.Any())
				context.Principal = new ClaimsPrincipal(claimsIdentities);
		}

	    
	    public Task ChallengeAsync(System.Web.Http.Filters.HttpAuthenticationChallengeContext context, System.Threading.CancellationToken cancellationToken)
        {
			var challenge = new AuthenticationHeaderValue("Bearer");
			context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);
			return Task.FromResult(0);
        }

        public bool AllowMultiple
        {
            get { return false; }
        }
    }
}