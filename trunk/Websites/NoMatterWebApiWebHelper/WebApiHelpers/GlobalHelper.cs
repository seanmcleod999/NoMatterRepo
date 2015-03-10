using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.Exceptions;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace NoMatterWebApiWebHelper.WebApiHelpers
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

				var response = client.GetAsync("api/v1/globals/settings").Result;

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot get global Settings", response);

				var globalSettings = response.Content.ReadAsAsync<List<GlobalSetting>>().Result;

				return globalSettings;

			}
		}
	}
}
