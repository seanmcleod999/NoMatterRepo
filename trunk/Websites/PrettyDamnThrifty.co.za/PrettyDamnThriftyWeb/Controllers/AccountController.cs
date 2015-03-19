using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PrettyDamnThriftyWeb.Mailers;
using PrettyDamnThriftyWeb.Providers;
using SharedLibrary;
using SharedLibrary.DatabaseModel;
using SharedLibrary.Helpers;
using SharedLibrary.Logging;
using SharedLibrary.Models;
using SharedLibrary.Services.ShoppingCartService;
using SharedLibrary.Services.UserService;
using SharedLibrary.ViewModels;

namespace PrettyDamnThriftyWeb.Controllers
{
	public class AccountController : Controller
	{
		private IUserService _userService;
		private ICurrentUser _currentUser;
		private IShoppingCartService _shoppingCartService;

		public AccountController()
		{
			_currentUser = new CurrentUser();
			_userService = new UserService();
			_shoppingCartService = new ShoppingCartService();	
		}

		public ActionResult Login(bool fromCart = false, string returnUrl = null)
		{
			var loginVm = new LoginVm
				{
					FromCart = fromCart,
					ReturnUrl = returnUrl
				};

			return View(loginVm);
		}

		[AllowAnonymous]
		[HttpPost]
		public ActionResult Login(LoginVm loginVm, string returnUrl)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var user = _userService.ValidateUser(loginVm.LoginModel.Email, loginVm.LoginModel.Password);

					GenerateAuthenticationCookie(user);

					//Need to check if there any cart items linked to the sessionid and move them to the userId
					_shoppingCartService.MoveSessionCartItemsToUser(_currentUser.CartId(), user.UserId);

					//_currentUser.CartId() = user.UserId;

					if (!string.IsNullOrEmpty(returnUrl))
					{
						return new RedirectResult(returnUrl, false);
					}

