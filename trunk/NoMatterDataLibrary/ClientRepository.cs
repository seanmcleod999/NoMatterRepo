using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using NoMatterDataLibrary.Enums;
using NoMatterDataLibrary.Extensions;
using NoMatterDatabaseModel;
using Client = NoMatterWebApiModels.Models.Client;
using ClientSetting = NoMatterWebApiModels.Models.ClientSetting;
using ClientPaymentType = NoMatterWebApiModels.Models.ClientPaymentType;
using ClientDeliveryOption = NoMatterWebApiModels.Models.ClientDeliveryOption;
using ClientPage = NoMatterWebApiModels.Models.ClientPage;
using Supplier = NoMatterWebApiModels.Models.Supplier;


namespace NoMatterDataLibrary
{
	public interface IClientRepository
	{
		Task<string> AddClientAsync(Client client);
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
		Task UpdateClientPageAsync(ClientPage clientPage);
		Task<int> AddClientPageAsync(ClientPage clientPage);
		Task DeleteClientPageAsync(Guid clientUuid, string pageName);
		Task<int> AddClientSettingAsync(ClientSetting clientSetting);
		Task DeleteClientSettingAsync(Guid clientUuid, short settingId);
		Task UpdateClientSettingAsync(ClientSetting clientSetting);
		Task UpdateClientAsync(Client client);
		Task<int> AddClientDeliveryOptionAsync(ClientDeliveryOption clientDeliveryOption);
		Task UpdateClientDeliveryOptionAsync(ClientDeliveryOption clientDeliveryOption);
		Task DeleteClientDeliveryOptionAsync(Guid clientUuid, short clientDeliveryOptionId);
		Task AllocateMissingClientSettingsAsync(Client client, List<short> currentSettingIds);

		Task<string> GetClientStringSettingsAsync(Guid clientUuid, SettingEnum setting);
		Task<int> GetClientIntSettingsAsync(Guid clientUuid, SettingEnum settingName);

		Task<List<Supplier>> GetClientSuppliersAsync(Guid clientUuid);

		Task AddClientDefaultAdminUserAsync(Guid clientUuid, string domainName, byte[] password);
	}

	public class ClientRepository : IClientRepository
	{

		public async Task<string> AddClientAsync(Client client)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var clientDb = client.ToDatabaseClient();

				clientDb.ClientUUID = Guid.NewGuid();
				mainDb.Clients.Add(clientDb);
				await mainDb.SaveChangesAsync();

