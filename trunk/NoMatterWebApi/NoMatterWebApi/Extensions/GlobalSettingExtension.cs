using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Extensions
{
	public static class GlobalSettingExtension
	{
		public static NoMatterWebApiModels.Models.GlobalSetting ToDomainGlobalSetting(this   NoMatterDatabaseModel.GlobalSetting setting)
		{
			return new NoMatterWebApiModels.Models.GlobalSetting
			{
				SettingName = setting.SettingName,
				SettingType = setting.SettingType,
				StringValue = setting.StringValue,
				IntValue = setting.IntValue

			};
		}
	}
}