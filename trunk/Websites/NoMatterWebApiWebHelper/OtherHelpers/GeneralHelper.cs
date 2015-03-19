
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.Exceptions;

namespace NoMatterWebApiWebHelper.OtherHelpers
{
	public class GeneralHelper
	{
		public static List<SelectListItem> GetProvinces()
		{
			var suburbs = new List<SelectListItem>()
			{
				new SelectListItem() { Text="Eastern Cape", Value="Eastern Cape"},
				new SelectListItem() { Text="Free State", Value="Free State"},
				new SelectListItem() { Text="Gauteng", Value="Gauteng"},
				new SelectListItem() { Text="KwaZulu-Natal", Value="KwaZulu-Natal"},
				new SelectListItem() { Text="Limpopo", Value="Limpopo"},
				new SelectListItem() { Text="Mpumalanga", Value="Mpumalanga"},
				new SelectListItem() { Text="Northern Cape", Value="Northern Cape"},
				new SelectListItem() { Text="North-West", Value="North-West"},
				new SelectListItem() { Text="Western Cape", Value="Western Cape"},
        
			};

			return suburbs;

		}

		public static int? ExtractWebApiFailedResultCode(HttpResponseMessage response)
		{
			if (response.StatusCode == HttpStatusCode.BadRequest)
			{
				var resultContentErrorModel = response.Content.ReadAsAsync<WebApiErrorModel>().Result;
				return resultContentErrorModel.Code;
			}

			return null;

		}

		public static void HandleWebApiFailedResult(HttpResponseMessage response)
		{
			if (response.StatusCode == HttpStatusCode.BadRequest)
			{
				var resultContentErrorModel = response.Content.ReadAsAsync<WebApiErrorModel>().Result;
				throw new WebApiException(resultContentErrorModel.Text, response);
			}
			else
			{
				throw new WebApiException(response);
			}
			
		}

		public static WebApiResult HandleWebApiSuccessfulResult(object resultObject)
		{
			var webApiResult = new WebApiResult
			{
				ResultCode = 0,
				ResultObject = resultObject
			};

			return webApiResult;
		}
	}
}
