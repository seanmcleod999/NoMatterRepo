

using System.ComponentModel.DataAnnotations;

namespace NoMatterWebApiModels.Models
{
	public class ClientSetting
	{
		public Client Client { get; set; }

		public int ClientSettingId { get; set; }

		public short SettingId { get; set; }

		public byte SettingType { get; set; }

		public string StringValue { get; set; }

		public int? IntValue { get; set; }

		public Setting Setting { get; set; }
	}
}
