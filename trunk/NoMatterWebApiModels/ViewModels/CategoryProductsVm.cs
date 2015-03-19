﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApiModels.ViewModels
{
	public class CategoryProductsVm
	{
		public Client Client { get; set; }

		public Section Section { get; set; }

		public Category Category { get; set; }

		public List<Product> CategoryProducts { get; set; }
	}
}