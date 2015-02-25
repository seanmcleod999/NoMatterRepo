

namespace NoMatterWebApiModels.Models
{
	public class ClientSetting
	{
		public string SettingName { get; set; }

		public byte SettingType { get; set; }

		public string StringValue { get; set; }

		public int? IntValue { get; set; }
	}
}
