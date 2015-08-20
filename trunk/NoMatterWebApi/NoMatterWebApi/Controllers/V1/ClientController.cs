using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CustomAuthLib;
using NoMatterDataLibrary;
using NoMatterWebApi.ActionResults;
using NoMatterWebApi.Helpers;
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

			_clientRepository = new ClientRepository();
			_userRepository = new UserRepository();
			_sectionRepository = new SectionRepository();
			_categoryRepository = new CategoryRepository();
		}

		public ClientController(IClientRepository clientRepository, IUserRepository userRepository, SectionRepository sectionRepository, CategoryRepository categoryRepository)
		{
			_clientRepository = clientRepository;
			_userRepository = userRepository;
			_sectionRepository = sectionRepository;
			_categoryRepository = categoryRepository;
		}

	
		[HttpGet]
		[Route("api/v1/clients")]
		[ResponseType(typeof(List<Client>))]
		public async Task<IHttpActionResult> GetClientsAsync()
		{
			try
			{
				var clients = await _clientRepository.GetClientsAsync();

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
				var clientSettings = await _clientRepository.GetClientSettingsAsync(new Guid(clientId));

				return Ok(clientSettings);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
			
		}

		[HttpGet]
		[Route("api/v1/clients/{clientId}/settings/{clientSettingId}")]
		[ResponseType(typeof(ClientSetting))]
		public async Task<IHttpActionResult> GetClientSettingAsync(string clientId, int clientSettingId)
		{
			try
			{
				var clientSetting = await _clientRepository.GetClientSettingAsync(new Guid(clientId), clientSettingId);

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

				//Save the section
				await _clientRepository.AddClientSettingAsync(clientSetting);

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

				//var clientSettingDb = await _clientRepository.GetClientSettingAsync(new Guid(clientId), clientSettingId);
				//if (clientSettingDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientSettingNotFound);

				//Update the page
				await _clientRepository.UpdateClientSettingAsync(clientSetting);

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
				var clientDeliveryOptions = await _clientRepository.GetClientDeliveryOptionsAsync(new Guid(clientId));

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
				var clientPaymentTypes = await _clientRepository.GetClientPaymentTypesAsync(new Guid(clientId));

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
				var clientPages = await _clientRepository.GetClientPagesAsync(new Guid(clientId));

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
				var clientPage = await _clientRepository.GetClientPageAsync(new Guid(clientId), pageName);

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


				//Save the section
				await _clientRepository.AddClientPageAsync(clientPage);
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

				//var clientPageDb = await _clientRepository.GetClientPageAsync(new Guid(clientId), pageName);
				//if (clientPageDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientPageNotFound);
				
				//Update the page
				await _clientRepository.UpdateClientPageAsync(clientPage);

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
				var sections = await _sectionRepository.GetClientSectionsAsync(new Guid(clientId), includeHidden);

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

		[HttpGet]
		[Route("api/v1/clients/{clientId}/sectionsandcategories")]
		[ResponseType(typeof(List<SectionsAndCategories>))]
		public async Task<IHttpActionResult> GetClientSectionsAndCategories(string clientId, bool includeEmpty = false, bool includeHidden = false)
		{
			try
			{
				var sections = await _sectionRepository.GetClientSectionsAndCategoriesAsync(new Guid(clientId), includeHidden);

				//var sectionsAndCategories = sectionsDb.Select(x => x.ToDomainSectionsAndCategories()).ToList();

				//if (!includeEmpty)
				//{
				//	sectionsAndCategories = sectionsAndCategories.Where(x => x.VisibleCategoryCount > 0).ToList();
				//}

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

				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
				if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);


				//Save the section
				var sectionId = await _sectionRepository.AddSectionAsync(section);

				await SectionHelper.AddDefaultSectionCategories(sectionId, _categoryRepository);

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
				var clientDeliveryOption = await _clientRepository.GetClientDeliveryOptionAsync(clientDeliveryOptionId);

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

				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
				if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				//Save the section
				await _clientRepository.AddClientDeliveryOptionAsync(clientDeliveryOption);

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

				//var clientDeliveryOptionDb = await _clientRepository.GetClientDeliveryOptionAsync(clientDeliveryOptionId);
				//if (clientDeliveryOptionDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientDeliveryOptionNotFound);

				//Update the page
				await _clientRepository.UpdateClientDeliveryOptionAsync(clientDeliveryOption);

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