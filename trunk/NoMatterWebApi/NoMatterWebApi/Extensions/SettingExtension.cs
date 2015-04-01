using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterDatabaseModel;

namespace NoMatterWebApi.Extensions
{
	public static class SettingExtension
	{
		public static NoMatterWebApiModels.Models.Setting ToDomainSetting(this   NoMatterDatabaseModel.Setting setting)
		{
			return new NoMatterWebApiModels.Models.Setting
			{
				SettingId = setting.SettingId,
				SettingName = setting.SettingName,
				SettingDescription = setting.SettingDescription,
				//RegexValidation = setting.RegexValidation,
				SettingTypeId = setting.SettingType.SettingTypeId,
				SettingType = setting.SettingType.Type,
				SettingCategoryId = setting.SettingCategory.SettingCategoryId,
				SettingCategory = setting.SettingCategory.Category

			};
		}

		//public static NoMatterDatabaseModel.Setting ToDatabaseSetting(this  NoMatterWebApiModels.Models.Setting setting, int clientId)
		//{
		//	return new NoMatterDatabaseModel.Setting
		//	{
		//		SettingId = setting.SettingId,
		//		ClientId = clientId,
		//		SettingName = clientSetting.SettingName,
		//		StringValue = clientSetting.StringValue,
		//		IntValue = clientSetting.IntValue,
		//		SettingType = (byte) (clientSetting.StringValue != null ? 1 : 2)
		//	};
		//}
	}
}