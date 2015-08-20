using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NoMatterDataLibrary;
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
			_globalRepository = new GlobalRepository();
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
				var settings = await _globalRepository.GetGlobalSettingsAsync();

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