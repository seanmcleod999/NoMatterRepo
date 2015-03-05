using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.WebApiHelpers;

namespace NoMatterWebApiWebHelper.OtherHelpers
{
	public static class SectionCategoriesSessionCache
	{

		public static async Task<List<Category>> GetSectionCategories(ISectionHelper sectionHelper, string sectionId)
		{
			if (HttpContext.Current.Session["section-" + sectionId] == null)
			{
				var categories = await sectionHelper.GetSectionCategoriesAsync(sectionId);

				HttpContext.Current.Session["section-" + sectionId] = categories;
			}

			return (List<Category>)HttpContext.Current.Session["section-" + sectionId];
		}

		public static async Task<Category> GetSectionCategory(ISectionHelper sectionHelper, string sectionId, string categoryId)
		{
			var categories = new List<Category>();

			if (HttpContext.Current.Session["section-" + sectionId] == null)
			{
				categories = await sectionHelper.GetSectionCategoriesAsync(sectionId);

				HttpContext.Current.Session["section-" + sectionId] = categories;
			}
			else
			{
				categories = (List<Category>)HttpContext.Current.Session["section-" + sectionId];
			}

			return categories.SingleOrDefault(x => x.CategoryId == categoryId);
		}

	}
}
