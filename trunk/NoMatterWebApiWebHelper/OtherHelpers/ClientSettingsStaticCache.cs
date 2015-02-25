using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApiWebHelper.OtherHelpers
{
	public class ClientSettingsStaticCache
	{
		private static List<ClientSetting> _clientSettings;

		static  ClientSettingsStaticCache()
		{
			if (_clientSettings == null) LoadClientSettingsCache();
		}


		public static void LoadClientSettingsCache()
		{
			var clientHelper = new ClientHelper();
			var globalSettings = new GlobalSettings();

			_clientSettings = clientHelper.GetClientSettings(globalSettings.DefaultClientId);
		}

		public static List<ClientSetting> GetClientSettings()
		{
			return _clientSettings;
		}


		public static string GetStringSetting(string settingName)
		{
			if (_clientSettings == null) _clientSettings = GetClientSettings();

			var setting = _clientSettings.SingleOrDefault(x => x.SettingName == settingName);

			return setting != null ? setting.StringValue : "";
		}

		public static int GetIntegerSetting(string settingName)
		{
			if (_clientSettings == null) _clientSettings = GetClientSettings();

			var setting = _clientSettings.SingleOrDefault(x => x.SettingName == settingName);

			if (setting != null)
			{
				if (setting.IntValue != null) return setting.IntValue.Value;
			}

			return 0;
		}

		public static bool GetBooleanSetting(string settingName)
		{
			var setting = _clientSettings.SingleOrDefault(x => x.SettingName == settingName);

			if (setting != null)
			{
				if (setting.IntValue != null) return Convert.ToBoolean(setting.IntValue.Value);
			}

			return false;
		}

	}
}
