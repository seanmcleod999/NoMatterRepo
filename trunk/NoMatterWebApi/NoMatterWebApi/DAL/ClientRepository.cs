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
		Task<int> AddClientAsync(Client client);
		Task<Client> GetClientAsync(int clientId);
		Task<Client> GetClientAsync(Guid clientUuid);
		Task<List<Client>> GetClientsAsync();
		Task<List<Setting>> GetClientSettingsAsync(Guid clientUuid);
		Task<Setting> GetClientSettingAsync(Guid clientUuid, short settingId);
		Task<List<ClientPaymentType>> GetClientPaymentTypesAsync(Guid clientUuid);
		Task<List<ClientDeliveryOption>> GetClientDeliveryOptionsAsync(Guid clientUuid);
		Task<ClientDeliveryOption> GetClientDeliveryOptionAsync(short clientDeliveryOptionId);
		Task<List<ClientPage>> GetClientPagesAsync(Guid clientUuid);
		Task<ClientPage> GetClientPageAsync(Guid clientUuid, string pageName);
		Task UpdateClientPageAsync(ClientPage clientPageDb, NoMatterWebApiModels.Models.ClientPage clientPage);
		Task<int> AddClientPageAsync(ClientPage clientPage);
		Task DeleteClientPageAsync(Guid clientUuid, string pageName);
		Task<int> AddClientSettingAsync(Setting clientSetting);
		Task DeleteClientSettingAsync(Guid clientUuid, short settingId);
		Task UpdateClientSettingAsync(Setting clientSettingDb, NoMatterWebApiModels.Models.ClientSetting clientSetting);
	}

	public class ClientRepository : IClientRepository
	{
		private DatabaseEntities databaseConnection;

		public ClientRepository(DatabaseEntities databaseConnection)
		{
			this.databaseConnection = databaseConnection;
		}

		public async Task<int> AddClientAsync(Client client)
		{
			client.ClientUUID = Guid.NewGuid();
			databaseConnection.Clients.Add(client);
			await databaseConnection.SaveChangesAsync();

			return client.ClientId;
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

		public async Task<List<Setting>> GetClientSettingsAsync(Guid clientUuid)
		{
			var settings = await databaseConnection.Settings.Where(x => x.Client.ClientUUID == clientUuid).ToListAsync();

			return settings;
		}

		public async Task<Setting> GetClientSettingAsync(Guid clientUuid, short settingId)
		{
			var setting = await databaseConnection.Settings.Where(x => x.Client.ClientUUID == clientUuid && x.SettingId == settingId).FirstOrDefaultAsync();

			return setting;
		}

		public async Task<List<ClientPaymentType>> GetClientPaymentTypesAsync(Guid clientUuid)
		{
			var paymentTypes = await databaseConnection.ClientPaymentTypes.Include("PaymentType")
				.Where(x => x.Client.ClientUUID == clientUuid)
				.OrderBy(x=>x.PaymentTypeOrder)
				.ToListAsync();

			return paymentTypes;
		}

		public async Task<List<ClientDeliveryOption>> GetClientDeliveryOptionsAsync(Guid clientUuid)
		{
			var deliveryOptions = await databaseConnection.ClientDeliveryOptions
				.Where(x => x.Client.ClientUUID == clientUuid && x.Enabled)
				.OrderBy(x=>x.OptionOrder)
				.ToListAsync();

			return deliveryOptions;
		}

		public async Task<List<ClientPage>> GetClientPagesAsync(Guid clientUuid)
		{
			var clientPages = await databaseConnection.ClientPages
				.Where(x => x.Client.ClientUUID == clientUuid)
				.OrderBy(x => x.ClientPageId)
				.ToListAsync();

			return clientPages;
		}

		public async Task<ClientPage> GetClientPageAsync(Guid clientUuid, string pageName)
		{
			var clientPage = await databaseConnection.ClientPages
			    .Where(
				    x =>
					x.Client.ClientUUID == clientUuid && x.PageName == pageName)
				.OrderBy(x => x.PageName)
			    .FirstOrDefaultAsync();

			return clientPage;
		}

		public async Task<int> AddClientPageAsync(ClientPage clientPage)
		{
			databaseConnection.ClientPages.Add(clientPage);
			await databaseConnection.SaveChangesAsync();

			return clientPage.ClientPageId;
		}

		public async Task<int> AddClientSettingAsync(Setting clientSetting)
		{
			databaseConnection.Settings.Add(clientSetting);
			await databaseConnection.SaveChangesAsync();

			return clientSetting.SettingId;
		}

		public async Task UpdateClientPageAsync(ClientPage clientPageDb, NoMatterWebApiModels.Models.ClientPage clientPage)
		{
			databaseConnection.ClientPages.Attach(clientPageDb);

			clientPageDb.PageName = clientPage.PageName;
			clientPageDb.PageText = clientPage.PageText;

			await databaseConnection.SaveChangesAsync();
		}

		public async Task UpdateClientSettingAsync(Setting clientSettingDb, NoMatterWebApiModels.Models.ClientSetting clientSetting)
		{
			databaseConnection.Settings.Attach(clientSettingDb);

			clientSettingDb.SettingName = clientSetting.SettingName;
			clientSettingDb.StringValue = clientSetting.StringValue;
			clientSettingDb.IntValue = clientSetting.IntValue;

			await databaseConnection.SaveChangesAsync();
		}

		public async Task<ClientDeliveryOption> GetClientDeliveryOptionAsync(short clientDeliveryOptionId)
		{
			var deliveryOption = await databaseConnection.ClientDeliveryOptions.Where(x => x.ClientDeliveryOptionId == clientDeliveryOptionId).SingleOrDefaultAsync();
				
			return deliveryOption;
		}

		public async Task DeleteClientPageAsync(Guid clientUuid, string pageName)
		{
			var clientPage = await databaseConnection.ClientPages.Where(x => x.Client.ClientUUID == clientUuid && x.PageName == pageName).FirstOrDefaultAsync();

			databaseConnection.ClientPages.Remove(clientPage);
			await databaseConnection.SaveChangesAsync();

		}

		public async Task DeleteClientSettingAsync(Guid clientUuid, short settingId)
		{
			var clientsetting = await databaseConnection.Settings.Where(x => x.Client.ClientUUID == clientUuid && x.SettingId == settingId).FirstOrDefaultAsync();

			databaseConnection.Settings.Remove(clientsetting);
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
