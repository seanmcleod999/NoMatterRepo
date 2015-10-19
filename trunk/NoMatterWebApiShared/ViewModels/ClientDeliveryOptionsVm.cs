using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApiModels.ViewModels
{
	public class ClientDeliveryOptionsVm
	{
		public Client Client { get; set; }
		public List<ClientDeliveryOption> ClientDeliveryOptions { get; set; }
	}
}
