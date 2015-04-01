﻿using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ImageResizer;
using NoMatterDatabaseModel;
using NoMatterWebApi.ActionResults;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApiModels.Models;
using Client = NoMatterDatabaseModel.Client;

namespace NoMatterWebApi.Controllers.V1
{
	public class ImageController : ApiController
	{
		private IClientRepository _clientRepository;
		private IGlobalSettings _globalSettings;
		private IGeneralHelper _generalHelper;
		

		public ImageController()
		{
			var databaseEntity = new DatabaseEntities();

			_clientRepository = new ClientRepository(databaseEntity);		
			_globalSettings = new GlobalSettings();
			_generalHelper = new GeneralHelper();
		}

		public ImageController(IClientRepository clientRepository, IGlobalSettings globalSettings, IGeneralHelper generalHelper)
		{
			_clientRepository = clientRepository;
			_globalSettings = globalSettings;
			_generalHelper = generalHelper;
		}


		[HttpPost]
		[Route("api/v1/clients/{clientId}/images")]
		[ResponseType(typeof(UploadImageResponse))]
		public async Task<IHttpActionResult> UploadImageAsync(string clientId, string ImageData)
		{
			try
			{
				if (string.IsNullOrEmpty(ImageData)) return new CustomBadRequest(Request, ApiResultCode.ParametersNotValid);

				var client = await _clientRepository.GetClientAsync(new Guid(clientId));
				if (client == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				var imagepath = _generalHelper.SaveImage(ImageData, client.ClientId);

				return Created(new Uri(_globalSettings.ImagesBaseUrl + imagepath), imagepath);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);

				return InternalServerError(ex);
			}
		}

		
	}
}