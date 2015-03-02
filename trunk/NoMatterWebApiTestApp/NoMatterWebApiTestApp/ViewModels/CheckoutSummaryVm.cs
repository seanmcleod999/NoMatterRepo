using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace WebApplication7.ViewModels
{
	public class CheckoutSummaryVm
	{
		public Order Order { get; set; }

		//public ClientDeliveryOption DeliveryOption { get; set; }

		public string PaymentType { get; set; }

		public List<ClientPaymentType> PaymentTypes { get; set; }
	}
}