using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NoMatterDatabaseModel;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Extensions;
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
			var databaseEntity = new DatabaseEntities();

			_userRepository = new UserRepository(databaseEntity);
			_clientRepository = new ClientRepository(databaseEntity);
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
			var clientsDb = await _clientRepository.GetClientsAsync();

			var clients = clientsDb.Select(x => x.ToDomainClient()).ToList();

			clients.Add(new Client() { ClientId = "sys", ClientName = "System" });

			return clients;

		}

		[HttpPost]
		public async Task<ActionResult> UserLogin(UserLoginVm userLoginVm)
		{
			try
			{
				Guid? clientUuid = null;

				if (userLoginVm.SelectedClientId != "sys") clientUuid = new Guid(userLoginVm.SelectedClientId);

				var userDb = await _userRepository.GetClientUserByEmailAsync(clientUuid, userLoginVm.Email);

				if (userDb != null)				
				{
					//var user = userDb.toM
					var dBytes = userDb.Password;
					var enc = new UTF8Encoding();
					var length = dBytes.TakeWhile(b => b != 0).Count();
					var strDbPassword = enc.GetString(dBytes, 0, length);

					if (strDbPassword == PasswordHelper.CreatePasswordHash(userLoginVm.Password, userDb.PasswordSalt))
					{
						var userRoles = String.Join(",", userDb.UserRoles.Select(x => x.Role.RoleName));

						GenerateAuthenticationCookie(userDb.UserId.ToString(), userDb.FullName, userRoles, clientUuid.ToString(), userLoginVm.RememberMe);

						return RedirectToAction("Index", "Admin");
					}

				}

				ModelState.AddModelError(string.Empty, "Authentication Failed");

				//Get the list of clients again for the dropdown
				var clients = await GetClientsForDropDown();

				userLoginVm.Clients = clients;

				return View(userLoginVm);

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
