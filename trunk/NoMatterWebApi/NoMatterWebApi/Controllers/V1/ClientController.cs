using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NoMatterDatabaseModel;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Extensions;
using NoMatterWebApi.Logging;
using NoMatterWebApiModels.Models;
using Client = NoMatterWebApiModels.Models.Client;
using ClientDeliveryOption = NoMatterWebApiModels.Models.ClientDeliveryOption;
using ClientPaymentType = NoMatterWebApiModels.Models.ClientPaymentType;
using Section = NoMatterWebApiModels.Models.Section;


namespace NoMatterWebApi.Controllers.V1
{
	//[Authorize]
	[RoutePrefix("api/v1/clients")]
	public class ClientController : ApiController
	{
		private IClientRepository _clientRepository;
		private IUserRepository _userRepository;

		public ClientController()
		{
			var databaseEntity = new DatabaseEntities();

			_clientRepository = new ClientRepository(databaseEntity);
			_userRepository = new UserRepository(databaseEntity);
		}

		public ClientController(IClientRepository clientRepository, IUserRepository userRepository)
		{
			_clientRepository = clientRepository;
			_userRepository = userRepository;
		}

		// GET api/v1/clients
		[Route("")]
		[ResponseType(typeof(List<Client>))]
		public async Task<IHttpActionResult> GetClients()
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

		// GET api/v1/clients/{clientUuid}/sections
		[Route("{clientUuid}/sections")]
		[ResponseType(typeof(List<Section>))]
		public async Task<IHttpActionResult> GetClientSections(string clientUuid)
		{
			try
			{
		
				var sectionsDb = await _clientRepository.GetClientSectionsAsync(new Guid(clientUuid));

				var sections = sectionsDb.Select(x => x.ToDomainSection()).ToList();

				return Ok(sections);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// GET api/v1/client/5/settingd
		[Route("{clientUuid}/settings")]
		[ResponseType(typeof(ClientSetting))]
		public async Task<IHttpActionResult> GetClientSettings(string clientUuid)
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

		// GET api/v1/client/5/payment-types
		[Route("{clientUuid}/delivery-options")]
		[ResponseType(typeof(List<ClientDeliveryOption>))]
		public async Task<IHttpActionResult> GetClientDeliveryOptions(string clientUuid)
		{
			try
			{
				var clientDeliveryOptionsDb = await _clientRepository.GetClientDeliveryOptionsAsync(new Guid(clientUuid));

				var clientDeliveryOptions = clientDeliveryOptionsDb.Select(x => x.ToDomainClientDeliveryOption()).ToList();

				return Ok(clientDeliveryOptions);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// GET api/v1/client/5/payment-types
		[Route("{clientUuid}/payment-types")]
		[ResponseType(typeof(ClientPaymentType))]
		public async Task<IHttpActionResult> GetClientPaymentTypes(string clientUuid)
		{
			try
			{
				var clientPaymentTypesDb = await _clientRepository.GetClientPaymentTypesAsync(new Guid(clientUuid));

				var clientPaymentTypes = clientPaymentTypesDb.Select(x => x.ToDomainClientPaymentType()).ToList();

				return Ok(clientPaymentTypes);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		//// POST api/<controller>
		//public void Post([FromBody]string value)
		//{
		//}

		//// PUT api/<controller>/5
		//public void Put(int id, [FromBody]string value)
		//{
		//}

		//// DELETE api/<controller>/5
		//public void Delete(int id)
		//{
		//}
	}
}