using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApiModels.ViewModels
{
	public class SectionCategoriesVm
	{
		public Client Client { get; set; }

		public Section Section { get; set; }

		public List<Category> Categories { get; set; }
	}
}