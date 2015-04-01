using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NoMatterDatabaseModel;
using NoMatterWebApi.Helpers;

namespace NoMatterWebApi.DAL
{

	public interface IGlobalRepository
	{
		Task<List<GlobalSetting>> GetGlobalSettingsAsync();
		Task<List<Setting>> GetSettingsAsync();
		Task<Setting> GetSettingAsync(short settingId);
		Task DeleteSettingAsync(short settingId);
		Task AddSettingAsync(Setting setting);
		Task UpdateSettingAsync(Setting settingDb, NoMatterWebApiModels.Models.Setting setting);
	}

	public class GlobalRepository : IGlobalRepository
	{
		private DatabaseEntities databaseConnection;

		public GlobalRepository(DatabaseEntities databaseConnection)
		{
			this.databaseConnection = databaseConnection;
		}


		public async Task<List<GlobalSetting>> GetGlobalSettingsAsync()
		{
			var settings = await databaseConnection.GlobalSettings.ToListAsync();

			return settings;
		}

		public async Task<List<Setting>> GetSettingsAsync()
		{
			var settings = await databaseConnection.Settings.ToListAsync();

			return settings;
		}

		public async Task<Setting> GetSettingAsync(short settingId)
		{
			var setting = await databaseConnection.Settings.Where(x=>x.SettingId == settingId).FirstOrDefaultAsync();

			return setting;
		}

		public async Task AddSettingAsync(Setting setting)
		{
			databaseConnection.Settings.Add(setting);

			await databaseConnection.SaveChangesAsync();
		}

		public async Task DeleteSettingAsync(short settingId)
		{
			var setting = await databaseConnection.Settings.Where(x => x.SettingId == settingId).SingleOrDefaultAsync();

			databaseConnection.Settings.Remove(setting);
			await databaseConnection.SaveChangesAsync();

		}

		public async Task UpdateSettingAsync(Setting settingDb, NoMatterWebApiModels.Models.Setting setting)
		{

			databaseConnection.Settings.Attach(settingDb);

			settingDb.SettingName = setting.SettingName;
			settingDb.SettingDescription = setting.SettingDescription;
			settingDb.SettingCategoryId = setting.SettingCategoryId;
			settingDb.SettingTypeId = setting.SettingTypeId;

			await databaseConnection.SaveChangesAsync();

		}

		public void Save()
		{
			databaseConnection.SaveChanges();
		}

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					databaseConnection.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}