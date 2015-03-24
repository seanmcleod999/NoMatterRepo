using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Extensions
{
	public static class ClientSettingExtension
	{
		public static NoMatterWebApiModels.Models.ClientSetting ToDomainClientSetting(this   NoMatterDatabaseModel.ClientSetting clientSetting)
		{
			return new NoMatterWebApiModels.Models.ClientSetting
			{
				ClientSettingId = clientSetting.ClientSettingId,
				//SettingName = clientSetting.SettingName,
				//SettingType = clientSetting.SettingType,
				StringValue = clientSetting.StringValue,
				IntValue = clientSetting.IntValue,
				Setting = clientSetting.Setting.ToDomainSetting()
			};
		}

		public static NoMatterDatabaseModel.ClientSetting ToDatabaseClientSetting(this  NoMatterWebApiModels.Models.ClientSetting clientSetting, int clientId)
		{
			return new NoMatterDatabaseModel.ClientSetting
			{
				ClientSettingId = clientSetting.ClientSettingId,
				ClientId = clientId,
				//SettingName = clientSetting.SettingName,
				StringValue = clientSetting.StringValue,
				IntValue = clientSetting.IntValue,
				//SettingType = (byte) (clientSetting.StringValue != null ? 1 : 2)
			};
		}
	}
}