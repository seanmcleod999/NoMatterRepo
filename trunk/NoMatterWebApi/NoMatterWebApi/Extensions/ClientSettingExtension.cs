using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Extensions
{
	public static class ClientSettingExtension
	{
		public static NoMatterWebApiModels.Models.ClientSetting ToDomainClientSetting(this   NoMatterDatabaseModel.Setting setting)
		{
			return new NoMatterWebApiModels.Models.ClientSetting
			{
				SettingId = setting.SettingId,
				SettingName = setting.SettingName,
				SettingType = setting.SettingType,
				StringValue = setting.StringValue,
				IntValue = setting.IntValue

			};
		}

		public static NoMatterDatabaseModel.Setting ToDatabaseClientSetting(this  NoMatterWebApiModels.Models.ClientSetting clientSetting, int clientId)
		{
			return new NoMatterDatabaseModel.Setting
			{
				SettingId = clientSetting.SettingId,
				ClientId = clientId,
				SettingName = clientSetting.SettingName,
				StringValue = clientSetting.StringValue,
				IntValue = clientSetting.IntValue,
				SettingType = (byte) (clientSetting.StringValue != null ? 1 : 2)
			};
		}
	}
}