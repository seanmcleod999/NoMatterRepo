using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class ClientDeliveryOption
	{
		public string ClientDeliveryOptionId { get; set; }

		public string Description { get; set; }

		public Decimal DeliveryAmount { get; set; }
	}
}
