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
				ClientId = section.Client.ClientUUID.ToString(),
				SectionId = section.SectionUUID.ToString(),
				SectionName = section.SectionName,
				SectionDescription = section.SectionDescription,
				SectionOrder = section.SectionOrder,
				Picture = section.Picture,
				Hidden = section.Hidden,
				FullCategoryCount = section.Categories.Count(x => !x.Conditional),
				VisibleCategoryCount = section.Categories.Count(x => !x.Hidden) 
			};
		}

		public static NoMatterDatabaseModel.Section ToDatabaseSection(this  NoMatterWebApiModels.Models.Section section, int clientId)
		{
			return new NoMatterDatabaseModel.Section
			{
				ClientId = clientId,
				SectionName = section.SectionName,
				SectionDescription = section.SectionDescription,
				SectionOrder = section.SectionOrder,
				Hidden = section.Hidden,
				Picture = section.Picture
				
			};
		}
	}
}