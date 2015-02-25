using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApiWebHelper.OtherHelpers
{
	public class GlobalSettingsStaticCache
	{
		private static List<GlobalSetting> _globalSettings;

		static  GlobalSettingsStaticCache()
		{
			if (_globalSettings == null) LoadGlobalSettingsCache();
		}


		public static void LoadGlobalSettingsCache()
		{
			var globalHelper = new GlobalHelper();

			_globalSettings = globalHelper.GetGlobalSettings();
		}

		public static List<GlobalSetting> GetGlobalSettings()
		{
			return _globalSettings;
		}


		public static string GetStringSetting(string settingName)
		{
			if (_globalSettings == null) _globalSettings = GetGlobalSettings();

			var setting = _globalSettings.SingleOrDefault(x => x.SettingName == settingName);

			return setting != null ? setting.StringValue : "";
		}

		public static int GetIntegerSetting(string settingName)
		{
			if (_globalSettings == null) _globalSettings = GetGlobalSettings();

			var setting = _globalSettings.SingleOrDefault(x => x.SettingName == settingName);

			if (setting != null)
			{
				if (setting.IntValue != null) return setting.IntValue.Value;
			}

			return 0;
		}

		public static bool GetBooleanSetting(string settingName)
		{
			var setting = _globalSettings.SingleOrDefault(x => x.SettingName == settingName);

			if (setting != null)
			{
				if (setting.IntValue != null) return Convert.ToBoolean(setting.IntValue.Value);
			}

			return false;
		}

	}
}
