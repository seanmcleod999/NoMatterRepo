using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApiModels.ViewModels
{
	public class ClientPagesVm
	{
		public Client Client { get; set; }
		public List<ClientPage> ClientPages { get; set; }

	}
}
