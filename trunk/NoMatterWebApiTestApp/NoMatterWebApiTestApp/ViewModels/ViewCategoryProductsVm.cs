using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace WebApplication7.ViewModels
{
	public class ViewCategoryProductsVm
	{
		public Category Category { get; set; }
		public List<Product> CategoryProducts { get; set; }
	}
}