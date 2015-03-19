using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class ClientPaymentType
	{
		public string ClientPaymentTypeId { get; set; }

		public short PaymentTypeId { get; set; }

		public string PaymentTypeName { get; set; }

		public string PaymentTypeDetails { get; set; }

		public string PaymentTypePicture { get; set; }

		public bool Selected { get; set; }
	}
}
