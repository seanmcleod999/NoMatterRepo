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
