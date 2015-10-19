using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace RestyledLiving
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

			ClientSectionsStaticCache.LoadClientSectionsCache();
	        ClientSettingsStaticCache.LoadClientSettingsCache();
			ClientStaticCache.LoadClientCache();
			//GlobalSettingsStaticCache.LoadGlobalSettingsCache();
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

					var profileUuid = userDataArray[0];
					var clientId = userDataArray[1];
					var token = userDataArray[2];
					var userRoles = userDataArray[3];

					var principal = new CustomPrincipal(new GenericIdentity(auth.Name), token, userRoles);

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
