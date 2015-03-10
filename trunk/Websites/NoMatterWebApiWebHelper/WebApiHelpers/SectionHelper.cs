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
		Task<Section> GetSectionAsync(string sectionId);
		Task PostSectionAsync(Section section, string token);
		Task DeleteSectionAsync(string sectionId, string token);
		Task UpdateSectionAsync(Section section, string token);
		Task<List<Category>> GetSectionCategoriesAsync(string sectionId);
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

		public async Task<Section> GetSectionAsync(string sectionId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/sections/{0}", sectionId));

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot get Section", response);	

				var section = await response.Content.ReadAsAsync<Section>();

				return section;
			}
		}



		public async Task PostSectionAsync(Section section, string token)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

				var response = await client.PostAsJsonAsync(string.Format("api/v1/clients/{0}/sections", _globalSettings.DefaultClientId), section);

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot save Section", response);

			}
		}

		public async Task DeleteSectionAsync(string sectionId, string token)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

				var response = await client.DeleteAsync(string.Format("api/v1/sections/{0}", sectionId));

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot delete Section", response);
			}
		}

		public async Task UpdateSectionAsync(Section section, string token)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

				var response = await client.PutAsJsonAsync(string.Format("api/v1/clients/{0}/sections/{1}", _globalSettings.DefaultClientId, section.SectionId), section);

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot save Section", response);

			}
		}

		public async Task<List<Category>> GetSectionCategoriesAsync(string sectionId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(string.Format("api/v1/sections/{0}/categories", sectionId));

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot get Categories", response);	
               
                var sections = await response.Content.ReadAsAsync<List<Category>>();

                return sections;
               
            }
        }
    }
}
