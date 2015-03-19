using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
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
        }

		protected void Application_Error()
		{
			var exception = Server.GetLastError();
			if (exception != null)
			{
				Logger.WriteGeneralError(exception);
			}
		}

		protected void Application_OnPostAuthenticateRequest(Object sender, EventArgs e)
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
					var clientId = userDataArray[1];
					var token = userDataArray[2];
					var userRoles = userDataArray[3];


					//Get the roles
					//var roles = "";
					//if (userDataArray.Count() > 1) roles = userDataArray[1];

					var principal = new CustomPrincipal(new GenericIdentity(auth.Name), profileId, clientId, token, userRoles);

					Context.User = Thread.CurrentPrincipal = principal;
				}
			}
			catch (Exception ex)
			{
				//Logger.WriteGeneralError(ex);
			}
		}
    }
}