				return clientDb.ClientUUID.ToString();
			}
		}


		public async Task UpdateClientAsync(Client client)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var clientDb = await mainDb.Clients.Where(x => x.ClientUUID == new Guid(client.ClientUuid)).FirstOrDefaultAsync();

				clientDb.ClientName = client.ClientName;
				clientDb.SiteUrl = client.SiteUrl;
				clientDb.Enabled = client.Enabled;
				clientDb.Logo = client.Logo;

				await mainDb.SaveChangesAsync();
			}
		}

		public async Task<Client> GetClientAsync(int clientId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var clientDb = await mainDb.Clients.SingleOrDefaultAsync(x => x.ClientId == clientId);

				return clientDb.ToDomainClient();
			}
		}

		public async Task<Client> GetClientAsync(Guid clientUuid)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var clientDb = await mainDb.Clients.SingleOrDefaultAsync(x => x.ClientUUID == clientUuid);

				return clientDb.ToDomainClient();
			}
		}

		public async Task<List<Client>> GetClientsAsync()
		{
			using (var mainDb = new DatabaseEntities())
			{
				var clientsDb = await mainDb.Clients.ToListAsync();

				return clientsDb.Select(x => x.ToDomainClient()).ToList();
			}		
		}

		public async Task<List<ClientSetting>> GetClientSettingsAsync(Guid clientUuid)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var settingsDb = await mainDb.ClientSettings
				                        .Include(x => x.Setting)
				                        .Include(x => x.Setting.SettingType)
				                        .Include(x => x.Setting.SettingCategory)
				                        .Where(x => x.Client.ClientUUID == clientUuid).ToListAsync();

				return settingsDb.Select(x => x.ToDomainClientSetting()).ToList();
			}
		}

		public async Task<ClientSetting> GetClientSettingAsync(Guid clientUuid, int clientSettingId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var settingDb = await mainDb.ClientSettings
				                    .Include(x => x.Setting)
				                    .Include(x => x.Setting.SettingType)
				                    .Include(x => x.Setting.SettingCategory)
				                    .Where(x => x.Client.ClientUUID == clientUuid && x.ClientSettingId == clientSettingId)
				                    .FirstOrDefaultAsync();

				return settingDb.ToDomainClientSetting();
			}
		}

		public async Task<List<ClientPaymentType>> GetClientPaymentTypesAsync(Guid clientUuid)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var paymentTypes = await mainDb.ClientPaymentTypes.Include("PaymentType")
				.Where(x => x.Client.ClientUUID == clientUuid)
				.OrderBy(x => x.PaymentTypeOrder)
				.ToListAsync();

				return paymentTypes.Select(x=>x.ToDomainClientPaymentType()).ToList();
			}		
		}

		public async Task<List<ClientDeliveryOption>> GetClientDeliveryOptionsAsync(Guid clientUuid)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var deliveryOptions = await mainDb.ClientDeliveryOptions
				.Where(x => x.Client.ClientUUID == clientUuid && x.Enabled)
				.OrderBy(x => x.OptionOrder)
				.ToListAsync();

				return deliveryOptions.Select(x=>x.ToDomainClientDeliveryOption()).ToList();
			}		
		}

		public async Task<List<ClientPage>> GetClientPagesAsync(Guid clientUuid)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var clientPages = await mainDb.ClientPages
				                    .Where(x => x.Client.ClientUUID == clientUuid)
				                    .OrderBy(x => x.ClientPageId)
				                    .ToListAsync();

				return clientPages.Select(x=>x.ToDomainClientPage()).ToList();
			}
		}

		public async Task<ClientPage> GetClientPageAsync(Guid clientUuid, string pageName)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var clientPageDb = await mainDb.ClientPages
				.Where(x => x.Client.ClientUUID == clientUuid && x.PageName == pageName)
				.OrderBy(x => x.PageName)
				.FirstOrDefaultAsync();

				return clientPageDb.ToDomainClientPage();
			}			
		}

		public async Task<int> AddClientPageAsync(ClientPage clientPage)
		{
		
			using (var mainDb = new DatabaseEntities())
			{
				var clientDb  = 
					await mainDb.Clients.Where(x => x.ClientUUID == new Guid(clientPage.Client.ClientUuid)).FirstOrDefaultAsync();

				var clientPageDb = clientPage.ToDatabaseClientPage(clientDb.ClientId);

				mainDb.ClientPages.Add(clientPageDb);
				await mainDb.SaveChangesAsync();

				return clientPageDb.ClientPageId;

			}
		}

		public async Task<int> AddClientSettingAsync(ClientSetting clientSetting)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var clientDb = await mainDb.Clients.Where(x => x.ClientUUID == new Guid(clientSetting.Client.ClientUuid)).FirstOrDefaultAsync();

				var clientSettingDb = clientSetting.ToDatabaseClientSetting(clientDb.ClientId);

				mainDb.ClientSettings.Add(clientSettingDb);
				await mainDb.SaveChangesAsync();

				return clientSettingDb.SettingId;
			}
			
		}

		public async Task UpdateClientPageAsync(ClientPage clientPage)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var clientPageDb = await mainDb.ClientPages.Where(x => x.ClientPageId == clientPage.ClientPageId).FirstOrDefaultAsync();

				clientPageDb.PageName = clientPage.PageName;
				clientPageDb.PageText = clientPage.PageText;

				await mainDb.SaveChangesAsync();
			}
		}

		public async Task UpdateClientSettingAsync(ClientSetting clientSetting)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var clientSettingDb = await mainDb.ClientSettings.Where(x => x.ClientSettingId == clientSetting.ClientSettingId).FirstOrDefaultAsync();

				clientSettingDb.StringValue = clientSetting.StringValue;
				clientSettingDb.IntValue = clientSetting.IntValue;

				await mainDb.SaveChangesAsync();
			}
			
		}

		public async Task<ClientDeliveryOption> GetClientDeliveryOptionAsync(short clientDeliveryOptionId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var deliveryOption = await mainDb.ClientDeliveryOptions.Where(x => x.ClientDeliveryOptionId == clientDeliveryOptionId).SingleOrDefaultAsync();

				return deliveryOption.ToDomainClientDeliveryOption();
			}

		}

		public async Task DeleteClientPageAsync(Guid clientUuid, string pageName)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var clientPage = await mainDb.ClientPages.Where(x => x.Client.ClientUUID == clientUuid && x.PageName == pageName).FirstOrDefaultAsync();

				mainDb.ClientPages.Remove(clientPage);
				await mainDb.SaveChangesAsync();
			}
		}

		public async Task DeleteClientSettingAsync(Guid clientUuid, short settingId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var clientsetting = await mainDb.ClientSettings.Where(x => x.Client.ClientUUID == clientUuid && x.SettingId == settingId).FirstOrDefaultAsync();

				mainDb.ClientSettings.Remove(clientsetting);
				await mainDb.SaveChangesAsync();
			}
		}

		public async Task<int> AddClientDeliveryOptionAsync(ClientDeliveryOption clientDeliveryOption)
		{
			

			using (var mainDb = new DatabaseEntities())
			{

				var clientDb = await mainDb.Clients.Where(x => x.ClientUUID == new Guid(clientDeliveryOption.Client.ClientUuid)).FirstOrDefaultAsync();

				var clientDeliveryOptionDb = clientDeliveryOption.ToDatabaseClientDeliveryOption(clientDb.ClientId);

				mainDb.ClientDeliveryOptions.Add(clientDeliveryOptionDb);
				await mainDb.SaveChangesAsync();

				return clientDeliveryOption.ClientDeliveryOptionId;
			}

		}

		public async Task UpdateClientDeliveryOptionAsync(ClientDeliveryOption clientDeliveryOption)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var clientDeliveryOptionDb = await mainDb.ClientDeliveryOptions.Where(x => x.ClientDeliveryOptionId == clientDeliveryOption.ClientDeliveryOptionId).FirstOrDefaultAsync();

				clientDeliveryOptionDb.Description = clientDeliveryOption.Description;
				clientDeliveryOptionDb.DeliveryAmount = clientDeliveryOption.DeliveryAmount;
				clientDeliveryOptionDb.OptionOrder = clientDeliveryOption.OptionOrder;
				clientDeliveryOptionDb.Enabled = clientDeliveryOption.Enabled;

				await mainDb.SaveChangesAsync();
			}
			
		}

		public async Task DeleteClientDeliveryOptionAsync(Guid clientUuid, short clientDeliveryOptionId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var clientDeliveryOption = await mainDb.ClientDeliveryOptions.Where(x => x.Client.ClientUUID == clientUuid && x.ClientDeliveryOptionId == clientDeliveryOptionId).FirstOrDefaultAsync();

				mainDb.ClientDeliveryOptions.Remove(clientDeliveryOption);
				await mainDb.SaveChangesAsync();
			}
			

		}

		public async Task AllocateMissingClientSettingsAsync(Client client, List<short> currentSettingIds)
		{

			using (var mainDb = new DatabaseEntities())
			{
				var clientDb = await mainDb.Clients.Where(x => x.ClientUUID == new Guid(client.ClientUuid)).FirstOrDefaultAsync();

				var missingSettings = await (from f in mainDb.Settings
											 where !currentSettingIds.Contains(f.SettingId)
											 select f).ToListAsync();

				foreach (var setting in missingSettings)
				{
					var clientSetting = mainDb.ClientSettings.Create();

					clientSetting.ClientId = clientDb.ClientId;
					clientSetting.SettingId = setting.SettingId;

					mainDb.ClientSettings.Add(clientSetting);
				}

				await mainDb.SaveChangesAsync();
			}

		}

		public async Task<string> GetClientStringSettingsAsync(Guid clientUuid, SettingEnum setting)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var clientSetting = await mainDb.ClientSettings.Where(x => x.Client.ClientUUID == clientUuid && x.SettingId == (short)setting)
								  .FirstOrDefaultAsync();

				return clientSetting == null ? "" : clientSetting.StringValue;
			}
			
		}

		public async Task<int> GetClientIntSettingsAsync(Guid clientUuid, SettingEnum setting)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var clientSetting = await mainDb.ClientSettings.Where(x => x.Client.ClientUUID == clientUuid && x.SettingId == (short)setting)
								  .FirstOrDefaultAsync();

				if (clientSetting == null) return 0;

				if (clientSetting.IntValue == null) return 0;

				return clientSetting.IntValue.Value;
			}
			
		}

		public async Task AddClientDefaultAdminUserAsync(Guid clientUuid, string domainName, byte[] password)
		{ 
			using (var mainDb = new DatabaseEntities())
			{
				var client = mainDb.Clients.FirstOrDefault(x => x.ClientUUID == clientUuid);

				var email = "admin@" + domainName;

				var user = mainDb.Users.Create();

				user.Client = client;
				user.UserUUID = Guid.NewGuid();
				user.Identifier = email;
				user.Email = email;
				user.CredentialTypeId = 1;
				user.FullName = "Administrator";
				user.DateAdded = DateTime.Now;
				user.DateUpdated = DateTime.Now;
				user.Password = password;

				user.UserRoles = new List<UserRole> {new UserRole() {RoleId = 1}};

				mainDb.Users.Add(user);

				await mainDb.SaveChangesAsync();

				//var userRole = mainDb.UserRoles.Create();

				//userRole.User = user;
				//userRole.RoleId = 1; //admin role

				//mainDb.UserRoles.Add(userRole);

			}

		}

		public async Task<List<Supplier>> GetClientSuppliersAsync(Guid clientUuid)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var suppliers = await mainDb.Suppliers.Where(x => x.Client.ClientUUID == clientUuid).OrderBy(x=>x.Name).ToListAsync();

				return suppliers.Select(x => x.ToDomainSupplier()).ToList();
			}
		}
	}
}
