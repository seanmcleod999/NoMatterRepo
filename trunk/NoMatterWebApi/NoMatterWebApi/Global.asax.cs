using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using CustomAuthLib;
using NoMatterWebApi.Logging;
using NoMatterWebApi.Models;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

			AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }

		protected void Application_Error()
		{
			var exception = Server.GetLastError();
			if (exception != null)
			{
				Logger.WriteGeneralError(exception);
			}
		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{
			try
			{
				HttpCookie formsCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

				if (formsCookie != null)
				{
					FormsAuthenticationTicket auth = FormsAuthentication.Decrypt(formsCookie.Value);


					//Get all the stored user data
					string userData = auth.UserData;

					var userDataArray = userData.Split(';');

					var profileId = userDataArray[0];
					var fullname = userDataArray[1];
					var clientId = userDataArray[2];
					//var token = userDataArray[2];
					var userRoles = userDataArray[3];

					var roles = userRoles.Split(',');

					////Get the roles
					//var roles = "";
					//if (userDataArray.Count() > 1) roles = userDataArray[1];

					//var principal = new CustomPrincipal(new GenericIdentity(auth.Name), profileId, clientId, token, userRoles);

					//Context.User = Thread.CurrentPrincipal = principal;

					IList<Claim> claimCollection = new List<Claim>
					{
						new Claim(ClaimTypes.NameIdentifier, profileId),
						new Claim(ClaimTypes.Name, fullname),
						//, new Claim(ClaimTypes.Email, user.Email)
						new Claim(CustomAuthentication.ClientId, clientId ?? "")

					};

					var claimsIdentity = new ClaimsIdentity(claimCollection, "NoMatterApi", ClaimTypes.Name, ClaimTypes.Role);

					foreach (var role in roles)
					{
						claimsIdentity.AddClaim(
							new Claim(ClaimTypes.Role, role));
					}

					var principal = new ClaimsPrincipal(claimsIdentity);

					//Thread.CurrentPrincipal = principal;

					Context.User = Thread.CurrentPrincipal = principal;

				}
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				//throw ex;
			}
		}
    }
}
