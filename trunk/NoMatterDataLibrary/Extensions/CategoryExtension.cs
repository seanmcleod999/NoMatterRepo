using System.Linq;
using NoMatterWebApiModels.Models;

namespace NoMatterDataLibrary.Extensions
{
	public static class CategoryExtension
	{
		public static Category ToDomainCategory(this   NoMatterDatabaseModel.Category category)
		{
			return new Category()
			{
				Section = category.Section.ToDomainSection(),
				CategoryId = category.CategoryId,
				CategoryName = category.CategoryName,
				CategoryFriendlyName = category.CategoryFriendlyName,
				CategoryOrder = category.CategoryOrder,
				CategoryDescription = category.CategoryDescription,
				ActionName = category.ActionName,
				Conditional = category.Conditional,
				Hidden = category.Hidden,
				Picture = category.Picture,
				VisibleProductCount = category.Products.Count(x => !x.Sold && !x.Hidden), //TODO may have to include release date in this
				FullProductCount = category.Products.Count() 
			};
		}

		public static NoMatterDatabaseModel.Category ToDatabaseCategory(this  Category category, int sectionId)
		{
			return new NoMatterDatabaseModel.Category
			{
				SectionId = sectionId,
				CategoryName = category.CategoryName,
				CategoryFriendlyName = category.CategoryFriendlyName,
				CategoryDescription = category.CategoryDescription,
				CategoryOrder = category.CategoryOrder,
				Hidden = category.Hidden,
				Picture = category.Picture

			};
		}
	}
}