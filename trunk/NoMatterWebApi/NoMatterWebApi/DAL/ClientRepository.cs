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
		Task<List<ClientSetting>> GetClientSettingsAsync(Guid clientUuid);
		Task<ClientSetting> GetClientSettingAsync(Guid clientUuid, int clientSettingId);
		Task<List<ClientPaymentType>> GetClientPaymentTypesAsync(Guid clientUuid);
		Task<List<ClientDeliveryOption>> GetClientDeliveryOptionsAsync(Guid clientUuid);
		Task<ClientDeliveryOption> GetClientDeliveryOptionAsync(short clientDeliveryOptionId);
		Task<List<ClientPage>> GetClientPagesAsync(Guid clientUuid);
		Task<ClientPage> GetClientPageAsync(Guid clientUuid, string pageName);
		Task UpdateClientPageAsync(ClientPage clientPageDb, NoMatterWebApiModels.Models.ClientPage clientPage);
		Task<int> AddClientPageAsync(ClientPage clientPage);
		Task DeleteClientPageAsync(Guid clientUuid, string pageName);
		Task<int> AddClientSettingAsync(ClientSetting clientSetting);
		Task DeleteClientSettingAsync(Guid clientUuid, short settingId);
		Task UpdateClientSettingAsync(ClientSetting clientSettingDb, NoMatterWebApiModels.Models.ClientSetting clientSetting);
		Task UpdateClientAsync(Client clientDb, NoMatterWebApiModels.Models.Client client);
		Task<int> AddClientDeliveryOptionAsync(ClientDeliveryOption clientDeliveryOption);
		Task UpdateClientDeliveryOptionAsync(ClientDeliveryOption clientDeliveryOptionDb, NoMatterWebApiModels.Models.ClientDeliveryOption clientDeliveryOption);
		Task DeleteClientDeliveryOptionAsync(Guid clientUuid, short clientDeliveryOptionId);
		Task AllocateMissingClientSettingsAsync(Client client, List<short> currentSettingIds);
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

		public async Task UpdateClientAsync(Client clientDb, NoMatterWebApiModels.Models.Client client)
		{

			databaseConnection.Clients.Attach(clientDb);

			clientDb.ClientName = client.ClientName;
			clientDb.SiteUrl = client.SiteUrl;
			clientDb.Enabled = client.Enabled;
			//clientDb.Logo = client.Logo;

			await databaseConnection.SaveChangesAsync();

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
			var clients = await databaseConnection.Clients.ToListAsync();

			return clients;
		}

		public async Task<List<ClientSetting>> GetClientSettingsAsync(Guid clientUuid)
		{
			var settings = await databaseConnection.ClientSettings
				.Include(x => x.Setting)
				.Include(x => x.Setting.SettingType)
				.Include(x => x.Setting.SettingCategory)
				.Where(x => x.Client.ClientUUID == clientUuid).ToListAsync();

			return settings;
		}

		public async Task<ClientSetting> GetClientSettingAsync(Guid clientUuid, int clientSettingId)
		{
			var setting = await databaseConnection.ClientSettings
				.Include(x=>x.Setting)
				.Include(x=>x.Setting.SettingType)
				.Include(x=>x.Setting.SettingCategory)
				.Where(x => x.Client.ClientUUID == clientUuid && x.ClientSettingId == clientSettingId).FirstOrDefaultAsync();

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

		public async Task<int> AddClientSettingAsync(ClientSetting clientSetting)
		{
			databaseConnection.ClientSettings.Add(clientSetting);
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

		public async Task UpdateClientSettingAsync(ClientSetting clientSettingDb, NoMatterWebApiModels.Models.ClientSetting clientSetting)
		{
			databaseConnection.ClientSettings.Attach(clientSettingDb);

			//clientSettingDb.SettingName = clientSetting.SettingName;
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
			var clientsetting = await databaseConnection.ClientSettings.Where(x => x.Client.ClientUUID == clientUuid && x.SettingId == settingId).FirstOrDefaultAsync();

			databaseConnection.ClientSettings.Remove(clientsetting);
			await databaseConnection.SaveChangesAsync();

		}

		public async Task<int> AddClientDeliveryOptionAsync(ClientDeliveryOption clientDeliveryOption)
		{
			databaseConnection.ClientDeliveryOptions.Add(clientDeliveryOption);
			await databaseConnection.SaveChangesAsync();

			return clientDeliveryOption.ClientDeliveryOptionId;
		}

		public async Task UpdateClientDeliveryOptionAsync(ClientDeliveryOption clientDeliveryOptionDb, NoMatterWebApiModels.Models.ClientDeliveryOption clientDeliveryOption)
		{
			databaseConnection.ClientDeliveryOptions.Attach(clientDeliveryOptionDb);

			clientDeliveryOptionDb.Description = clientDeliveryOption.Description;
			clientDeliveryOptionDb.DeliveryAmount = clientDeliveryOption.DeliveryAmount;
			clientDeliveryOptionDb.OptionOrder = clientDeliveryOption.OptionOrder;
			clientDeliveryOptionDb.Enabled = clientDeliveryOption.Enabled;

			await databaseConnection.SaveChangesAsync();
		}

		public async Task DeleteClientDeliveryOptionAsync(Guid clientUuid, short clientDeliveryOptionId)
		{
			var clientDeliveryOption = await databaseConnection.ClientDeliveryOptions.Where(x => x.Client.ClientUUID == clientUuid && x.ClientDeliveryOptionId == clientDeliveryOptionId).FirstOrDefaultAsync();

			databaseConnection.ClientDeliveryOptions.Remove(clientDeliveryOption);
			await databaseConnection.SaveChangesAsync();

		}

		public async Task AllocateMissingClientSettingsAsync(Client client, List<short> currentSettingIds)
		{
			var missingSettings = await (from f in databaseConnection.Settings
			                       where !currentSettingIds.Contains(f.SettingId)
			                       select f).ToListAsync();

			foreach (var setting in missingSettings)
			{
				var clientSetting = databaseConnection.ClientSettings.Create();

				clientSetting.Client = client;
				clientSetting.SettingId = setting.SettingId;

				databaseConnection.ClientSettings.Add(clientSetting);
			}

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
