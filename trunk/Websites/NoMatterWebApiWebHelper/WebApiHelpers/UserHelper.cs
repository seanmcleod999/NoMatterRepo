using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.Exceptions;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace NoMatterWebApiWebHelper.WebApiHelpers
{
	public interface IUserHelper
	{
		Task RegisterUser(string clientId, RegisterModel registerModel);
		Task<SignInResult> SignInUser(string clientId, SignInModel signInModel);
		Task<UserAuthenticatedResult> Login(string clientId, string facebookToken, string email, string password);
		Task<string> CreateOrUpdateUser(string clientId, UserModel userModel);
	}

	public class UserHelper : IUserHelper
	{
		private IGlobalSettings _globalSettings;
		
		public UserHelper()
		{
			_globalSettings = new GlobalSettings();
		}

		public UserHelper(IGlobalSettings globalSettings)
		{
			_globalSettings = globalSettings;
		}

		public async Task<UserAuthenticatedResult> Login(string clientId, string facebookToken, string email, string password)
		{
			
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var userAuthenticateModel = new UserAuthenticateModel() { FacebookToken = facebookToken, Email = email, Password = password };

				var response = await client.PostAsJsonAsync(string.Format("api/v1/clients/{0}/users/authenticate", clientId), userAuthenticateModel);

				if (!response.IsSuccessStatusCode)
				{
					var resultCode = GeneralHelper.ExtractWebApiFailedResultCode(response);

					if (resultCode == 10) return null;
					
					GeneralHelper.HandleWebApiFailedResult(response);
				}

				var userAuthenticatedResult = await response.Content.ReadAsAsync<UserAuthenticatedResult>();

				return userAuthenticatedResult;				

			}		
		}

		public async Task RegisterUser(string clientId, RegisterModel registerModel)
		{
			await WebApiService.Instance.PostAsync(string.Format("/api/v1/clients/{0}/users/register", clientId), registerModel);						
		}

		public async Task<SignInResult> SignInUser(string clientId, SignInModel signInModel)
		{
			var result = await WebApiService.Instance.AuthenticateAsync<SignInResult>(signInModel.Email, signInModel.Password);

			return result;
		}
		
		public async Task<string> CreateOrUpdateUser(string clientId, UserModel userModel)
		{

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				//var userAuthenticateModel = new UserAuthenticateModel() { FacebookToken = facebookToken, Email = email, Password = password };

				var response = await client.PostAsJsonAsync(string.Format("api/v1/clients/{0}/users", clientId), userModel);

				if (!response.IsSuccessStatusCode)
				{
					GeneralHelper.HandleWebApiFailedResult(response);
				}

				var userId = await response.Content.ReadAsAsync<string>();

				return userId;			

			}
		}
	}
}
