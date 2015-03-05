using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.WebApiHelpers;

namespace NoMatterWebApiWebHelper.OtherHelpers
{
	public static class ClientSectionsStaticCache
	{

		private static List<Section> _clientSections;

		static  ClientSectionsStaticCache()
		{
			if (_clientSections == null) LoadClientSectionsCache();
		}


		public static void LoadClientSectionsCache()
		{
			var clientHelper = new ClientHelper();

			var clientId = ConfigurationManager.AppSettings["DefaultClientId"];

			_clientSections = clientHelper.GetClientSections(clientId);
		}

		public static List<Section> GetClientSections()
		{
			return _clientSections;
		}

		public static Section GetClientSection(string sectionId)
		{
			return _clientSections.SingleOrDefault(x => x.SectionId == sectionId);
		}
	}
}
