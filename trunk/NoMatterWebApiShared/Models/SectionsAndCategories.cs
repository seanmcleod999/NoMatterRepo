using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoMatterWebApiModels.Models
{
	public class SectionsAndCategories
	{
		public int SectionId { get; set; }

		public string ClientId { get; set; }

		public string SectionName { get; set; }

		public string SectionDescription { get; set; }

		public short SectionOrder { get; set; }

		public string Picture { get; set; }

		public int FullCategoryCount { get; set; }

		public int VisibleCategoryCount { get; set; }

		public bool Hidden { get; set; }

		public List<Category> Categories { get; set; }
	}
}