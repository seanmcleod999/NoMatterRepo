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
		Task<List<Client>> GetClientsAsync();
		Task<List<Section>> GetClientSectionsAsync(string clientId);
		List<Section> GetClientSections(string clientId);
		List<ClientSetting> GetClientSettings(string clientId);
		Task<List<ClientPaymentType>> GetClientPaymentTypes(string clientId);
		Task<List<ClientDeliveryOption>> GetClientDeliveryOptions(string clientId);
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

		public async Task<List<Client>> GetClientsAsync()
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/clients"));

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot get Clients", response);	
	
				var clients = await response.Content.ReadAsAsync<List<Client>>();

				return clients;
				
			}
		}

		public async Task<List<Section>> GetClientSectionsAsync(string clientId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/clients/{0}/Sections", clientId));

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot get Sections", response);	

				var sections = await response.Content.ReadAsAsync<List<Section>>();

				return sections;
			}
		}

		public List<Section> GetClientSections(string clientId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = client.GetAsync(string.Format("api/v1/clients/{0}/Sections", clientId)).Result;

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot get Sections", response);

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
					throw new WebApiException("Cannot get Client Settings", response);		

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
					throw new WebApiException("Cannot get Client Payment Types", response);				
				
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
					throw new WebApiException("Cannot get Client Delivery Options", response);

				var deliveryOptions = await response.Content.ReadAsAsync<List<ClientDeliveryOption>>();

				return deliveryOptions;

			}
		}

    }
}
