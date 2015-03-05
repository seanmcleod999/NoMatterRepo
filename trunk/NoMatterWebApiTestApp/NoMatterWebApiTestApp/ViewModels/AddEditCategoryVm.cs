using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace WebApplication7.ViewModels
{
	public class AddEditCategoryVm
	{
		public string SectionId { get; set; }

		public Category Category { get; set; }
	}
}