using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.Exceptions;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace NoMatterWebApiWebHelper.WebApiHelpers
{
	public interface IClientHelper
	{
		Task PostClientSectionAsync(string clientId, Section section, string token);
		Task<Client> GetClientAsync(string clientId);
		Task<List<Client>> GetClientsAsync();
		Task<List<Section>> GetClientSectionsAsync(string clientId, bool includeEmpty, bool includeHidden);
		List<Section> GetClientSections(string clientId, bool includeEmpty, bool includeHidden);
		List<ClientSetting> GetClientSettings(string clientId);
		Task<List<ClientPaymentType>> GetClientPaymentTypes(string clientId);
		Task<List<ClientDeliveryOption>> GetClientDeliveryOptions(string clientId);
		Task<ClientPage> GetClientPage(string clientId, string pageName);
	}

    public class ClientHelper : IClientHelper
    {
		private IGlobalSettings _globalSettings;
		
		public ClientHelper()
		{
			_globalSettings = new GlobalSettings();
		}

		public ClientHelper(IGlobalSettings globalSettings)
		{
			_globalSettings = globalSettings;
		}

		public async Task PostClientSectionAsync(string clientId, Section section, string token)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

				var response = await client.PostAsJsonAsync(string.Format("api/v1/clients/{0}/sections", clientId), section);

				if (!response.IsSuccessStatusCode)
					GeneralHelper.HandleWebApiFailedResult(response);

			}
		}

		public async Task<Client> GetClientAsync(string clientId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/clients/{0}", clientId));

				if (!response.IsSuccessStatusCode)
				{
					GeneralHelper.HandleWebApiFailedResult(response);
				}

				var clients = await response.Content.ReadAsAsync<Client>();

				return clients;

			}
		}

		public async Task<List<Client>> GetClientsAsync()
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/clients"));

				if (!response.IsSuccessStatusCode)
				{
					GeneralHelper.HandleWebApiFailedResult(response);
				}
	
				var clients = await response.Content.ReadAsAsync<List<Client>>();

				return clients;
				
			}
		}

		public async Task<List<Section>> GetClientSectionsAsync(string clientId, bool includeEmpty, bool includeHidden)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/clients/{0}/Sections?includeEmpty={1}&includeHidden={2}", clientId, includeEmpty, includeHidden));

				if (!response.IsSuccessStatusCode)
				{
					GeneralHelper.HandleWebApiFailedResult(response);
				}	

				var sections = await response.Content.ReadAsAsync<List<Section>>();

				return sections;
			}
		}

		public List<Section> GetClientSections(string clientId, bool includeEmpty, bool includeHidden)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = client.GetAsync(string.Format("api/v1/clients/{0}/Sections?includeEmpty={1}&includeHidden={2}", clientId, includeEmpty, includeHidden)).Result;

				if (!response.IsSuccessStatusCode)
				{
					GeneralHelper.HandleWebApiFailedResult(response);
				}	

				var sections = response.Content.ReadAsAsync<List<Section>>().Result;

				return sections;
			}
		}

		public List<ClientSetting> GetClientSettings(string clientId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = client.GetAsync(string.Format("api/v1/clients/{0}/settings", clientId)).Result;

				if (!response.IsSuccessStatusCode)
				{
					GeneralHelper.HandleWebApiFailedResult(response);
				}	

				var clientSettings = response.Content.ReadAsAsync<List<ClientSetting>>().Result;

				return clientSettings;

			}
		}

		public async Task<List<ClientPaymentType>> GetClientPaymentTypes(string clientId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = client.GetAsync(string.Format("api/v1/clients/{0}/payment-types", clientId)).Result;

				if (!response.IsSuccessStatusCode)
				{
					GeneralHelper.HandleWebApiFailedResult(response);
				}			
				
				var paymentTypes = await response.Content.ReadAsAsync<List<ClientPaymentType>>();

				return paymentTypes;
				
			}
		}

		public async Task<List<ClientDeliveryOption>> GetClientDeliveryOptions(string clientId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = client.GetAsync(string.Format("api/v1/clients/{0}/delivery-options", clientId)).Result;

				if (!response.IsSuccessStatusCode)
					GeneralHelper.HandleWebApiFailedResult(response);

				var deliveryOptions = await response.Content.ReadAsAsync<List<ClientDeliveryOption>>();

				return deliveryOptions;

			}
		}

		public async Task<ClientPage> GetClientPage(string clientId, string pageName)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = client.GetAsync(string.Format("api/v1/clients/{0}/pages/{1}", clientId, pageName)).Result;

				if (!response.IsSuccessStatusCode)
					GeneralHelper.HandleWebApiFailedResult(response);

				var clientPage = await response.Content.ReadAsAsync<ClientPage>();

				return clientPage;

			}
		}

    }
}
