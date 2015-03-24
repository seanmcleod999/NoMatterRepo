﻿using System;
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
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

				var response = await client.PostAsJsonAsync(string.Format("api/v1/clients/{0}/sections/{1}/categories", clientId, category.SectionId), category);

				if (!response.IsSuccessStatusCode)
				{
					GeneralHelper.HandleWebApiFailedResult(response);
				}

			}
		}

		public async Task<Section> GetSectionAsync(string clientId, string sectionId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/clients/{0}/sections/{1}", clientId, sectionId));

				if (!response.IsSuccessStatusCode)
				{
					GeneralHelper.HandleWebApiFailedResult(response);
				}

				var section = await response.Content.ReadAsAsync<Section>();

				return section;

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

				var response = await client.DeleteAsync(string.Format("api/v1/clients/{0}/sections/{1}", _globalSettings.SiteClientId, sectionId));
					
				if (!response.IsSuccessStatusCode)
				{
					GeneralHelper.HandleWebApiFailedResult(response);
				}
			}
		}

		public async Task UpdateSectionAsync(string clientId, Section section, string token)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

				var response = await client.PutAsJsonAsync(string.Format("api/v1/clients/{0}/sections/{1}", clientId, section.SectionId), section);

				if (!response.IsSuccessStatusCode)
				{
					GeneralHelper.HandleWebApiFailedResult(response);
				}

			}
		}

		public async Task<List<Category>> GetSectionCategoriesAsync(string sectionId, bool includeEmpty, bool includeHidden)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/clients/{0}/sections/{1}/categories?includeEmpty={2}&includeHidden={3}", _globalSettings.SiteClientId, sectionId, includeEmpty, includeHidden));

				if (!response.IsSuccessStatusCode)
				{
					GeneralHelper.HandleWebApiFailedResult(response);
				}
               
                var sections = await response.Content.ReadAsAsync<List<Category>>();

                return sections;
               
            }
        }
    }
}