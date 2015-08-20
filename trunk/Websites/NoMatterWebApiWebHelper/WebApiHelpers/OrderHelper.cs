using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.Enums;
using NoMatterWebApiWebHelper.Exceptions;
using NoMatterWebApiWebHelper.OtherHelpers;


namespace NoMatterWebApiWebHelper.WebApiHelpers
{
	public interface IOrderHelper
	{
		Task<string> GenerateUserOrderAsync(string userId, GenerateUserOrder generateUserOrder);
		Task<Order> GetOrderAsync(int orderId);
		Task<Order> ProcessEftOrderAsync(int orderId);
		//Task<Order> ProcessPayfastOrderAsync(int orderId, PayfastPaymentStatusEnum paymentStatus);
		//string GeneratePayfastRedirectUrl(string orderId, string amount);
		Task<Order> UpdateOrderPaymentType(int orderId, short paymentTypeId);
		Task UpdateOrderPaidAndSold(int orderId);
		Task UpdateOrderReserved(int orderId);
		Task UpdateOrderFailed(int orderId, string errorMessage);
	}
	

	public class OrderHelper : IOrderHelper
	{

		private IGlobalSettings _globalSettings;
		private ICurrentUser _currentUser;
		
		public OrderHelper()
		{
			_globalSettings = new GlobalSettings();
			_currentUser = new CurrentUser();
		}

		public OrderHelper(IGlobalSettings globalSettings, ICurrentUser currentUser)
		{
			_globalSettings = globalSettings;
			_currentUser = currentUser;
		}

		public async Task<string> GenerateUserOrderAsync(string userId, GenerateUserOrder generateUserOrder)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.PostAsJsonAsync(String.Format("api/v1/clients/{0}/users/{1}/orders", _globalSettings.SiteClientId, userId), generateUserOrder);

				if (!response.IsSuccessStatusCode)
					GeneralHelper.HandleWebApiFailedResult(response);

				var orderId = await response.Content.ReadAsAsync<string>();

				return orderId;
			}
		}

		public async Task<Order> GetOrderAsync(int orderId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/clients/{0}/orders/{1}", _globalSettings.SiteClientId, orderId));

				if (!response.IsSuccessStatusCode)
					GeneralHelper.HandleWebApiFailedResult(response);

				var order = await response.Content.ReadAsAsync<Order>();

				return order;

			}
		}

		public async Task<Order> ProcessEftOrderAsync(int orderId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var model = new ProcessEftOrderModel();

				var response = await client.PostAsJsonAsync(string.Format("api/v1/clients/{0}/orders/{1}/processeft", _globalSettings.SiteClientId, orderId), model);

				if (!response.IsSuccessStatusCode)
					GeneralHelper.HandleWebApiFailedResult(response);

				var order = await response.Content.ReadAsAsync<Order>();

				return order;

			}
		}

		public async Task<Order> UpdateOrderPaymentType(int orderId, short paymentTypeId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var model = new UpdateOrderPaymentTypeModel
					{
						PaymentTypeId = paymentTypeId
					};

				var response = await client.PostAsJsonAsync(string.Format("api/v1/clients/{0}/orders/{1}/updatepaymenttype", _globalSettings.SiteClientId, orderId), model);

				if (!response.IsSuccessStatusCode)
					GeneralHelper.HandleWebApiFailedResult(response);

				var order = await response.Content.ReadAsAsync<Order>();

				return order;

			}
		}

		


		//public async Task<Order> ProcessPayfastOrderAsync(int orderId, PayfastPaymentStatusEnum paymentStatus)
		//{

		//		switch (paymentStatus)
		//		{
		//			case PayfastPaymentStatusEnum.Complete:
		//				await UpdateOrderPaidAndSold(Convert.ToInt16(orderId));
		//				break;

		//			case PayfastPaymentStatusEnum.Failed:
		//				await UpdateOrderFailed(Convert.ToInt16(orderId), "Payfast Failure");
		//				break;

		//			default:
		//				await UpdateOrderFailed(Convert.ToInt16(orderId), "Unknow Payment Status");
		//				break;
		//		}

		//		var order = await GetOrderAsync(Convert.ToInt32(orderId));

		//		return order;

			

		//}


		public async Task UpdateOrderPaidAndSold(int orderId)
		{		
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var model = new ProcessPaidOrderModel();

				var response = await client.PostAsJsonAsync(String.Format("api/v1/clients/{0}/orders/{1}/paid", _globalSettings.SiteClientId, orderId), model);

				if (!response.IsSuccessStatusCode)
					GeneralHelper.HandleWebApiFailedResult(response);
			}
		}

		public async Task UpdateOrderReserved(int orderId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var model = new ProcessFailedOrderModel();

				var response = await client.PostAsJsonAsync(String.Format("api/v1/clients/{0}/orders/{1}/reserve", _globalSettings.SiteClientId, orderId), model);

				if (!response.IsSuccessStatusCode)
					GeneralHelper.HandleWebApiFailedResult(response);
			}
		}

		public async Task UpdateOrderFailed(int orderId, string errorMessage)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				//TODO: add the error message to the post

				var response = await client.PostAsJsonAsync(String.Format("api/v1/orders/{0}/failed", orderId), new object());

				if (!response.IsSuccessStatusCode)
					GeneralHelper.HandleWebApiFailedResult(response);
			}
		}

	}
}
