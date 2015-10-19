using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class DiscountDetails
	{
		public bool Discounted { get; set; }
		public decimal DiscountAmount { get; set; }
		public decimal DiscountedPrice { get; set; }
		public short DiscountTypeId { get; set; }
	}
}
