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
using Client = NoMatterWebApiModels.Models.Client;
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

			var sectionsDb = await _clientRepository.GetClientSectionsAsync(new Guid(clientUuid));

			var sections = sectionsDb.Select(x => x.ToDomainSection()).ToList();

			return Ok(sections);
		}

		// GET api/v1/client/5/settingd
		[Route("{clientUuid}/settings")]
		[ResponseType(typeof(Client))]
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
			
			//var client = new Client() { ClientId = id, ClientName = "Test1" };
			//return Ok(client);
		}

		// GET api/v1/client/5/payment-types
		[Route("{clientUuid}/payment-types")]
		[ResponseType(typeof(Client))]
		public IHttpActionResult GetClientPaymentTypes(string clientUuid)
		{
			var client = new Client() { ClientId = clientUuid, ClientName = "Test1" };
			return Ok(client);
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