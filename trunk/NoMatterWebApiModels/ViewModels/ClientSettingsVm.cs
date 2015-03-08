using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApiModels.ViewModels
{
	public class ClientSettingsVm
	{
		public string ClientId { get; set; }

		public List<ClientSetting> ClientSettings { get; set; }
	}
}