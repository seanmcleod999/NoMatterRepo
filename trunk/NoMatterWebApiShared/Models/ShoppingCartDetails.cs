﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class ShoppingCartDetails
	{
		public List<Product> Products { get; set; }

		public decimal TotalAmount { get; set; }
	}
}
