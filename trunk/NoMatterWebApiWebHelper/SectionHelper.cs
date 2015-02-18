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
				client.BaseAddress = new Uri(_globalSettings.BaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/sections/{0}", sectionId));

				if (response.IsSuccessStatusCode)
				{
					var section = await response.Content.ReadAsAsync<Section>();

					return section;
				}

				throw new Exception("Cannot get Section. " + response.ToString());

			}
		}

		public async Task<List<Category>> GetSectionCategoriesAsync(string sectionId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_globalSettings.BaseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(string.Format("api/v1/sections/{0}/categories", sectionId));

                if (response.IsSuccessStatusCode)
                {
                    var sections = await response.Content.ReadAsAsync<List<Category>>();

                    return sections;
                }

                throw new Exception("Cannot get Categories. " + response.ToString());

            }
        }
    }
}
