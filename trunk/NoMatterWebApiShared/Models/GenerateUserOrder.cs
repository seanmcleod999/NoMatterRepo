using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class GenerateUserOrder
	{
		public string CartId { get; set; }

		public string Message { get; set; }

		public int ClientDeliveryOptionId { get; set; }

	}
}
