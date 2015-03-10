using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.Exceptions;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace NoMatterWebApiWebHelper.WebApiHelpers
{
	public interface IImageHelper
	{
		//Task<string> UploadImageAsync(string imageData);		
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

		//public async Task<string> UploadImageAsync(string imageData)
		//{
		//	using (var client = new HttpClient())
		//	{
		//		client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
		//		client.DefaultRequestHeaders.Accept.Clear();
		//		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

		//		var uploadImageModel = new UploadImageModel
		//			{
		//				ImageData = imageData
		//			};

		//		var response = await client.PostAsJsonAsync(string.Format("api/v1/clients/{0}/images", _globalSettings.DefaultClientId), uploadImageModel);

		//		if (!response.IsSuccessStatusCode)
		//			throw new WebApiException("Cannot save Image", response);

		//	}
		//}

	}
}
