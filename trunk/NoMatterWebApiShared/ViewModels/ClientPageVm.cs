using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApiModels.ViewModels
{
	public class ClientPageVm
	{
		public Client Client { get; set; }
		public ClientPage ClientPage { get; set; }
	}
}