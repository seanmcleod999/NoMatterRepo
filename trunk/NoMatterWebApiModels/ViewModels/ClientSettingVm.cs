using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApiModels.ViewModels
{
	public class ClientSettingVm
	{
		public Client Client { get; set; }

		public ClientSetting ClientSetting { get; set; }
	}
}