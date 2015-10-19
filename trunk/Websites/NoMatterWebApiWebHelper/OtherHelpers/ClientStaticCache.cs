using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.WebApiHelpers;

namespace NoMatterWebApiWebHelper.OtherHelpers
{
	public static class ClientStaticCache
	{
		private static Client _client;

		static  ClientStaticCache()
		{
			if (_client == null) LoadClientCache();
		}


		public static void LoadClientCache()
		{
			var clientHelper = new ClientHelper();

			var clientId = ConfigurationManager.AppSettings["SiteClientId"];

			_client = clientHelper.GetClient(clientId);
		}

		public static Client Client()
		{
			return _client;
		}


		
	}


}
