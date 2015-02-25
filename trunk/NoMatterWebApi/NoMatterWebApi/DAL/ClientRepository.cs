using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using NoMatterDatabaseModel;

namespace NoMatterWebApi.DAL
{
	public interface IClientRepository
	{
		Task<Client> GetClientAsync(int clientId);
		Task<Client> GetClientAsync(Guid clientUuid);
		Task<List<Client>> GetClientsAsync();
		Task<List<Section>> GetClientSectionsAsync(Guid clientUuid);
		Task<List<Setting>> GetClientSettingsAsync(Guid clientUuid);
	}

	public class ClientRepository : IClientRepository
	{
		private DatabaseEntities databaseConnection;

		public ClientRepository(DatabaseEntities databaseConnection)
		{
			this.databaseConnection = databaseConnection;
		}

		public async Task<Client> GetClientAsync(int clientId)
		{
			var client = await databaseConnection.Clients.SingleOrDefaultAsync(x => x.ClientId == clientId);

			return client;
		}

		public async Task<Client> GetClientAsync(Guid clientUuid)
		{
			var client = await databaseConnection.Clients.SingleOrDefaultAsync(x => x.ClientUUID == clientUuid);

			return client;
		}

		public async Task<List<Client>> GetClientsAsync()
		{
			var clients = await databaseConnection.Clients.Where(x=>x.Enabled).ToListAsync();

			return clients;
		}

		public async Task<List<Section>> GetClientSectionsAsync(Guid clientUuid)
		{
			var sections = await databaseConnection.Sections.Where(x => x.Client.ClientUUID == clientUuid).ToListAsync();

			return sections;
		}

		public async Task<List<Setting>> GetClientSettingsAsync(Guid clientUuid)
		{
			var settings = await databaseConnection.Settings.Where(x => x.Client.ClientUUID == clientUuid).ToListAsync();

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
