using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using NoMatterWebApiWebHelper.OtherHelpers;
using WebApplication7.Models;

namespace WebApplication7
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
			GlobalSettingsStaticCache.LoadGlobalSettingsCache();
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

					var userDataArray = userData.Split(',');

					//Get the userId
					//var userDetails = userDataArray[0].Split(',');
					
					var profileUuid = userDataArray[0];
					var token = userDataArray[1];


					//Get the roles
					var roles = "";
					if (userDataArray.Count() > 1) roles = userDataArray[1];

					var principal = new CustomPrincipal(new GenericIdentity(auth.Name), profileUuid, token);

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
