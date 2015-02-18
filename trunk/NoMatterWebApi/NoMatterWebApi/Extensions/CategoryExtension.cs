using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Extensions
{
	public static class CategoryExtension
	{
		public static Models.Category ToDomainCategory(this   NoMatterApiDAL.DatabaseModel.Category category)
		{
			return new Models.Category()
			{
				CategoryId = category.CategoryUUID.ToString(),
				CategoryName = category.CategoryName,
				CategoryDescription = category.CategoryDescription,
				Picture = category.Picture
			};
		}
	}
}