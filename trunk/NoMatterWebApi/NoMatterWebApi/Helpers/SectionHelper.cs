using System.Threading.Tasks;
using NoMatterDataLibrary;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApi.Helpers
{
	public class SectionHelper
	{

		public static async Task AddDefaultSectionCategories(int sectionId, ICategoryRepository categoryRepository)
		{
			//Now add the defualt system categories
			var latestItemsCategory = new Category
			{
				CategoryName = "Latest Items",
				ActionName = "Latest",
				CategoryOrder = 1,
				Conditional = true,
				Hidden = false,
				Section = new Section { SectionId = sectionId }
			};

			var salesItemsCategory = new Category
			{
				CategoryName = "Sale Items",
				ActionName = "Sale",
				CategoryOrder = 2,
				Conditional = true,
				Hidden = false,
				Section = new Section { SectionId = sectionId }
			};

			await categoryRepository.AddSectionCategoryAsync(latestItemsCategory);
			await categoryRepository.AddSectionCategoryAsync(salesItemsCategory);
		}

		public static async Task DeleteDefaultSectionCategories(ICategoryRepository categoryRepository, int sectionId)
		{
			var sectionCategories = await categoryRepository.GetSectionCategoriesAsync(sectionId, true);

			//Delete the default Latest Items and Sale categories
			foreach (var category in sectionCategories)
			{
				if (category.ActionName == "Latest" || category.ActionName == "Sale")
				{
					//Delete latest item and sale categories
					await categoryRepository.DeleteCategoryAsync(category.CategoryId);
				}
			}
		}
	}
}