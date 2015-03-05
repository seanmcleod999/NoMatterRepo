using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class Order
	{
		public int OrderId { get; set; }

		public string Message { get; set; }

		public decimal ProductAmount { get; set; }

		public decimal DeliveryAmount { get; set; }

		public decimal TotalAmount { get; set; }

		public string DeliveryDescription { get; set; }

		public short OrderStatusId { get; set; }

		public bool Paid { get; set; }

		public List<Product> Products { get; set; }

		public User User { get; set; }
	}
}
