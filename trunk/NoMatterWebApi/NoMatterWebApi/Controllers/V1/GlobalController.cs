using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NoMatterDatabaseModel;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Extensions;
using NoMatterWebApi.Logging;
using GlobalSetting = NoMatterWebApiModels.Models.GlobalSetting;

namespace NoMatterWebApi.Controllers.V1
{
	[RoutePrefix("api/v1/globals")]
	public class GlobalController : ApiController
	{
		
		private IGlobalRepository _globalRepository;

		public GlobalController()
		{
			var databaseEntity = new DatabaseEntities();

			_globalRepository = new GlobalRepository(databaseEntity);
		}

		public GlobalController(IGlobalRepository productRepository)
		{
			_globalRepository = productRepository;
		}

		// GET api/v1/client/5/settingd
		[Route("settings")]
		[ResponseType(typeof(GlobalSetting))]
		public async Task<IHttpActionResult> GetGlobalSettings()
		{
			try
			{
				var settingsDb = await _globalRepository.GetGlobalSettingsAsync();

				var settings = settingsDb.Select(x => x.ToDomainGlobalSetting()).ToList();

				return Ok(settings);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}

		}
	}
}