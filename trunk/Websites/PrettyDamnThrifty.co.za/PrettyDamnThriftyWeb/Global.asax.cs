using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Ninject;
using Ninject.Web.Common;
using PrettyDamnThriftyWeb.Providers;
using SharedLibrary.Helpers;
using SharedLibrary.Logging;
using SharedLibrary.Models;

namespace PrettyDamnThriftyWeb
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : NinjectHttpApplication
	{
		protected override void OnApplicationStarted()
		{
			AreaRegistration.RegisterAllAreas();

			//WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			DbSettingsStaticCache.LoadDbGlobalSettingsCache();
			SliderStaticCache.LoadSliderStaticCache();
			CategoryStaticCache.LoadCategoryStaticCache();
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

					//Get the userId
					var userDetails = userDataArray[0].Split(',');
					var userId = Convert.ToInt32(userDetails[0]);
					var emailAddress = userDetails[1];

					//Get the roles
					var roles = "";
					if (userDataArray.Count() > 1) roles = userDataArray[1];

					var principal = new CustomPrincipal(new GenericIdentity(auth.Name), userId, emailAddress, roles);

					Context.User = Thread.CurrentPrincipal = principal;
				}
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
			}
		}

		protected override IKernel CreateKernel()
		{
			var kernel = new StandardKernel();

			//kernel.Load(new RepositoryModule());
			//kernel.Bind<IBlogRepository>().To<BlogRepository>();
			kernel.Bind<IAuthProvider>().To<AuthProvider>();

			return kernel;
		}
	}
}