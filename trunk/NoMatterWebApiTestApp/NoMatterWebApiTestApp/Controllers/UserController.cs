using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NoMatterWebApiWebHelper;
using NoMatterWebApiWebHelper.OtherHelpers;
using WebApplication7.Logging;
using WebApplication7.ViewModels;

namespace WebApplication7.Controllers
{
    public class UserController : Controller
    {
		private IUserHelper _userHelper;
		private IGlobalSettings _globalSettings;
		
		public UserController()
		{
			_userHelper = new UserHelper();
			_globalSettings = new GlobalSettings();
		}

		public UserController(IUserHelper userHelper, IGlobalSettings globalSettings)
		{
			_userHelper = userHelper;
			_globalSettings = globalSettings;
		}

		// GET: User
		public ActionResult FacebookRegisterQuery()
		{
			return View();
		}

        // GET: User
        public ActionResult FacebookRegister()
        {
            return View();
        }

		// GET: User
		public ActionResult FacebookLogin()
		{
			return View();
		}


	    public async Task<ActionResult> LoginFacebook(string facebookToken)
	    {
			var userAuthenticatedResult = await _userHelper.Login(_globalSettings.DefaultClientId, facebookToken, null, null);

			GenerateAuthenticationCookie(userAuthenticatedResult.TokenDetails.Token, userAuthenticatedResult.Id, userAuthenticatedResult.FirstName);

			userAuthenticatedResult.FacebookToken = facebookToken;

			return View("LoginSuccess", userAuthenticatedResult);
	    }

		// GET: User
		public ActionResult UserLogin()
		{
			var userLoginVm = new UserLoginVm();

			return View(userLoginVm);
		}

		[HttpPost]
		public async Task<ActionResult> UserLogin(UserLoginVm userLoginVm)
		{

			var userAuthenticatedResult = await _userHelper.Login(_globalSettings.DefaultClientId, null, userLoginVm.Username, userLoginVm.Password);

			GenerateAuthenticationCookie(userAuthenticatedResult.TokenDetails.Token, userAuthenticatedResult.Id, userAuthenticatedResult.FirstName);

			return View("LoginSuccess", userAuthenticatedResult);
		}


	    //public async Task<ActionResult> RegisterFacebook(string accessToken)
        //{
        //    //Get the user details from the facebook, ie facebookid, firstname, lastname
        //    //Show the user a screen to check and update thier details

        //    FacebookUser user = FacebookHelper.VerifyFacebookToken(accessToken);

        //    return View(user);

        //}

        //[HttpPost]
        //public async Task<ActionResult> RegisterFacebookFinish(FacebookUser facebookUser)
        //{

        //    try
        //    {		

        //        if (ModelState.IsValid)
        //        {

        //            using (var client = new HttpClient())
        //            {
        //                client.BaseAddress = new Uri(GlobalSettings.BaseAddress);
        //                client.DefaultRequestHeaders.Accept.Clear();
        //                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //                var credentials = new Credentials
        //                    {
        //                        Type = "Facebook",
        //                        Token = facebookUser.AccessToken
        //                    };

        //                var registerModel = new RegisterModel()
        //                    {
        //                        credentials = credentials, 
        //                        last_name = facebookUser.Last_Name, 
        //                        first_name = 
        //                        facebookUser.First_Name,
        //                        email = facebookUser.Email, 
        //                        avatar_url = facebookUser.AvatarUrl
        //                    };

        //                var response = await client.PostAsJsonAsync("api/v1/users", registerModel);

        //                if (!response.IsSuccessStatusCode)
        //                {
        //                    if (response.StatusCode == HttpStatusCode.BadRequest)
        //                    {
        //                        var resultContent = response.Content.ReadAsStringAsync();

        //                        var temp = resultContent.Result;

        //                        return View("BadRequest", resultContent);
        //                    }

