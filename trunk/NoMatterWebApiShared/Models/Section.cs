using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoMatterWebApiModels.Models
{
	public class Section
	{
		public int SectionId { get; set; }

		public Client Client { get; set; }

		[Required(ErrorMessage = "required")]
		[StringLength(50, ErrorMessage = "Must be under 50 characters")]
		public string SectionName { get; set; }

		public string SectionDescription { get; set; }

		[Range(0,100)]
		[Required(ErrorMessage = "required")]
		public short SectionOrder { get; set; }

		public string Picture { get; set; }

		public int FullCategoryCount { get; set; }

		public int VisibleCategoryCount { get; set; }

		public bool Hidden { get; set; }
	}
}