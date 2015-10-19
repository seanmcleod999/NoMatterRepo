using System.Collections.Generic;
using System.Threading.Tasks;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace NoMatterWebApiWebHelper.WebApiHelpers
{
	public interface IClientHelper
	{
		Task PostClientSectionAsync(string clientId, Section section, string token);
		//Task<Client> GetClientAsync(string clientId);
		Client GetClient(string clientId);
		Task<List<Client>> GetClientsAsync();
		Task<List<Section>> GetClientSectionsAsync(string clientId, bool includeEmpty, bool includeHidden);
		List<Section> GetClientSections(string clientId, bool includeEmpty, bool includeHidden);
		List<ClientSetting> GetClientSettings(string clientId);
		Task<List<ClientPaymentType>> GetClientPaymentTypes(string clientId);
		Task<List<ClientDeliveryOption>> GetClientDeliveryOptions(string clientId);
		Task<ClientPage> GetClientPage(string clientId, string pageName);
		List<SectionsAndCategories> GetClientSectionsAndCategories(string clientId, bool includeEmpty, bool includeHidden);
	}

    public class ClientHelper : IClientHelper
    {

		public async Task PostClientSectionAsync(string clientId, Section section, string token)
		{
			await WebApiService.Instance.PostAsync(string.Format("api/v1/clients/{0}/sections", clientId), section, token);
		}

		//public async Task<Client> GetClientAsync(string clientId)
		//{
		//	var client = await WebApiService.Instance.GetAsync<Client>(string.Format("api/v1/clients/{0}", clientId));

		//	return client;
		//}

		public Client GetClient(string clientId)
		{
			var client = WebApiService.Instance.GetAsync<Client>(string.Format("api/v1/clients/{0}", clientId)).Result;

			return client;
		}

		public async Task<List<Client>> GetClientsAsync()
		{
			var clients = await WebApiService.Instance.GetAsync<List<Client>>(string.Format("api/v1/clients"));

			return clients;
		}

		public async Task<List<Section>> GetClientSectionsAsync(string clientId, bool includeEmpty, bool includeHidden)
		{
			var sections = await WebApiService.Instance.GetAsync<List<Section>>(
					string.Format("api/v1/clients/{0}/Sections?includeEmpty={1}&includeHidden={2}", clientId, includeEmpty, includeHidden));

			return sections;
		}

		public List<Section> GetClientSections(string clientId, bool includeEmpty, bool includeHidden)
		{
			var sections = WebApiService.Instance.GetAsync<List<Section>>(
				string.Format("api/v1/clients/{0}/Sections?includeEmpty={1}&includeHidden={2}", clientId, includeEmpty, includeHidden)).Result;

			return sections;
		}

		public List<SectionsAndCategories> GetClientSectionsAndCategories(string clientId, bool includeEmpty, bool includeHidden)
		{
			var sectionsAndCategories = WebApiService.Instance.GetAsync<List<SectionsAndCategories>>(
				string.Format("api/v1/clients/{0}/sectionsandcategories?includeEmpty={1}&includeHidden={2}", clientId, includeEmpty, includeHidden)).Result;

			return sectionsAndCategories;
		}

		public List<ClientSetting> GetClientSettings(string clientId)
		{
			var settings = WebApiService.Instance.GetAsync<List<ClientSetting>>(
					string.Format("api/v1/clients/{0}/settings", clientId)).Result;

			return settings;
		}

		public async Task<List<ClientPaymentType>> GetClientPaymentTypes(string clientId)
		{
			var paymentTypes = await WebApiService.Instance.GetAsync<List<ClientPaymentType>>(
				string.Format("api/v1/clients/{0}/payment-types", clientId));

			return paymentTypes;
		}

		public async Task<List<ClientDeliveryOption>> GetClientDeliveryOptions(string clientId)
		{
			var deliveryOptions = await WebApiService.Instance.GetAsync<List<ClientDeliveryOption>>(
				string.Format("api/v1/clients/{0}/delivery-options", clientId));

			return deliveryOptions;
		}

		public async Task<ClientPage> GetClientPage(string clientId, string pageName)
		{
			var page = await WebApiService.Instance.GetAsync<ClientPage>(
				string.Format("api/v1/clients/{0}/pages/{1}", clientId, pageName));

			return page;
		}
    }
}
