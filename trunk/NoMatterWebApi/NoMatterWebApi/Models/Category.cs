using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Models
{
	public class Category
	{
		public string CategoryId { get; set; }

		public string CategoryName { get; set; }

		public string CategoryDescription { get; set; }

		public string Picture { get; set; }
	}
}