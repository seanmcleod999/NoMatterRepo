using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApiModels.ViewModels
{
	public class AddProductVm
	{
		public string CategoryId { get; set; }

		public Product Product { get; set; }
	}
}