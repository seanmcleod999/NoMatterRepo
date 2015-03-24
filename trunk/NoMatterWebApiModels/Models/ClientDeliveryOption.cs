using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class ClientDeliveryOption
	{
		public short ClientDeliveryOptionId { get; set; }

		[Required(ErrorMessage = "required")]
		[StringLength(50, ErrorMessage = "Must be under 50 characters")]
		public string Description { get; set; }

		[Range(0, 500)]
		public Decimal DeliveryAmount { get; set; }

		[Required(ErrorMessage = "required")]
		public byte OptionOrder { get; set; }

		public bool Enabled { get; set; }

		public bool Selected { get; set; }
	}
}
