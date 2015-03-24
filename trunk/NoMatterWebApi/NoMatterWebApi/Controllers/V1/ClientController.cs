using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NoMatterDatabaseModel;
using NoMatterWebApi.ActionResults;
using NoMatterWebApi.CustomAuth;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Extensions;
using NoMatterWebApi.Logging;
using NoMatterWebApiModels.Models;
using Client = NoMatterWebApiModels.Models.Client;
using ClientDeliveryOption = NoMatterWebApiModels.Models.ClientDeliveryOption;
using ClientPage = NoMatterWebApiModels.Models.ClientPage;
using ClientPaymentType = NoMatterWebApiModels.Models.ClientPaymentType;
using ClientSetting = NoMatterWebApiModels.Models.ClientSetting;
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

		//// POST api/v1/client
		//[HttpPost]
		//[Authorize]
		//[Route("api/v1/clients")]
		//public async Task<IHttpActionResult> AddClientAsync(Client client)
		//{
		//	//TODO: make sure the user can add this page

		//	try
		//	{
		//		var userToken = User.Identity.Name;

		//		var userDb = await _userRepository.GetClientUserByTokenAsync(userToken);
		//		if (userDb == null) return new CustomBadRequest(Request, ApiResultCode.UserNotFound);

		//		NoMatterDatabaseModel.Client clientDb = client.ToDatabaseClient();

		//		//Save the client
		//		await _clientRepository.AddClientAsync(clientDb);
		//		return Ok();

		//	}
		//	catch (Exception ex)
		//	{
		//		Logger.WriteGeneralError(ex);
		//		return InternalServerError(ex);
		//	}
		//}

		//// PUR api/v1/clients
		//[HttpPut]
		//[Authorize]
		//[Route("api/v1/clients/{clientId}")]
		//public async Task<IHttpActionResult> UpdateClientAsync(string clientId, Client client)
		//{
		//	//TODO: make sure the user can update this client

		//	try
		//	{
		//		var userToken = User.Identity.Name;

		//		var userDb = await _userRepository.GetClientUserByTokenAsync(userToken);
		//		if (userDb == null) return new CustomBadRequest(Request, ApiResultCode.UserNotFound);

		//		var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
		//		if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

		//		if (userDb.Client != null && userDb.ClientId != clientDb.ClientId) return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

		//		//Update the client
		//		await _clientRepository.UpdateClientAsync(clientDb, client);

		//		return Ok();

		//	}
		//	catch (Exception ex)
		//	{
		//		Logger.WriteGeneralError(ex);
		//		return InternalServerError(ex);
		//	}
		//}

		[HttpGet]
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

		//// GET api/v1/clients/{clientId}
		//[Route("api/v1/clients/{clientId}")]
		//[ResponseType(typeof(List<Client>))]
		//public async Task<IHttpActionResult> GetClientAsync(string clientId)
		//{
		//	try
		//	{
		//		var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));

		//		if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

		//		var client = clientDb.ToDomainClient();

		//		return Ok(client);
		//	}
		//	catch (Exception ex)
		//	{
		//		Logger.WriteGeneralError(ex);
		//		return InternalServerError(ex);
		//	}

		//}

		// POST api/v1/clients/{clientId}/sections
		

		[HttpGet]
		[Route("api/v1/clients/{clientId}/settings")]
		[ResponseType(typeof(List<ClientSetting>))]
		public async Task<IHttpActionResult> GetClientSettingsAsync(string clientId)
		{
			try
			{
				var clientSettingsDb = await _clientRepository.GetClientSettingsAsync(new Guid(clientId));

				var clientSettings = clientSettingsDb.Select(x => x.ToDomainClientSetting()).ToList();

				return Ok(clientSettings);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
			
		}

		[HttpGet]
		[Route("api/v1/clients/{clientId}/setting/{settingId}")]
		[ResponseType(typeof(ClientSetting))]
		public async Task<IHttpActionResult> GetClientSettingAsync(string clientId, int clientSettingId)
		{
			try
			{
				var clientSettingDb = await _clientRepository.GetClientSettingAsync(new Guid(clientId), clientSettingId);

				var clientSetting = clientSettingDb.ToDomainClientSetting();

				return Ok(clientSetting);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}


		[HttpPost]
		[Authorize]
		[Route("api/v1/clients/{clientId}/settings")]
		public async Task<IHttpActionResult> AddClientSettingAsync(string clientId, ClientSetting clientSetting)
		{
			try
			{
				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
				if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				var clienSettingDb = clientSetting.ToDatabaseClientSetting(clientDb.ClientId);

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


		[HttpPost]
		[Authorize]
		[Route("api/v1/clients/{clientId}/settings/allocate-missing")]
		public async Task<IHttpActionResult> AllocateMissingClientSettingsAsync(string clientId)
		{
			//TODO: make sure the user can add this page

			try
			{

				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
				if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				//Save the section
				var clientSettings = await _clientRepository.GetClientSettingsAsync(new Guid(clientId));

				var settingsIds = clientSettings.Select(x => x.SettingId).ToList();

				await _clientRepository.AllocateMissingClientSettingsAsync(clientDb, settingsIds);

				return Ok();

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		[HttpPut]
		[Authorize]
		[Route("api/v1/clients/{clientId}/settings/{settingId}")]
		public async Task<IHttpActionResult> UpdateClientSettingAsync(string clientId, int clientSettingId, ClientSetting clientSetting)
		{
			try
			{
				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				var clientSettingDb = await _clientRepository.GetClientSettingAsync(new Guid(clientId), clientSettingId);
				if (clientSettingDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientSettingNotFound);

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

		[Authorize]
		[HttpDelete]
		[Route("api/v1/clients/{clientId}/settings/{settingId}")]
		public async Task<IHttpActionResult> DeleteClientSettingAsync(string clientId, short settingId)
		{
			try
			{
				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				await _clientRepository.DeleteClientSettingAsync(new Guid(clientId), settingId);

				return Ok();
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		[HttpGet]
		[Route("api/v1/clients/{clientId}/delivery-options")]
		[ResponseType(typeof(List<ClientDeliveryOption>))]
		public async Task<IHttpActionResult> GetClientDeliveryOptions(string clientId)
		{
			try
			{
				var clientDeliveryOptionsDb = await _clientRepository.GetClientDeliveryOptionsAsync(new Guid(clientId));

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


		[HttpGet]
		[Route("api/v1/clients/{clientId}/payment-types")]
		[ResponseType(typeof(List<ClientPaymentType>))]
		public async Task<IHttpActionResult> GetClientPaymentTypes(string clientId)
		{
			try
			{
				var clientPaymentTypesDb = await _clientRepository.GetClientPaymentTypesAsync(new Guid(clientId));

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

		
		[HttpGet]
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


		[HttpGet]
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


		[HttpPost]
		[Authorize]
		[Route("api/v1/clients/{clientId}/pages")]
		public async Task<IHttpActionResult> AddClientPageAsync(string clientId, ClientPage clientPage)
		{

			try
			{

				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);


				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
				if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				var clientPageDb = clientPage.ToDatabaseClientPage(clientDb.ClientId);

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
			try
			{

				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				var clientPageDb = await _clientRepository.GetClientPageAsync(new Guid(clientId), pageName);
				if (clientPageDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientPageNotFound);
				
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

		[Authorize]
		[HttpDelete]
		[Route("api/v1/clients/{clientId}/pages/{pageName}")]
		public async Task<IHttpActionResult> DeleteClientPageAsync(string clientId, string pageName)
		{
			try
			{
				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				await _clientRepository.DeleteClientPageAsync(new Guid(clientId), pageName);

				return Ok();
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}


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
			try
			{
				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				//var userToken = User.Identity.Name;

				//var user = await _userRepository.GetClientUserByTokenAsync(userToken);

				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
				if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				//if (user.ClientId != null && user.ClientId != null && user.ClientId != client.ClientId) 
				//	return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				var sectionDb = section.ToDatabaseSection(clientDb.ClientId);

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


		[HttpGet]
		[Route("api/v1/clients/{clientId}/delivery-options/{clientDeliveryOptionId}")]
		[ResponseType(typeof(ClientDeliveryOption))]
		public async Task<IHttpActionResult> GetClientDeliveryOptionAsync(string clientId, short clientDeliveryOptionId)
		{
			try
			{
				var clientDeliveryOptionDb = await _clientRepository.GetClientDeliveryOptionAsync(clientDeliveryOptionId);

				var clientDeliveryOption = clientDeliveryOptionDb.ToDomainClientDeliveryOption();

				return Ok(clientDeliveryOption);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}


		[HttpPost]
		[Authorize]
		[Route("api/v1/clients/{clientId}/delivery-options")]
		public async Task<IHttpActionResult> AddClientDeliveryOptionAsync(string clientId, ClientDeliveryOption clientDeliveryOption)
		{
			try
			{
				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);



				//var userToken = User.Identity.Name;

				//var userDb = await _userRepository.GetClientUserByTokenAsync(userToken);
				//if (userDb == null) return new CustomBadRequest(Request, ApiResultCode.UserNotFound);

				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
				if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				//if (userDb.Client != null && userDb.ClientId != clientDb.ClientId) return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				var clientDeliveryOptionDb = clientDeliveryOption.ToDatabaseClientDeliveryOption(clientDb.ClientId);

				//Save the section
				await _clientRepository.AddClientDeliveryOptionAsync(clientDeliveryOptionDb);

				return Ok();

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}


		[HttpPut]
		[Authorize]
		[Route("api/v1/clients/{clientId}/delivery-options/{clientDeliveryOptionId}")]
		public async Task<IHttpActionResult> UpdateClientDeliveryOptionAsync(string clientId, short clientDeliveryOptionId, ClientDeliveryOption clientDeliveryOption)
		{
			try
			{
				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);



				//var userToken = User.Identity.Name;

				//var userDb = await _userRepository.GetClientUserByTokenAsync(userToken);
				//if (userDb == null) return new CustomBadRequest(Request, ApiResultCode.UserNotFound);

				//var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
				//if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				var clientDeliveryOptionDb = await _clientRepository.GetClientDeliveryOptionAsync(clientDeliveryOptionId);
				if (clientDeliveryOptionDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientDeliveryOptionNotFound);

				//if (userDb.Client != null && userDb.ClientId != clientDb.ClientId) return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				//Update the page
				await _clientRepository.UpdateClientDeliveryOptionAsync(clientDeliveryOptionDb, clientDeliveryOption);

				return Ok();

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		[Authorize]
		[HttpDelete]
		[Route("api/v1/clients/{clientId}/delivery-options/{clientDeliveryOptionId}")]
		public async Task<IHttpActionResult> DeleteClientDeliveryOptionAsync(string clientId, short clientDeliveryOptionId)
		{
			try
			{
				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				await _clientRepository.DeleteClientDeliveryOptionAsync(new Guid(clientId), clientDeliveryOptionId);

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