        //                    return View("BadRequest");
        //                }
        //                else
        //                {
        //                    var loginResponse = await response.Content.ReadAsAsync<LoginResponse>();

        //                    GenerateAuthenticationCookie(loginResponse.token_details.access_token, loginResponse.id, loginResponse.first_name);

        //                    loginResponse.FacebookToken = facebookUser.AccessToken;

        //                    return View(loginResponse);

        //                    //return RedirectToAction("Index", "Home");
        //                }
        //            }
        //        }

        //        return RedirectToAction("Index", "Home");

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteGeneralError(ex);
        //        throw;
        //    }
        //}

        //public async Task<ActionResult> LoginFacebook(string accessToken)
        //{
        //    try
        //    {
				

        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri(GlobalSettings.BaseAddress);
        //            client.DefaultRequestHeaders.Accept.Clear();
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //            var credentials = new Credentials
        //            {
        //                Type = "Facebook",
        //                Token = accessToken
        //            };

        //            var loginModel = new LoginModel() { credentials = credentials, device_type = "Android", device_id = "12345"};

        //            var response = await client.PostAsJsonAsync("api/v1/authenticate", loginModel);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                var loginResponse = await response.Content.ReadAsAsync<LoginResponse>();
					
        //                GenerateAuthenticationCookie(loginResponse.token_details.access_token, loginResponse.id, loginResponse.first_name);

        //                loginResponse.FacebookToken = accessToken;

        //                return View("LoginSuccess", loginResponse);
        //            }
        //            else
        //            {
        //                return HandleUnsuccessfulResponse(response);
        //            }
					
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteGeneralError(ex);
        //        throw;
        //    }
        //}

		



		private void GenerateAuthenticationCookie(string accessToken, string profileId, string firstName)
		{

			var userData = profileId + "," + accessToken;

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

		//[Authorize]
		//public async Task<ActionResult> GetUsers()
		//{
		//	using (var client = new HttpClient())
		//	{
		//		client.BaseAddress = new Uri(GlobalSettings.BaseAddress);
		//		client.DefaultRequestHeaders.Accept.Clear();
		//		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

		//		var token = ((CustomPrincipal)HttpContext.User).Token;
		//		client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

		//		var userId = ((CustomPrincipal)HttpContext.User).ProfileId;

		//		var response = await client.GetAsync("api/v1/users/");

		//		if (response.IsSuccessStatusCode)
		//		{
		//			var getUsersResponse = await response.Content.ReadAsAsync<List<Profile>>();

		//			return View(getUsersResponse);
		//		}
		//		else
		//		{
		//			return HandleUnsuccessfulResponse(response);
		//		}
		//	}
		//}

		//[Authorize]
		//public async Task<ActionResult> DeleteUser(string id)
		//{
		//	using (var client = new HttpClient())
		//	{
		//		client.BaseAddress = new Uri(GlobalSettings.BaseAddress);
		//		client.DefaultRequestHeaders.Accept.Clear();
		//		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

		//		var token = ((CustomPrincipal)HttpContext.User).Token;
		//		client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

		//		var userId = ((CustomPrincipal)HttpContext.User).ProfileId;

		//		var response = await client.DeleteAsync("api/v1/users/" + id);

		//		if (response.IsSuccessStatusCode)
		//		{
		//			//var getUsersResponse = await response.Content.ReadAsAsync<List<Profile>>();

		//			//return View(getUsersResponse);

		//			return RedirectToAction("GetUsers");
		//		}
		//		else
		//		{
		//			return HandleUnsuccessfulResponse(response);
		//		} 
		//	}
		//}

        private ActionResult HandleUnsuccessfulResponse(HttpResponseMessage response)
        {
            var resultContent = response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var temp = resultContent.Result;

				Logger.WriteGeneralInformationLog("Bad Request: " + resultContent.Result);

                return View("BadRequest", resultContent);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("Unauthorized");
            }
            else
            {
				return View("BadRequest", resultContent);
            }
        }

    }
}