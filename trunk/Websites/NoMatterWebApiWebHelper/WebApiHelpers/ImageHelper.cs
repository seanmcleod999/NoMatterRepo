using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.Exceptions;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace NoMatterWebApiWebHelper.WebApiHelpers
{
	public interface IImageHelper
	{
		Task<string> UploadImageAsync(string clientId, HttpPostedFileBase file);
	}

	public class ImageHelper : IImageHelper
	{
		private IGlobalSettings _globalSettings;
		
		public ImageHelper()
		{
			_globalSettings = new GlobalSettings();
		}

		public ImageHelper(IGlobalSettings globalSettings)
		{
			_globalSettings = globalSettings;
		}

		public async Task<string> UploadImageAsync(string clientId, HttpPostedFileBase file)
		{

			string imageBase64String = null;

			if (file != null)
			{
				using (var binaryReader = new BinaryReader(file.InputStream))
				{
					var imageData = binaryReader.ReadBytes(file.ContentLength);

					// Convert byte[] to Base64 String
					imageBase64String = Convert.ToBase64String(imageData);
				}
			}

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var uploadImageModel = new UploadImageModel
					{
						ImageData = imageBase64String
					};

				var response = await client.PostAsJsonAsync(string.Format("api/v1/clients/{0}/images", clientId), uploadImageModel);

				if (!response.IsSuccessStatusCode)
				{
					GeneralHelper.HandleWebApiFailedResult(response);
				}

				var uploadImageResponse = await response.Content.ReadAsAsync<UploadImageResponse>();

				return uploadImageResponse.ImageId;

			}
		}

	}
}
