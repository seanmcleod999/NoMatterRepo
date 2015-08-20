using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using NoMatterWebApiModels.Models;
using NoMatterWebApiModels.ViewModels;
using NoMatterWebApiWebHelper;
using NoMatterWebApiWebHelper.Exceptions;
using NoMatterWebApiWebHelper.Logging;
using NoMatterWebApiWebHelper.OtherHelpers;
using NoMatterWebApiWebHelper.WebApiHelpers;

namespace RedOrange.Controllers
{
    public class AccountController : WebApiController
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

		// GET: Account/SignIn
		public ActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> SignIn(SignInModel model, string redirectUrl = null)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{

				var authenticatedUser = await _userHelper.Login(_globalSettings.SiteClientId, null, model.Email, model.Password);

				if (authenticatedUser == null)
				{
					ModelState.AddModelError(string.Empty, "Authentication Failed");
					return View();
				}

				GenerateAuthenticationCookie(model.RememberMe, authenticatedUser.TokenDetails.AccessToken, authenticatedUser.Id, authenticatedUser.Fullname, authenticatedUser.UserRoles);

				return RedirectToAction("Index", "Admin");

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}


		public ActionResult Login()
		{
			var userLoginVm = new UserLoginVm();

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

				GenerateAuthenticationCookie(true, authenticatedUser.TokenDetails.AccessToken, authenticatedUser.Id, authenticatedUser.Fullname, authenticatedUser.UserRoles);

				//return View("LoginSuccess", authenticatedUser);

				return RedirectToAction("Index", "Admin");

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		private void GenerateAuthenticationCookie(bool rememberMe, string accessToken, string profileId, string firstName, string userRoles)
		{

			var userData = profileId + ";" + accessToken + ";" + userRoles;

			var authTicket = new FormsAuthenticationTicket(1, firstName, DateTime.Now, DateTime.Now.AddMonths(3),
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