using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NoMatterDataLibrary.Extensions;
using NoMatterDatabaseModel;
using GlobalSetting = NoMatterWebApiModels.Models.GlobalSetting;
using Setting = NoMatterWebApiModels.Models.Setting;

namespace NoMatterDataLibrary
{

	public interface IGlobalRepository
	{
		Task<List<GlobalSetting>> GetGlobalSettingsAsync();
		Task<List<Setting>> GetSettingsAsync();
		Task<Setting> GetSettingAsync(short settingId);
		Task DeleteSettingAsync(short settingId);
		Task AddSettingAsync(Setting setting);
		Task UpdateSettingAsync(Setting setting);
		Task InsertClientEmailSendLog(ClientEmail clientEmail);
	}

	public class GlobalRepository : IGlobalRepository
	{


		public async Task<List<GlobalSetting>> GetGlobalSettingsAsync()
		{
			using (var mainDb = new DatabaseEntities())
			{
				var settingsDb = await mainDb.GlobalSettings.ToListAsync();

				return settingsDb.Select(x=>x.ToDomainGlobalSetting()).ToList();
			}
			
		}

		public async Task<List<Setting>> GetSettingsAsync()
		{
			using (var mainDb = new DatabaseEntities())
			{
				var settingsDb = await mainDb.Settings.ToListAsync();

				return settingsDb.Select(x => x.ToDomainSetting()).ToList();
			}
			
		}

		public async Task<Setting> GetSettingAsync(short settingId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var settingDb = await mainDb.Settings.Where(x => x.SettingId == settingId).FirstOrDefaultAsync();

				return settingDb.ToDomainSetting();
			}

		}

		public async Task AddSettingAsync(Setting setting)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var settingDb = setting.ToDatabaseSetting();

				mainDb.Settings.Add(settingDb);

				await mainDb.SaveChangesAsync();
			}
			
		}

		public async Task DeleteSettingAsync(short settingId)
		{
			using (var mainDb = new DatabaseEntities())
			{						
				var setting = await mainDb.Settings.Where(x => x.SettingId == settingId).SingleOrDefaultAsync();

				mainDb.Settings.Remove(setting);
				await mainDb.SaveChangesAsync();
			}

		}

		public async Task UpdateSettingAsync(Setting setting)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var settingDb = await mainDb.Settings.Where(x => x.SettingId == setting.SettingId).FirstOrDefaultAsync();

				settingDb.SettingName = setting.SettingName;
				settingDb.SettingDescription = setting.SettingDescription;
				settingDb.SettingCategoryId = setting.SettingCategoryId;
				settingDb.SettingTypeId = setting.SettingTypeId;

				await mainDb.SaveChangesAsync();
			}
			

		}

		public async Task InsertClientEmailSendLog(ClientEmail clientEmail)
		{
			using (var mainDb = new DatabaseEntities())
			{
				mainDb.ClientEmails.Add(clientEmail);

				await mainDb.SaveChangesAsync();
			}
		
		}

		
	}
}