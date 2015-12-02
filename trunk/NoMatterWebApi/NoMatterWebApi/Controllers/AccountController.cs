using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NoMatterDataLibrary;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApiModels.ViewModels;

using Client = NoMatterWebApiModels.Models.Client;

namespace NoMatterWebApi.Controllers.v1
{
	public class AccountController : Controller
	{
		private IUserRepository _userRepository;
		private IClientRepository _clientRepository; 

		public AccountController()
		{
			_userRepository = new UserRepository();
			_clientRepository = new ClientRepository();
		}

		public AccountController(IUserRepository userRepository, IClientRepository clientRepository)
		{
			_userRepository = userRepository;
			_clientRepository = clientRepository;
		}


		public async Task<ActionResult> UserLogin()
		{
			var clients = await GetClientsForDropDown();

			var userLoginVm = new UserLoginVm
				{
					Clients = clients
				};

			return View(userLoginVm);
		}

		private async Task<List<Client>> GetClientsForDropDown()
		{
			var clients = await _clientRepository.GetClientsAsync();

			clients.Add(new Client() { ClientUuid = "sys", ClientName = "System" });

			return clients;

		}

		[HttpPost]
		public async Task<ActionResult> UserLogin(UserLoginVm userLoginVm)
		{
			try
			{
				Guid? clientUuid = null;

				if (userLoginVm.SelectedClientId != "sys") clientUuid = new Guid(userLoginVm.SelectedClientId);

				var user = await _userRepository.GetClientUserByEmailAsync(clientUuid, userLoginVm.Email);

				if (user == null || !PasswordCrypto.CheckPassword(user.Password, userLoginVm.Password))
				{
					var clients = await GetClientsForDropDown();

					userLoginVm.Clients = clients;

					ModelState.AddModelError(string.Empty, "Authentication Failed");
					return View(userLoginVm);
				}

				GenerateAuthenticationCookie(user.UserUuid, user.Fullname, user.UserRolesString, clientUuid.ToString(), userLoginVm.RememberMe);

				return RedirectToAction("Index", "Admin");					
				
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		private void GenerateAuthenticationCookie(string profileId, string fullName, string userRoles, string clientId, bool rememberMe)
		{

			var userData = profileId + ";" + fullName + ";" + clientId + ";" + userRoles;

			//const bool createPersistentCookie = true;

			var authTicket = new FormsAuthenticationTicket(1, fullName, DateTime.Now, DateTime.Now.AddMonths(3),
														   rememberMe, userData);

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
