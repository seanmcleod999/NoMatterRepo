using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApiModels.Models;
using NoMatterWebApiModels.ViewModels;
using NoMatterWebApiWebHelper.OtherHelpers;
using NoMatterWebApiWebHelper.WebApiHelpers;

namespace NoMatterWebApi.Controllers.v1
{
	public class AccountController : Controller
	{
		private IUserHelper _userHelper;
		private IClientHelper _clientHelper;
		private IGlobalSettings _globalSettings;

		public AccountController()
		{
			_userHelper = new UserHelper();
			_clientHelper= new ClientHelper();
			_globalSettings = new GlobalSettings();
		}

		public AccountController(IUserHelper userHelper, IClientHelper clientHelper, IGlobalSettings globalSettings)
		{
			_userHelper = userHelper;
			_clientHelper = clientHelper;
			_globalSettings = globalSettings;
		}


		public async Task<ActionResult> UserLogin()
		{

			var clients = await _clientHelper.GetClientsAsync();
			clients.Add(new Client() {ClientId = "sys", ClientName = "System"});

			var userLoginVm = new UserLoginVm
				{
					Clients = clients
				};

			return View(userLoginVm);
		}

		[HttpPost]
		public async Task<ActionResult> UserLogin(UserLoginVm userLoginVm)
		{
			try
			{
				var user = await _userHelper.Login(userLoginVm.SelectedClientId, null, userLoginVm.Username, userLoginVm.Password);

				if (user == null)
				{
					ModelState.AddModelError(string.Empty, "Authentication Failed");

					//Get the list of clients again for the dropdown
					var clients = await _clientHelper.GetClientsAsync();
					clients.Add(new Client() { ClientId = "sys", ClientName = "System" });

					userLoginVm.Clients = clients;

					return View();
				}

				GenerateAuthenticationCookie(user.TokenDetails.Token, user.Id, user.Fullname, user.UserRoles, user.ClientId);

				//return View("LoginSuccess", user);
				return RedirectToAction("Index", "Admin");
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		private void GenerateAuthenticationCookie(string accessToken, string profileId, string firstName, string userRoles, string clientId)
		{

			var userData = profileId + ";" + clientId + ";" + accessToken + ";" + userRoles;

			const bool createPersistentCookie = true;

			var authTicket = new FormsAuthenticationTicket(1, firstName, DateTime.Now, DateTime.Now.AddMonths(3),
														   createPersistentCookie, userData);

			string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

			var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

			if (authTicket.IsPersistent)
			{
				authCookie.Expires = authTicket.Expiration;
			}

			Response.Cookies.Add(authCookie);
		}

		//
		// POST: /Account/LogOff
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff()
		{
			Session.Clear();
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Home");
		}

	}
}
