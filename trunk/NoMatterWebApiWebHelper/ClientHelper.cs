using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace NoMatterWebApiWebHelper
{
	public interface IClientHelper
	{
		Task<List<Client>> GetClientsAsync();
		Task<List<Section>> GetClientSectionsAsync(string clientId);
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
				client.BaseAddress = new Uri(_globalSettings.BaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/clients"));

				if (response.IsSuccessStatusCode)
				{
					var clients = await response.Content.ReadAsAsync<List<Client>>();

					return clients;
				}

				throw new Exception("Cannot get Clients. " + response.ToString());
				
			}
		}

		public async Task<List<Section>> GetClientSectionsAsync(string clientId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.BaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/clients/{0}/Sections", clientId));

				if (response.IsSuccessStatusCode)
				{
					var sections = await response.Content.ReadAsAsync<List<Section>>();

					return sections;
				}

				throw new Exception("Cannot get Sections. " + response.ToString());

			}
		}

    }
}
