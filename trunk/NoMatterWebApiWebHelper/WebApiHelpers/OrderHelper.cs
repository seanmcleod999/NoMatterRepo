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
	public interface IOrderHelper
	{
		Task<string> GenerateUserOrder(string userId, GenerateUserOrder generateUserOrder);
			
	}
	

	public class OrderHelper : IOrderHelper
	{

		private IGlobalSettings _globalSettings;
		
		public OrderHelper()
		{
			_globalSettings = new GlobalSettings();
		}

		public OrderHelper(IGlobalSettings globalSettings)
		{
			_globalSettings = globalSettings;
		}

		public async Task<string> GenerateUserOrder(string userId, GenerateUserOrder generateUserOrder)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.PostAsJsonAsync(String.Format("api/v1/clients/{0}/users/{1}/orders", _globalSettings.DefaultClientId, userId), generateUserOrder);

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Error generating user order", response);

				var orderId = await response.Content.ReadAsAsync<string>();

				return orderId;
			}
		}

	}
}
