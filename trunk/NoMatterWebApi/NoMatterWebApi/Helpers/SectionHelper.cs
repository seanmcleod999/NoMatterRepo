using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NoMatterWebApi.DAL;

namespace NoMatterWebApi.Helpers
{
	public class SectionHelper
	{

		public static async Task AddDefaultSectionCategories(int sectionId, ICategoryRepository categoryRepository)
		{
			//Now add the defualt system categories
			var latestItemsCategory = new NoMatterDatabaseModel.Category
			{
				CategoryName = "Latest Items",
				ActionName = "Latest",
				CategoryOrder = 1,
				Conditional = true,
				Hidden = false,
				SectionId = sectionId
			};

			var salesItemsCategory = new NoMatterDatabaseModel.Category()
			{
				CategoryName = "Sale Items",
				ActionName = "Sale",
				CategoryOrder = 2,
				Conditional = true,
				Hidden = false,
				SectionId = sectionId
			};

			await categoryRepository.AddSectionCategoryAsync(latestItemsCategory);
			await categoryRepository.AddSectionCategoryAsync(salesItemsCategory);
		}

		public static async Task DeleteDefaultSectionCategories(ICategoryRepository categoryRepository, string sectionId)
		{
			var sectionCategories = await categoryRepository.GetSectionCategoriesAsync(new Guid(sectionId), true);

			//Delete the default Latest Items and Sale categories
			foreach (var category in sectionCategories)
			{
				if (category.ActionName == "Latest" || category.ActionName == "Sale")
				{
					//Delete latest item and sale categories
					await categoryRepository.DeleteCategoryAsync(category.CategoryUUID);
				}
			}
		}
	}
}