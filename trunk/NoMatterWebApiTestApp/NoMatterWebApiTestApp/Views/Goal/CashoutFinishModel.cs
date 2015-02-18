using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Views.Goal
{
	public class CashoutFinishModel
	{
		public string selected_account_token { get; set; }

		public string account_data { get; set; } //base64 string from cashout_start 

		public decimal raised_amount { get; set; }

		public decimal fee_amount { get; set; }

		public decimal available_amount { get; set; }
	}
}