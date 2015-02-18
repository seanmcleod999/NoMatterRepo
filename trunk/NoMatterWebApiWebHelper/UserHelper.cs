using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace NoMatterWebApiWebHelper
{
	public interface IUserHelper
	{
		Task<UserAuthenticatedResult> Login(string clientId, string facebookToken, string email, string password);
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
				client.BaseAddress = new Uri(_globalSettings.BaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var userAuthenticateModel = new UserAuthenticateModel() { FacebookToken = facebookToken, Email = email, Password = password };

				var response = await client.PostAsJsonAsync(string.Format("api/v1/clients/{0}/users/authenticate", clientId), userAuthenticateModel);

				if (response.IsSuccessStatusCode)
				{
					var userAuthenticatedResult = await response.Content.ReadAsAsync<UserAuthenticatedResult>();

					return userAuthenticatedResult;
				}

				throw new Exception("Cannot login via facebook. " + response.ToString());
			}		
		}
	}
}
