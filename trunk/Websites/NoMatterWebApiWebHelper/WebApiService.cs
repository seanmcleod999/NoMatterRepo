using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NoMatterWebApiWebHelper.Exceptions;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace NoMatterWebApiWebHelper
{
	public class WebApiService
	{
		private WebApiService(string baseUri)
		{
			BaseUri = baseUri;
		}

		private static WebApiService _instance;

		public static WebApiService Instance
		{
			get { return _instance ?? (_instance = new WebApiService(StaticGlobalSettings.ApiBaseAddress)); }
		}

		public string BaseUri { get; private set; }

		public async Task<T> AuthenticateAsync<T>(string userName, string password)
		{
			using (var client = new HttpClient())
			{
				var result = await client.PostAsync(BuildActionUri("token"), new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("userName", userName), 
                    new KeyValuePair<string, string>("password", password)
                }));

				
				if (result.IsSuccessStatusCode)
				{
					string json = await result.Content.ReadAsStringAsync();

					return JsonConvert.DeserializeObject<T>(json);
				}

				//throw new ApiException(result.StatusCode, json);
				throw new WebApiException(result);
			}
		}

		public async Task<T> GetAsync<T>(string action, string authToken = null)
		{
			using (var client = new HttpClient())
			{
				if (!string.IsNullOrWhiteSpace(authToken))
				{
					//Add the authorization header
					client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + authToken);
				}

				var result = await client.GetAsync(BuildActionUri(action));

				
				if (result.IsSuccessStatusCode)
				{
					string json = await result.Content.ReadAsStringAsync();

					return JsonConvert.DeserializeObject<T>(json);
				}

				GeneralHelper.HandleWebApiFailedResult(result);

				throw new WebApiException(result);
			}
		}

		public async Task PutAsync<T>(string action, T data, string authToken = null)
		{
			using (var client = new HttpClient())
			{
				if (!string.IsNullOrWhiteSpace(authToken))
				{
					//Add the authorization header
					client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + authToken);
				}

				var result = await client.PutAsJsonAsync(BuildActionUri(action), data);
				if (result.IsSuccessStatusCode)
				{
					return;
				}

				GeneralHelper.HandleWebApiFailedResult(result);
			}
		}

		public async Task PostAsync<T>(string action, T data, string authToken = null)
		{
			using (var client = new HttpClient())
			{
				if (!string.IsNullOrWhiteSpace(authToken))
				{
					//Add the authorization header
					client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + authToken);
				}

				var result = await client.PostAsJsonAsync(BuildActionUri(action), data);
				if (result.IsSuccessStatusCode)
				{
					return;
				}

				GeneralHelper.HandleWebApiFailedResult(result);

			}
		}

		public async Task DeleteAsync(string action, string authToken = null)
		{
			using (var client = new HttpClient())
			{
				if (!string.IsNullOrWhiteSpace(authToken))
				{
					//Add the authorization header
					client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + authToken);
				}

				var result = await client.DeleteAsync(BuildActionUri(action));

				string json = await result.Content.ReadAsStringAsync();
				if (result.IsSuccessStatusCode)
				{
					return;
				}

				GeneralHelper.HandleWebApiFailedResult(result);
			}
		}

		private string BuildActionUri(string action)
		{
			return BaseUri + action;
		}
	}
}
