using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Extensions
{
	public static class SectionExtension
	{
		public static NoMatterWebApiModels.Models.Section ToDomainSection(this   NoMatterDatabaseModel.Section section)
		{
			return new NoMatterWebApiModels.Models.Section
			{
				SectionId = section.SectionUUID.ToString(),
				SectionName = section.SectionName,
				CategoryCount = section.Categories.Count
			};
		}
	}
}