					return RedirectToAction("Index", "Home");

				}

			}
			catch (UserValidationFailedException)
			{
				ModelState.AddModelError("", "The email or password provided is incorrect.");
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				ModelState.AddModelError("", "Sorry, but there has been a technical error.");				
			}

			return View(loginVm);
		}

		public ActionResult Register(bool fromCart = false, string returnUrl = null)
		{
			var loginVm = new LoginVm
			{
				FromCart = fromCart,
				ReturnUrl = returnUrl
			};

			return View("Login", loginVm);
		}


		[HttpPost]
		public ActionResult Register(LoginVm loginVm, string returnUrl)
		{

			try
			{
				var user = _userService.CreateUser(loginVm.RegisterUserModel.Email,
													 loginVm.RegisterUserModel.FullName,
													 loginVm.RegisterUserModel.Password);

				//Success so create authentication cookie
				GenerateAuthenticationCookie(user);

				var mailer = new PDTMailer();

				//Send an email response to the user
				mailer.RegisterUser(loginVm.RegisterUserModel.Email, loginVm.RegisterUserModel.FullName).Send();

				if (!string.IsNullOrEmpty(returnUrl))
				{
					return new RedirectResult(returnUrl, false);
				}

				return View("RegistrationSuccess");

			}
			catch (UserAlreadyExistsException ex)
			{
				ModelState.AddModelError("", "The email address has already been registered.");
			}
			catch (PasswordTooShortException ex)
			{
				ModelState.AddModelError("", "The supplied password is too short.");
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				ModelState.AddModelError("", "Sorry, but there has been a technical error.");
			}

			return View("Login", loginVm);

		}

		[AllowAnonymous]
		public ActionResult Index()
		{
			return RedirectToAction("Index", "Home");
		}

		[AllowAnonymous]
		[HttpPost]
		public ActionResult FacebookLogin(string accessToken)
		{
			var user = _userService.HandleFacebookLogin(accessToken);

			GenerateAuthenticationCookie(user);

			return RedirectToAction("Index", "Home");
		}

		private void GenerateAuthenticationCookie(User user)
		{

			var userData = user.UserId + "," + user.Email;

			if (!String.IsNullOrEmpty(user.UserRoles)) userData += ";" + user.UserRoles;

			bool createPersistentCookie = true;

			var authTicket = new FormsAuthenticationTicket(1, user.Fullname, DateTime.Now, DateTime.Now.AddMonths(3),
			                                               createPersistentCookie, userData);

			string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

			var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

			if (authTicket.IsPersistent)
			{
				authCookie.Expires = authTicket.Expiration;
			}

			Response.Cookies.Add(authCookie);
		}

		//public void AuthoriseFacebook()
		//{
		//	var url = FacebookHelper.GenerateLoginUrl(Globals.FacebookAppId, "manage_pages,publish_stream");

		//	Response.Redirect(url.AbsoluteUri, false);
		//}

		//public ActionResult FacebookAuth()
		//{
		//	string code = Request.QueryString["code"];

		//	string uri = Globals.FacebookAuthRedirectPath;

		//	var fb = new FacebookClient();

		//	//Get the short lived access token
		//	dynamic short_lived_token = fb.Get("oauth/access_token", new
		//	{
		//		client_id = Globals.FacebookAppId,
		//		client_secret = Globals.FacebookSecret,
		//		redirect_uri = uri,
		//		code = code
		//	});

		//	//Save the token in the facebook api
		//	fb.AccessToken = short_lived_token.access_token;

		//	//Get a long life token if you need to
		//	dynamic resultLongLifeToken = fb.Post("oauth/access_token",
		//		new
		//		{
		//			client_id = Globals.FacebookAppId,
		//			client_secret = Globals.FacebookSecret,
		//			grant_type = "fb_exchange_token",
		//			fb_exchange_token = short_lived_token.access_token
		//		});

		//	var longLifeAccessToken = resultLongLifeToken.access_token;

		//	dynamic me = fb.Get("me");

		//	string name = me.name;

		//	//Gets the list of accounts so that you can get the page token
		//	dynamic accounts = fb.Get("me/accounts");

		//	var facebookAccounts = new List<FacebookAccount>();

		//	//Loop over the accounts looking for the ID that matches your destination ID (Fan Page ID)
		//	foreach (dynamic account in accounts.data)
		//	{

		//		var facebookAccount = new FacebookAccount
		//			{
		//				Name = account.name, 
		//				Accesstoken = account.access_token
		//			};

		//		facebookAccounts.Add(facebookAccount);

		//	}

		//	return View("FacebookAuthorised", facebookAccounts);
		//}

		//public ActionResult AuthoriseTwitter()
		//{
		//	//IOAuthCredentials credentials = new SessionStateCredentials();

		//	//MvcAuthorizer auth;

		//	//if (credentials.ConsumerKey == null || credentials.ConsumerSecret == null)
		//	//{
		//	//	credentials.ConsumerKey = Globals.TwitterKey;
		//	//	credentials.ConsumerSecret = Globals.TwitterSecret;
		//	//	credentials.AccessToken = Globals.TwitterAccessToken;
		//	//	credentials.OAuthToken = Globals.TwitterOAuthToken;
		//	//}

		//	//auth = new MvcAuthorizer
		//	//{
		//	//	Credentials = credentials,
		//	//};

		//	//auth.CompleteAuthorization(Request.Url);

		//	//if (!auth.IsAuthorized)
		//	//{
		//	//	Uri specialUri = new Uri(Globals.TwitterAuthRedirectPath);
		//	//	return auth.BeginAuthorization(specialUri);
		//	//}

		//	//return View("TwitterAuthorised");

		//	return View("TwitterAuthorised");
		//}

		public ActionResult LogOff()
		{
			Session.Clear();
			FormsAuthentication.SignOut();

			return RedirectToAction("Index", "Home");
		}

		public ActionResult YourProfile()
		{

			var user = _userService.GetUser(_currentUser.UserId());

			var editUserVm = new EditUserVm
				{
					User = user
				};

			return View(editUserVm);
		}

		[HttpPost]
		public ActionResult YourProfile(EditUserVm editUserVm)
		{
			try
			{
				_userService.UpdateUser(editUserVm.User);

				editUserVm.UpdateSuccess = true;
			
			}
			catch (EmailAlreadyExistsException)
			{
				ModelState.AddModelError("", "The email address already exists.");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Sorry, but there has been a technical error.");
				Logger.WriteGeneralError(ex);
			}

			return View(editUserVm);
		
		}

		[Authorize(Roles = "Admin")]
		public ActionResult ViewUsers()
		{
			var usersVm = new UsersVm
			{
				Users = _userService.GetUsers()
			};

			return View(usersVm);
		}

		public ActionResult UserDelete(int id)
		{
			try
			{
				_userService.DeleteUser(id);
				return RedirectToAction("ViewUsers");
				
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Sorry, but there has been a technical error.");
				Logger.WriteGeneralError(ex);

				var usersVm = new UsersVm
				{
					Users = _userService.GetUsers()
				};

				return View("ViewUsers", usersVm);
			}

			
		}

		public ActionResult ForgotPassword()
		{
			var forgotPasswordVm = new ForgotPasswordVm();

			return View(forgotPasswordVm);
		}

		[HttpPost]
		public ActionResult ForgotPassword(ForgotPasswordVm forgotPasswordVm)
		{
			//Reset the password and send a new password


			return View(forgotPasswordVm);
		}

	}
}
