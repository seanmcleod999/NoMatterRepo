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
		public List<GlobalSetting> GetGlobalSettings()
		{
			var settings = WebApiService.Instance.GetAsync<List<GlobalSetting>>(
				"api/v1/globals/settings").Result;

			return settings;
		}
	}
}
