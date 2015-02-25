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