﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class AddProductToCartModel
	{
		public int ProductId { get; set; }

		public int Quantity { get; set; }

	}
}
