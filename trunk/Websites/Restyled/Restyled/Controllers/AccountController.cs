using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NoMatterWebApiModels.Models;
using NoMatterWebApiModels.ViewModels;
using NoMatterWebApiWebHelper.OtherHelpers;
using NoMatterWebApiWebHelper.WebApiHelpers;
using RestyledLiving.Logging;

namespace RestyledLiving.Controllers
{
    public class AccountController : Controller
    {
		private IUserHelper _userHelper;
		private IGlobalSettings _globalSettings;

		public AccountController()
		{
			_userHelper = new UserHelper();
			_globalSettings = new GlobalSettings();
		}

		public AccountController(IUserHelper userHelper, IGlobalSettings globalSettings)
		{
			_userHelper = userHelper;
			_globalSettings = globalSettings;
		}

		// GET: Account/Register
		public ActionResult Register()
		{
			return View();
		}

		// POST: Account/Register
		[HttpPost]
		public async Task<ActionResult> Register(RegisterModel model)
		{
			try
			{

				if (!ModelState.IsValid)
				{
					return View(model);
				}

				await _userHelper.RegisterUser(_globalSettings.SiteClientId, model);

				return View("Registered");
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}


		public ActionResult Login()
		{
			var userLoginVm = new LoginVm();

			return View(userLoginVm);
		}

		[HttpPost]
		public async Task<ActionResult> Login(UserLoginVm userLoginVm)
		{
			try
			{

				var authenticatedUser = await _userHelper.Login(_globalSettings.SiteClientId, null, userLoginVm.Email, userLoginVm.Password);

				if (authenticatedUser == null)
				{
					ModelState.AddModelError(string.Empty, "Authentication Failed");
					return View();
				}

				GenerateAuthenticationCookie(authenticatedUser.TokenDetails.AccessToken, authenticatedUser.Id, authenticatedUser.Fullname, authenticatedUser.UserRoles, authenticatedUser.ClientId);

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

			const bool createPersistentCookie = false;

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