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
	public interface ISectionHelper
	{
		Task<Section> GetSectionAsync(string clientId, string sectionId);
		
		Task DeleteSectionAsync(string sectionId, string token);
		Task UpdateSectionAsync(string clientId, Section section, string token);
		Task<List<Category>> GetSectionCategoriesAsync(string sectionId, bool includeEmpty, bool includeHidden);
		Task PostSectionCategoryAsync(string clientId, Category category, string token);
	}

	public class SectionHelper : ISectionHelper
    {
		private IGlobalSettings _globalSettings;
		
		public SectionHelper()
		{
			_globalSettings = new GlobalSettings();
		}

		public SectionHelper(IGlobalSettings globalSettings)
		{
			_globalSettings = globalSettings;
		}

		public async Task PostSectionCategoryAsync(string clientId, Category category, string token)
		{

			await WebApiService.Instance.PostAsync(string.Format("api/v1/clients/{0}/sections/{1}/categories", clientId, category.SectionId), category, token);

			//using (var client = new HttpClient())
			//{
			//	client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
			//	client.DefaultRequestHeaders.Accept.Clear();
			//	client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			//	client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

			//	var response = await client.PostAsJsonAsync(string.Format("api/v1/clients/{0}/sections/{1}/categories", clientId, category.SectionId), category);

			//	if (!response.IsSuccessStatusCode)
			//	{
			//		GeneralHelper.HandleWebApiFailedResult(response);
			//	}

			//}
		}

		public async Task<Section> GetSectionAsync(string clientId, string sectionId)
		{
			var section = await WebApiService.Instance.GetAsync<Section>(string.Format("api/v1/clients/{0}/sections/{1}", clientId, sectionId));

			return section;
		}

		public async Task DeleteSectionAsync(string sectionId, string token)
		{
			await WebApiService.Instance.DeleteAsync(string.Format("api/v1/clients/{0}/sections/{1}", StaticGlobalSettings.SiteClientId, sectionId), token);
		}

		public async Task UpdateSectionAsync(string clientId, Section section, string token)
		{
			await WebApiService.Instance.PutAsync(string.Format("api/v1/clients/{0}/sections/{1}", clientId, section.SectionId), section, token);
		}

		public async Task<List<Category>> GetSectionCategoriesAsync(string sectionId, bool includeEmpty, bool includeHidden)
        {
			var categories = await WebApiService.Instance.GetAsync<List<Category>>(string.Format("api/v1/clients/{0}/sections/{1}/categories?includeEmpty={2}&includeHidden={3}", _globalSettings.SiteClientId, sectionId, includeEmpty, includeHidden));

			return categories;
        }
    }
}
