using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace NoMatterWebApiWebHelper
{
	public interface IGlobalHelper
	{
		List<GlobalSetting> GetGlobalSettings();
	}

	class GlobalHelper : IGlobalHelper
	{
		private IGlobalSettings _globalSettings;
		
		public GlobalHelper()
		{
			_globalSettings = new GlobalSettings();
		}

		public GlobalHelper(IGlobalSettings globalSettings)
		{
			_globalSettings = globalSettings;
		}

		public List<GlobalSetting> GetGlobalSettings()
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = client.GetAsync("api/v1/global/settings").Result;

				if (response.IsSuccessStatusCode)
				{
					var globalSettings = response.Content.ReadAsAsync<List<GlobalSetting>>().Result;

					return globalSettings;
				}

				throw new Exception(string.Format("Cannot get global Settings. Status Code: {0}. Reason:{1}", response.StatusCode, response.ReasonPhrase));

			}
		}
	}
}
