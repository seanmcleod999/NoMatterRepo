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
using NoMatterWebApiWebHelper.Exceptions;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace NoMatterWebApiWebHelper.WebApiHelpers
{
	public interface IOrderHelper
	{
		Task<string> GenerateUserOrderAsync(string userId, GenerateUserOrder generateUserOrder);
		Task<Order> GetOrderAsync(string orderId);
		Task<Order> ProcessPayfastOrderAsync(NameValueCollection req, bool testing = false);
		string GeneratePayfastRedirectUrl(string orderId, string amount);
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

				var response = await client.PostAsJsonAsync(String.Format("api/v1/clients/{0}/users/{1}/orders", _globalSettings.DefaultClientId, userId), generateUserOrder);

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Error generating user order", response);

				var orderId = await response.Content.ReadAsAsync<string>();

				return orderId;
			}
		}

		public async Task<Order> GetOrderAsync(string orderId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/orders/{0}", orderId));

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot get order", response);

				var order = await response.Content.ReadAsAsync<Order>();

				return order;

			}
		}

		public string GeneratePayfastRedirectUrl(string orderId, string amount)
		{

			string paymentName = "Payment to " + _globalSettings.SiteName + ". Order #" + orderId;
			string paymentDescription = "Payment to " + _globalSettings.SiteName;

			string site = "";
			string merchant_id = "";
			string merchant_key = "";

			// Check if we are using the test or live system
			string paymentMode = _globalSettings.PayfastPaymentMode;

			if (paymentMode.ToLower() == "test")
			{
				site = "https://sandbox.payfast.co.za/eng/process?";
				merchant_id = "10000100";
				merchant_key = "46f0cd694581a";
			}
			else if (paymentMode.ToLower() == "live")
			{
				site = "https://www.payfast.co.za/eng/process?";
				merchant_id = _globalSettings.PayfastMerchantId;
				merchant_key = _globalSettings.PayfastMerchantKey;
			}
			else
			{
				throw new InvalidOperationException("Cannot process payment if PaymentMode (in web.config) value is unknown.");
			}

			// Build the query string for payment site
			var str = new StringBuilder();

			str.Append("merchant_id=" + HttpUtility.UrlEncode(merchant_id));
			str.Append("&merchant_key=" + HttpUtility.UrlEncode(merchant_key));
			str.Append("&return_url=" + HttpUtility.UrlEncode(_globalSettings.PayfastReturnUrl));
			str.Append("&cancel_url=" + HttpUtility.UrlEncode(_globalSettings.PayfastCancelUrl));
			str.Append("&notify_url=" + HttpUtility.UrlEncode(_globalSettings.PayfastNotifyUrl));
			str.Append("&m_payment_id=" + HttpUtility.UrlEncode(orderId.ToString()));
			str.Append("&amount=" + HttpUtility.UrlEncode(amount));
			str.Append("&item_name=" + HttpUtility.UrlEncode(paymentName));
			str.Append("&item_description=" + HttpUtility.UrlEncode(paymentDescription));
			str.Append("&custom_int1=" + HttpUtility.UrlEncode(orderId.ToString()));

			return site + str;

		}


		public async Task<Order> ProcessPayfastOrderAsync(NameValueCollection req, bool testing = false)
		{
			try
			{
				var arrPostedVariables = new NameValueCollection(); // We will use later to post data 

				string strPostedVariables = "";
				string key = "";
				string value = "";

				for (int i = 0; i < req.Count; i++)
				{
					key = req.Keys[i];
					value = req[i];

					if (key != "signature")
					{
						strPostedVariables += key + "=" + value + "&";
						arrPostedVariables.Add(key, value);

						//Logger.WriteDebugInformationLog("Key:" + key + ", Value:" + value);
					}
				}

				// Remove the last &
				strPostedVariables = strPostedVariables.TrimEnd(new char[] { '&' });

				// Also get the Ids early. They are used to log errors to the orders table.
				string orderId = req["m_payment_id"];
				string processorOrderId = req["pf_payment_id"];

				// Are we testing or making live payments
				string site = "";
				string merchant_id = "";
				string paymentMode = _globalSettings.PayfastPaymentMode;

				if (paymentMode.ToLower() == "test")
				{
					site = "https://sandbox.payfast.co.za/eng/query/validate";
					merchant_id = "10000100";
				}
				else if (paymentMode.ToLower() == "live")
				{
					site = "https://www.payfast.co.za/eng/query/validate";
					merchant_id = _globalSettings.PayfastMerchantId;
				}
				else
				{
					throw new InvalidOperationException("Cannot process payment if PaymentMode (in web.config) value is unknown.");
				}

				// Get the posted signature from the form.
				string postedSignature = req["signature"];

				if (string.IsNullOrEmpty(postedSignature))
					throw new Exception("Signature parameter cannot be null");

				// Check if this is a legitimate request from the payment processor
				PayfastHelper.PerformSecurityChecks(arrPostedVariables, merchant_id, _currentUser.UserHostAddress());

				if (!testing)
				{
					// The request is legitimate. Post back to payment processor to validate the data received
					PayfastHelper.ValidateData(site, arrPostedVariables);
				}

				// All is valid, process the order
				// Determine from payment status if we are supposed to credit or not
				string paymentStatus = arrPostedVariables["payment_status"];

				switch (paymentStatus)
				{
					case "COMPLETE":
						//OrderHelper.UpdateOrderPaid(Convert.ToInt16(orderId), true);
						//OrderHelper.SoldOrderShopItems(Convert.ToInt16(orderId));
						UpdateOrderPaid(Convert.ToInt16(orderId), true);
						MarkOrderShopItemsAsSold(Convert.ToInt32(orderId));
						break;

					case "FAILED":
						//Logger.WriteGeneralInformationLog(string.Format("A payfast payment failed with the following status {0} OrderID: {1}", paymentStatus, orderId));
						break;

					default:
						//Logger.WriteGeneralInformationLog(string.Format("A payfast payment failed with the following status: {0}. OrderID: {1}", paymentStatus, orderId));
						break;
				}

				var order = await GetOrderAsync(orderId);

				return order;

			}
			catch (Exception ex)
			{
				//Logger.WriteGeneralError(ex);
				throw;
			}

		}

		private void UpdateOrderPaid(int orderId, bool paid)
		{
			try
			{
				//using (var mainDb = new DatabaseModelEntities())
				//{
	
				//		var order = mainDb.orders.Find(orderId);

				//		order.Paid = paid;

				//		mainDb.SaveChanges();
					

				//}

			}
			catch (Exception ex)
			{
				//Logger.WriteGeneralError(ex);
				throw;
			}
		}

		private void MarkOrderShopItemsAsSold(int orderId)
		{
			try
			{
				//using (var mainDb = new DatabaseModelEntities())
				//{
				//	var orderShopItems = mainDb.order_shopitem.Where(x => x.OrderId == orderId).ToList();

				//	foreach (var shopItem in orderShopItems)
				//	{
				//		shopItem.shopitem.DateSold = DateTime.Now;
				//		shopItem.shopitem.Sold = true;
				//	}

				//	mainDb.SaveChanges();

				//}
				
			}
			catch (Exception ex)
			{
				//Logger.WriteGeneralError(ex);
				throw;
			}
		}
	}


}
