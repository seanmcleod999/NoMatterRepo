using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class GlobalSetting
	{
		public string SettingName { get; set; }

		public byte SettingType { get; set; }

		public string StringValue { get; set; }

		public int? IntValue { get; set; }
	}
}
