using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Security;
using NoMatterDatabaseModel;
using NoMatterWebApi.Controllers.V1;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Extensions;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApi.ViewModels;
using NoMatterWebApiModels.ViewModels;
using Client = NoMatterWebApiModels.Models.Client;

namespace NoMatterWebApi.Controllers.v1
{
    public class HomeController : Controller
    {
		private IGeneralHelper _generalHelper;
		private IClientRepository _clientRepository;

		public HomeController()
		{
			var databaseEntity = new DatabaseEntities();

			_generalHelper = new WebApiGeneralHelper();
			_clientRepository = new ClientRepository(databaseEntity);
		}

		public HomeController(IGeneralHelper facebookHelper, IClientRepository clientRepository)
		{
			_generalHelper = facebookHelper;
			_clientRepository = clientRepository;
		}

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

		public ActionResult Login()
		{
			ViewBag.Title = "Login Page";

			return View();
		}

		public async Task<ActionResult> LoginFacebook(string accessToken)
		{
			try
			{
				Logger.WriteGeneralInformationLog("loggin in via facebook");

				var facebookUser = await _generalHelper.VerifyFacebookTokenAsync(accessToken);

				if (facebookUser.Id != "10152508163385666") return View("LoginFailed");

				//Need to make sure its a valid facebook token.. get the facebook details back.. and check that the user is in the database (or hardcode my facebook id)

				GenerateAuthenticationCookie(facebookUser.Id);

				return View("AdminMenu");				

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

	    public ActionResult LogOff()
	    {
			Session.Clear();
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Home");
	    }

	    public ActionResult AdminMenu()
	    {
			return View();	
	    }

	    private void GenerateAuthenticationCookie(string userId)
		{

			var userData = userId;

			const bool createPersistentCookie = true;

			var authTicket = new FormsAuthenticationTicket(1, "NoMatterUser" + userId, DateTime.Now, DateTime.Now.AddMonths(3),
														   createPersistentCookie, userData);

			string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

			var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

			if (authTicket.IsPersistent)
			{
				authCookie.Expires = authTicket.Expiration;
			}

			Response.Cookies.Add(authCookie);
		}

		public async Task<ActionResult> Portfolio()
		{
			//var _clientController = new ClientController();

			//var clientControlleResult = await _clientController.GetClientsAsync();
			//var clientsResult = clientControlleResult as OkNegotiatedContentResult<List<Client>>;

			var clients = await _clientRepository.GetClientsAsync();

			var viewClientsVm = new ViewClientsVm
			{
				Clients = clients.Select(x => x.ToDomainClient()).ToList()
			};

			return View(viewClientsVm);
		}


    }
}
