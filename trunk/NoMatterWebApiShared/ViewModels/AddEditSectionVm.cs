using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApiModels.ViewModels
{
	public class AddEditSectionVm
	{
		public Section Section { get; set; }

		[Display(Name = "Picture")]
		public HttpPostedFileBase Picture { get; set; }
	}
}