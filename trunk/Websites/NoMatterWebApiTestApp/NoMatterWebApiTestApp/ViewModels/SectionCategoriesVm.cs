using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace WebApplication7.ViewModels
{
	public class SectionCategoriesVm
	{
		public Section Section { get; set; }

		public List<Category> Categories { get; set; }
	}
}