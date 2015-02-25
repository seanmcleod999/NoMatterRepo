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
				SettingName = setting.SettingName,
				SettingType = setting.SettingType,
				StringValue = setting.StringValue,
				IntValue = setting.IntValue

			};
		}
	}
}