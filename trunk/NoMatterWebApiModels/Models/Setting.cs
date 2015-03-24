using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class Setting
	{
		public short SettingId { get; set; }

		public string SettingName { get; set; }

		public string SettingDescription { get; set; }

		public string RegexValidation { get; set; }

		public string SettingType { get; set; }

		public string SettingCategory { get; set; }
	}
}
