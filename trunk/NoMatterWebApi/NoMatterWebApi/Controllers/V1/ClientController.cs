using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NoMatterDatabaseModel;
using NoMatterWebApi.ActionResults;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Extensions;
using NoMatterWebApi.Logging;
using NoMatterWebApiModels.Models;
using Client = NoMatterWebApiModels.Models.Client;
using ClientDeliveryOption = NoMatterWebApiModels.Models.ClientDeliveryOption;
using ClientPage = NoMatterWebApiModels.Models.ClientPage;
using ClientPaymentType = NoMatterWebApiModels.Models.ClientPaymentType;
using Section = NoMatterWebApiModels.Models.Section;


namespace NoMatterWebApi.Controllers.V1
{
	//[Authorize]
	//[RoutePrefix("api/v1/clients")]
	public class ClientController : ApiController
	{
		private IClientRepository _clientRepository;
		private IUserRepository _userRepository;
		private ISectionRepository _sectionRepository;
		private ICategoryRepository _categoryRepository;

		public ClientController()
		{
			var databaseEntity = new DatabaseEntities();

			_clientRepository = new ClientRepository(databaseEntity);
			_userRepository = new UserRepository(databaseEntity);
			_sectionRepository = new SectionRepository(databaseEntity);
			_categoryRepository = new CategoryRepository(databaseEntity);
		}

		public ClientController(IClientRepository clientRepository, IUserRepository userRepository, SectionRepository sectionRepository, CategoryRepository categoryRepository)
		{
			_clientRepository = clientRepository;
			_userRepository = userRepository;
			_sectionRepository = sectionRepository;
			_categoryRepository = categoryRepository;
		}

