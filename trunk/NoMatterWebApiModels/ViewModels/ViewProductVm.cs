using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApiModels.ViewModels
{
	public class ViewProductVm
	{
		public string ClientId { get; set; }

		public string FromCategory { get; set; }

		//public Category Category { get; set; }

		public Product Product { get; set; }
	}
}