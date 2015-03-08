using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Extensions
{
	public static class CategoryExtension
	{
		public static NoMatterWebApiModels.Models.Category ToDomainCategory(this   NoMatterDatabaseModel.Category category)
		{
			return new NoMatterWebApiModels.Models.Category()
			{
				SectionId = category.Section.SectionUUID.ToString(),
				CategoryId = category.CategoryUUID.ToString(),
				CategoryName = category.CategoryName,
				CategoryOrder = category.CategoryOrder,
				CategoryDescription = category.CategoryDescription,
				ActionName = category.ActionName,
				Conditional = category.Conditional,
				Picture = category.Picture,
				VisibleProductCount = category.Products.Count(x => !x.Sold && !x.Hidden), //TODO may have to include release date in this
				FullProductCount = category.Products.Count() 
			};
		}

		public static NoMatterDatabaseModel.Category ToDatabaseCategory(this  NoMatterWebApiModels.Models.Category category, int sectionId)
		{
			return new NoMatterDatabaseModel.Category
			{
				SectionId = sectionId,
				CategoryName = category.CategoryName,
				CategoryDescription = category.CategoryDescription,
				CategoryOrder = category.CategoryOrder,
				Hidden = category.Hidden,
				Picture = category.Picture

			};
		}
	}
}