		// POST api/v1/client
		[HttpPost]
		[Authorize]
		[Route("api/v1/clients")]
		public async Task<IHttpActionResult> AddClientAsync(Client client)
		{
			//TODO: make sure the user can add this page

			try
			{
				var userToken = User.Identity.Name;

				var userDb = await _userRepository.GetClientUserByTokenAsync(userToken);
				if (userDb == null) return new CustomBadRequest(Request, ApiResultCode.UserNotFound);

				NoMatterDatabaseModel.Client clientDb = client.ToDatabaseClient();

				//Save the client
				await _clientRepository.AddClientAsync(clientDb);
				return Ok();

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// GET api/v1/clients
		[Route("api/v1/clients")]
		[ResponseType(typeof(List<Client>))]
		public async Task<IHttpActionResult> GetClientsAsync()
		{
			try
			{
				var clientsDb = await _clientRepository.GetClientsAsync();

				var clients = clientsDb.Select(x => x.ToDomainClient()).ToList();

				return Ok(clients);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
			
		}

		// GET api/v1/clients/{clientId}
		[Route("api/v1/clients/{clientId}")]
		[ResponseType(typeof(List<Client>))]
		public async Task<IHttpActionResult> GetClientAsync(string clientId)
		{
			try
			{
				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));

				if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				var client = clientDb.ToDomainClient();

				return Ok(client);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}

		}

		// POST api/v1/clients/{clientId}/sections
		

		

		// GET api/v1/client/5/settingd
		[Route("api/v1/clients/{clientUuid}/settings")]
		[ResponseType(typeof(ClientSetting))]
		public async Task<IHttpActionResult> GetClientSettingsAsync(string clientUuid)
		{
			try
			{
				var settingsDb = await _clientRepository.GetClientSettingsAsync(new Guid(clientUuid));

				var settings = settingsDb.Select(x => x.ToDomainClientSetting()).ToList();

				return Ok(settings);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
			
		}

		// GET api/v1/client/5/settings/123
		[Route("api/v1/clients/{clientId}/setting/{settingId}")]
		[ResponseType(typeof(List<ClientPage>))]
		public async Task<IHttpActionResult> GetClientSettingAsync(string clientId, short settingId)
		{
			try
			{
				var clientSettingDb = await _clientRepository.GetClientSettingAsync(new Guid(clientId), settingId);

				var clientSetting = clientSettingDb.ToDomainClientSetting();

				return Ok(clientSetting);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// POST api/v1/client/5/settings
		[HttpPost]
		[Authorize]
		[Route("api/v1/clients/{clientId}/settings")]
		public async Task<IHttpActionResult> AddClientSettingAsync(string clientId, ClientSetting clientSetting)
		{
			//TODO: make sure the user can add this page

			try
			{
				var userToken = User.Identity.Name;

				var userDb = await _userRepository.GetClientUserByTokenAsync(userToken);
				if (userDb == null) return new CustomBadRequest(Request, ApiResultCode.UserNotFound);

				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
				if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				if (userDb.Client != null && userDb.ClientId != clientDb.ClientId) return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				NoMatterDatabaseModel.Setting clienSettingDb = clientSetting.ToDatabaseClientSetting(clientDb.ClientId);

				//Save the section
				await _clientRepository.AddClientSettingAsync(clienSettingDb);
				return Ok();

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// PUT api/v1/client/5/pages/123
		[HttpPut]
		[Authorize]
		[Route("api/v1/clients/{clientId}/settings/{settingId}")]
		public async Task<IHttpActionResult> UpdateClientSettingAsync(string clientId, short settingId, ClientSetting clientSetting)
		{
			//TODO: make sure the user can update this page

			try
			{
				var userToken = User.Identity.Name;

				var userDb = await _userRepository.GetClientUserByTokenAsync(userToken);
				if (userDb == null) return new CustomBadRequest(Request, ApiResultCode.UserNotFound);

				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
				if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				var clientSettingDb = await _clientRepository.GetClientSettingAsync(new Guid(clientId), settingId);
				if (clientSettingDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientSettingNotFound);

				if (userDb.Client != null && userDb.ClientId != clientDb.ClientId) return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				//Update the page
				await _clientRepository.UpdateClientSettingAsync(clientSettingDb, clientSetting);

				return Ok();

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// DELETE api/v1/clients/{clientId}/settings/{settingId}
		[HttpDelete]
		[Route("api/v1/clients/{clientId}/settings/{settingId}")]
		public async Task<IHttpActionResult> DeleteClientSettingAsync(string clientId, short settingId)
		{
			try
			{
				await _clientRepository.DeleteClientSettingAsync(new Guid(clientId), settingId);

				return Ok();
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// GET api/v1/client/5/payment-types
		[Route("api/v1/clients/{clientUuid}/delivery-options")]
		[ResponseType(typeof(List<ClientDeliveryOption>))]
		public async Task<IHttpActionResult> GetClientDeliveryOptions(string clientUuid)
		{
			try
			{
				var clientDeliveryOptionsDb = await _clientRepository.GetClientDeliveryOptionsAsync(new Guid(clientUuid));

				var clientDeliveryOptions = clientDeliveryOptionsDb.Select(x => x.ToDomainClientDeliveryOption()).ToList();

				if (clientDeliveryOptions.Any())
				{
					clientDeliveryOptions.First().Selected = true;
				}
				
				return Ok(clientDeliveryOptions);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// GET api/v1/client/5/payment-types
		[Route("api/v1/clients/{clientUuid}/payment-types")]
		[ResponseType(typeof(List<ClientPaymentType>))]
		public async Task<IHttpActionResult> GetClientPaymentTypes(string clientUuid)
		{
			try
			{
				var clientPaymentTypesDb = await _clientRepository.GetClientPaymentTypesAsync(new Guid(clientUuid));

				var clientPaymentTypes = clientPaymentTypesDb.Select(x => x.ToDomainClientPaymentType()).ToList();

				if (clientPaymentTypes.Any())
				{
					clientPaymentTypes.First().Selected = true;
				}
				
				return Ok(clientPaymentTypes);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// GET api/v1/client/5/payment-types
		[Route("api/v1/clients/{clientId}/pages")]
		[ResponseType(typeof(List<ClientPage>))]
		public async Task<IHttpActionResult> GetClientPagesAsync(string clientId)
		{
			try
			{
				var clientPagesDb = await _clientRepository.GetClientPagesAsync(new Guid(clientId));

				var clientPages = clientPagesDb.Select(x => x.ToDomainClientPage()).ToList();

				return Ok(clientPages);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// GET api/v1/client/5/pages/123
		[Route("api/v1/clients/{clientId}/pages/{pageName}")]
		[ResponseType(typeof(List<ClientPage>))]
		public async Task<IHttpActionResult> GetClientPageAsync(string clientId, string pageName)
		{
			try
			{
				var clientPageDb = await _clientRepository.GetClientPageAsync(new Guid(clientId), pageName);

				var clientPage = clientPageDb.ToDomainClientPage();

				return Ok(clientPage);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// POST api/v1/client/5/pages
		[HttpPost]
		[Authorize]
		[Route("api/v1/clients/{clientId}/pages")]
		public async Task<IHttpActionResult> AddClientPageAsync(string clientId, ClientPage clientPage)
		{
			//TODO: make sure the user can add this page

			try
			{
				var userToken = User.Identity.Name;

				var userDb = await _userRepository.GetClientUserByTokenAsync(userToken);
				if (userDb == null) return new CustomBadRequest(Request, ApiResultCode.UserNotFound);

				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
				if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				if (userDb.Client != null && userDb.ClientId != clientDb.ClientId) return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				NoMatterDatabaseModel.ClientPage clientPageDb = clientPage.ToDatabaseClientPage(clientDb.ClientId);

				//Save the section
				await _clientRepository.AddClientPageAsync(clientPageDb);
				return Ok();

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// PUT api/v1/client/5/pages/123
		[HttpPut]
		[Authorize]
		[Route("api/v1/clients/{clientId}/pages/{pageName}")]
		public async Task<IHttpActionResult> UpdateClientPageAsync(string clientId, string pageName, ClientPage clientPage)
		{
			//TODO: make sure the user can update this page

			try
			{
				var userToken = User.Identity.Name;

				var userDb = await _userRepository.GetClientUserByTokenAsync(userToken);
				if (userDb == null) return new CustomBadRequest(Request, ApiResultCode.UserNotFound);

				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
				if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				var clientPageDb = await _clientRepository.GetClientPageAsync(new Guid(clientId), pageName);
				if (clientPageDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientPageNotFound);
				
				if (userDb.Client != null && userDb.ClientId != clientDb.ClientId) return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				//Update the page
				await _clientRepository.UpdateClientPageAsync(clientPageDb, clientPage);

				return Ok();

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// DELETE api/v1/clients/{clientId}/pages/{pageName}
		[HttpDelete]
		[Route("api/v1/clients/{clientId}/pages/{pageName}")]
		public async Task<IHttpActionResult> DeleteClientPageAsync(string clientId, string pageName)
		{
			try
			{
				await _clientRepository.DeleteClientPageAsync(new Guid(clientId), pageName);

				return Ok();
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// GET api/v1/clients/{clientUuid}/sections
		[HttpGet]
		[Route("api/v1/clients/{clientId}/sections")]
		[ResponseType(typeof(List<Section>))]
		public async Task<IHttpActionResult> GetClientSections(string clientId, bool includeEmpty = false, bool includeHidden = false)
		{
			try
			{

				var sectionsDb = await _sectionRepository.GetClientSectionsAsync(new Guid(clientId), includeHidden);

				var sections = sectionsDb.Select(x => x.ToDomainSection()).ToList();

				if (!includeEmpty)
				{
					sections = sections.Where(x => x.VisibleCategoryCount > 0).ToList();
				}

				return Ok(sections);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		[Authorize]
		[HttpPost]
		[Route("api/v1/clients/{clientId}/sections")]
		public async Task<IHttpActionResult> AddClientSection(string clientId, Section section)
		{
			//TODO: make sure the user can post to this category

			try
			{
				var userToken = User.Identity.Name;

				var user = await _userRepository.GetClientUserByTokenAsync(userToken);

				var client = await _clientRepository.GetClientAsync(new Guid(clientId));

				if (user.ClientId != null && user.ClientId != null && user.ClientId != client.ClientId) 
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				NoMatterDatabaseModel.Section sectionDb = section.ToDatabaseSection(client.ClientId);

				//Save the section
				var sectionId = await _sectionRepository.AddSectionAsync(sectionDb);

				//Now add the defualt system categories
				var latestItemsCategory = new NoMatterDatabaseModel.Category
					{
						CategoryName = "Latest Items",
						ActionName = "Latest",
						CategoryOrder = 1,
						Conditional = true,
						Hidden = false,
						SectionId = sectionId
					};

				var salesItemsCategory = new NoMatterDatabaseModel.Category()
				{
					CategoryName = "Sale Items",
					ActionName = "Sale",
					CategoryOrder = 2,
					Conditional = true,
					Hidden = false,
					SectionId = sectionId
				};


				await _categoryRepository.AddSectionCategoryAsync(latestItemsCategory);
				await _categoryRepository.AddSectionCategoryAsync(salesItemsCategory);

				return Ok();

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

	}
}