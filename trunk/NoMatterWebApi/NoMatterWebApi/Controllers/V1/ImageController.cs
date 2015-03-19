using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using ImageResizer;
using NoMatterDatabaseModel;
using NoMatterWebApi.ActionResults;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApi.Controllers.V1
{
	public class ImageController : ApiController
	{
		private IClientRepository _clientRepository;
		private IWebApiGlobalSettings _webApiGlobalSettings;
		

		public ImageController()
		{
			var databaseEntity = new DatabaseEntities();

			_clientRepository = new ClientRepository(databaseEntity);		
			_webApiGlobalSettings = new WebApiGlobalSettings();
		}

		public ImageController(IClientRepository clientRepository, IWebApiGlobalSettings webApiGlobalSettings)
		{
			_clientRepository = clientRepository;
			_webApiGlobalSettings = webApiGlobalSettings;
		}

		// POST api/v1/images
		/// <summary>
		/// Upload an image and get the unique image identifier
		/// </summary>
		/// <returns>The image unique identifier</returns>
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

				byte[] imageData = Convert.FromBase64String(ImageData);

				var path = HttpContext.Current.Server.MapPath("~/Images/" + client.ClientId + "/");

				if (!Directory.Exists(path)) Directory.CreateDirectory(path);

				var imageGuid = Guid.NewGuid().ToString();

					ImageBuilder.Current.Build(
						new ImageJob(
							imageData,
							path + imageGuid ,
							new Instructions("maxwidth=1500&maxheight=1500&format=jpg&quality=95"),
							false,
							true));

				//var uploadImageResponse = new UploadImageResponse
				//{
				var imageId = string.Format("{0}/{1}.jpg", client.ClientId, imageGuid);
				//};

				return Created(new Uri(_webApiGlobalSettings.ImagesBaseUrl + imageId), imageId);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);

				return InternalServerError(ex);
			}
		}		
	}
}