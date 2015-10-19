using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApiModels.Models
{
	public class BankDetails
	{
		public string BankName { get; set; }
		public string AccountName { get; set; }
		public string BranchName { get; set; }
		public string BranchNumber { get; set; }
		public string AccountNumber { get; set; }
	}
}