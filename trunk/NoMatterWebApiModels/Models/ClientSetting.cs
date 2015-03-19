

using System.ComponentModel.DataAnnotations;

namespace NoMatterWebApiModels.Models
{
	public class ClientSetting
	{
		public short SettingId { get; set; }

		[Required(ErrorMessage = "required")]
		[StringLength(50, ErrorMessage = "Must be under 50 characters")]
		public string SettingName { get; set; }

		public byte SettingType { get; set; }

		public string StringValue { get; set; }

		public short? IntValue { get; set; }
	}
}
