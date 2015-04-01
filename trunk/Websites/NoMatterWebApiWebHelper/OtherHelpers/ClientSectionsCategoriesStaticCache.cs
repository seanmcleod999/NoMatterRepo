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
	public static class ClientSectionsCategoriesStaticCache
	{

		private static List<SectionsAndCategories> _clientSectionsCategories;

		static  ClientSectionsCategoriesStaticCache()
		{
			if (_clientSectionsCategories == null) LoadClientSectionsCategoriesCache();
		}


		public static void LoadClientSectionsCategoriesCache()
		{
			var clientHelper = new ClientHelper();

			var clientId = ConfigurationManager.AppSettings["SiteClientId"];

			_clientSectionsCategories = clientHelper.GetClientSectionsAndCategories(clientId, false, false);
		}

		public static List<SectionsAndCategories> GetClientSectionsAndCategories()
		{
			return _clientSectionsCategories;
		}

		public static SectionsAndCategories GetClientSectionById(string sectionId)
		{
			return _clientSectionsCategories.SingleOrDefault(x => x.SectionId == sectionId);
		}

		public static SectionsAndCategories GetClientSectionByName(string sectionName)
		{
			return _clientSectionsCategories.SingleOrDefault(x => x.SectionName == sectionName);
		}

		public static Category GetSectionCategoryByName(string sectionName, string categoryName)
		{

			var section = _clientSectionsCategories.FirstOrDefault(x => x.SectionName == sectionName);

			if (section != null)
			{
				var category = section.Categories.FirstOrDefault(x => x.CategoryName == categoryName);

				if (category != null) return category;

			}

			return null;
		}

		public static Category GetSectionCategoryById(string categoryId)
		{
			var category = _clientSectionsCategories.SelectMany(x => x.Categories).FirstOrDefault(x => x.CategoryId == categoryId);

			return category ?? null;
		}

		public static List<Category> GetSectionCategories(string sectionName)
		{
			var section = _clientSectionsCategories.FirstOrDefault(x => x.SectionName == sectionName);

			return section != null ? section.Categories : null;
		}
	}
}
