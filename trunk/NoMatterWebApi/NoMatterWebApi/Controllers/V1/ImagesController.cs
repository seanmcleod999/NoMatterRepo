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
using NoMatterWebApi.Logging;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApi.Controllers.V1
{
	public class ImagesController : ApiController
	{
		
		// POST api/v1/images
		/// <summary>
		/// Upload an image and get the unique image identifier
		/// </summary>
		/// <returns>The image unique identifier</returns>
		[HttpPost]
		[Route("api/v1/client/{clientId}/images")]
		//[ResponseType(typeof(UploadImageResponse))]
		public async Task<IHttpActionResult> UploadImageAsync(UploadImageModel model)
		{
			try
			{
				if (string.IsNullOrEmpty(model.ImageData)) return BadRequest("No Image Data");

				byte[] imageData = Convert.FromBase64String(model.ImageData);

				var imageUuid = Guid.NewGuid();

				//Save the drawing to disk
				using (var ms = new MemoryStream(imageData))
				{
					
					var image = System.Drawing.Image.FromStream(ms);

					var pictureFileName = imageUuid + ".jpg";
					var imageFolderPath = HttpContext.Current.Server.MapPath("~/Content/ClientImages/");
					var fullPicturePath = Path.Combine(imageFolderPath, pictureFileName);

					image.Save(fullPicturePath);
				}


				var uploadImageResponse = new UploadImageResponse
				{
					ImageId = imageUuid.ToString()
				};

				//return Created(new Uri(string.Format("http://nomatter.co.za/images/{0}/{1}", "123", "234")), uploadImageResponse);
				return Ok(uploadImageResponse);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);

				return InternalServerError(ex);
			}
		}		
	}
}