

using System.ComponentModel.DataAnnotations;

namespace NoMatterWebApiModels.Models
{
	public class ClientSetting
	{
		public int ClientSettingId { get; set; }

		//[Required(ErrorMessage = "required")]
		//[StringLength(50, ErrorMessage = "Must be under 50 characters")]
		//public string SettingName { get; set; }

		public byte SettingType { get; set; }

		public string StringValue { get; set; }

		public int? IntValue { get; set; }

		public Setting Setting { get; set; }
	}
}
