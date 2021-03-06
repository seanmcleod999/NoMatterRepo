﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace WebApplication7.ViewModels
{
	public class CheckoutPostageVm
	{
		public string UserId { get; set; }

		public string DeliveryOption { get; set; }

		public List<ClientDeliveryOption> DeliveryOptions { get; set; }

		public string Voucher { get; set; }

		[Display(Name = "Order Comments")]
		public string OrderComments { get; set; }
	}
}