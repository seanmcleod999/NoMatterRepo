﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterDataLibrary.Extensions;

namespace NoMatterDataLibrary.Extensions
{
	public static class SectionExtension
	{
		public static NoMatterWebApiModels.Models.Section ToDomainSection(this   NoMatterDatabaseModel.Section section)
		{
			return new NoMatterWebApiModels.Models.Section
			{
				SectionId = section.SectionId,
				Client = section.Client.ToDomainClient(),			
				SectionName = section.SectionName,
				SectionDescription = section.SectionDescription,
				SectionOrder = section.SectionOrder,
				Picture = section.Picture,
				Hidden = section.Hidden,
				FullCategoryCount = section.Categories.Count(x => !x.Conditional),
				VisibleCategoryCount = section.Categories.Count(x => !x.Hidden) 
			};
		}

		public static NoMatterWebApiModels.Models.SectionsAndCategories ToDomainSectionsAndCategories(this   NoMatterDatabaseModel.Section section)
		{
			return new NoMatterWebApiModels.Models.SectionsAndCategories
			{
				ClientId = section.Client.ClientUUID.ToString(),
				SectionId = section.SectionId,
				SectionName = section.SectionName,
				SectionDescription = section.SectionDescription,
				SectionOrder = section.SectionOrder,
				Picture = section.Picture,
				Hidden = section.Hidden,
				FullCategoryCount = section.Categories.Count(x => !x.Conditional),
				VisibleCategoryCount = section.Categories.Count(x => !x.Hidden),
				Categories = section.Categories.Select(x=>x.ToDomainCategory()).ToList()
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