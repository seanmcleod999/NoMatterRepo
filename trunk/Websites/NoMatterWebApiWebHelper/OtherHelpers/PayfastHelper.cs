using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NoMatterWebApiWebHelper.Enums;
using NoMatterWebApiWebHelper.Logging;
using NoMatterWebApiWebHelper.WebApiHelpers;

namespace NoMatterWebApiWebHelper.OtherHelpers
{
	public interface IPayfastHelper
	{
		//void PerformSecurityChecks(NameValueCollection arrPostedVariables, string merchantId, string requestIp);
		bool IsIpAddressInList(string[] validSites, string ipAddress);
		Task ProcessOrder(NameValueCollection req);
		PayfastPaymentStatusEnum GetPayfastPaymentStatus(NameValueCollection req, IGlobalSettings globalSettings, ICurrentUser currentUser, bool testing = false);
		string GeneratePayfastRedirectUrl(string orderId, string amount, IGlobalSettings globalSettings);
	}

	public class PayfastHelper : IPayfastHelper
	{
		private IGlobalSettings _globalSettings;
		private ICurrentUser _currentUser;
		private IOrderHelper _orderHelper;
		
		public PayfastHelper()
		{
			_globalSettings = new GlobalSettings();
			_currentUser = new CurrentUser();
			_orderHelper = new OrderHelper();
		}

		public PayfastHelper(IGlobalSettings globalSettings, ICurrentUser currentUser, IOrderHelper orderHelper)
		{
			_globalSettings = globalSettings;
			_currentUser = currentUser;
			_orderHelper = orderHelper;
		}

		

		public bool IsIpAddressInList(string[] validSites, string ipAddress)
		{
			// Get the ip addresses of the websites
			ArrayList validIps = new ArrayList();

			for (int i = 0; i < validSites.Length; i++)
			{
				validIps.AddRange(System.Net.Dns.GetHostAddresses(validSites[i]));
			}

			IPAddress ipObject = IPAddress.Parse(ipAddress);

			if (validIps.Contains(ipObject))
				return true;
			else
				return false;

		}

		public async Task ProcessOrder(NameValueCollection req)
		{
			try
			{
				string orderId = req["m_payment_id"];

				Logger.WriteGeneralInformationLog("Order id is... " + orderId);

				var paymentStatus = GetPayfastPaymentStatus(req, _globalSettings, _currentUser);

				Logger.WriteGeneralInformationLog("Payment status is... " + paymentStatus.ToString());

				switch (paymentStatus)
				{
					case PayfastPaymentStatusEnum.Complete:
						Logger.WriteGeneralInformationLog("Calling UpdateOrderPaidAndSold");
						await _orderHelper.UpdateOrderPaidAndSold(Convert.ToInt16(orderId));
						break;

					case PayfastPaymentStatusEnum.Failed:
						Logger.WriteGeneralInformationLog("Calling UpdateOrderFailed");
						await _orderHelper.UpdateOrderFailed(Convert.ToInt16(orderId), "Payfast Failure");
						break;

					default:
						Logger.WriteGeneralInformationLog("Calling UpdateOrderFailed - Unknow Payment Status");
						await _orderHelper.UpdateOrderFailed(Convert.ToInt16(orderId), "Unknow Payment Status");
						break;
				}

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		

		public string GeneratePayfastRedirectUrl(string orderId, string amount, IGlobalSettings globalSettings)
		{
			try
			{		
				string paymentName = "Payment to " + globalSettings.SiteName + ". Order #" + orderId;
				string paymentDescription = "Payment to " + globalSettings.SiteName;

				string site = "";
				string merchant_id = "";
				string merchant_key = "";

				// Check if we are using the test or live system
				string paymentMode = globalSettings.PayfastPaymentMode;

				if (paymentMode.ToLower() == "test")
				{
					site = "https://sandbox.payfast.co.za/eng/process?";
					merchant_id = "10000100";
					merchant_key = "46f0cd694581a";
				}
				else if (paymentMode.ToLower() == "live")
				{
					site = "https://www.payfast.co.za/eng/process?";
					merchant_id = globalSettings.PayfastMerchantId;
					merchant_key = globalSettings.PayfastMerchantKey;
				}
				else
				{
					throw new InvalidOperationException("Cannot process payment if PaymentMode (in web.config) value is unknown.");
				}

				// Build the query string for payment site
				var str = new StringBuilder();

				str.Append("merchant_id=" + HttpUtility.UrlEncode(merchant_id));
				str.Append("&merchant_key=" + HttpUtility.UrlEncode(merchant_key));
				str.Append("&return_url=" + HttpUtility.UrlEncode(globalSettings.PayfastReturnUrl));
				str.Append("&cancel_url=" + HttpUtility.UrlEncode(globalSettings.PayfastCancelUrl));
				str.Append("&notify_url=" + HttpUtility.UrlEncode(globalSettings.PayfastNotifyUrl));
				str.Append("&m_payment_id=" + HttpUtility.UrlEncode(orderId));
				str.Append("&amount=" + HttpUtility.UrlEncode(amount));
				str.Append("&item_name=" + HttpUtility.UrlEncode(paymentName));
				str.Append("&item_description=" + HttpUtility.UrlEncode(paymentDescription));
				//str.Append("&custom_int1=" + HttpUtility.UrlEncode(orderId.ToString()));

				return site + str;

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}

		}

		public PayfastPaymentStatusEnum GetPayfastPaymentStatus(NameValueCollection req, IGlobalSettings globalSettings, ICurrentUser currentUser, bool testing = false)
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
				string paymentMode = globalSettings.PayfastPaymentMode;

				if (paymentMode.ToLower() == "test")
				{
					site = "https://sandbox.payfast.co.za/eng/query/validate";
					merchant_id = "10000100";
				}
				else if (paymentMode.ToLower() == "live")
				{
					site = "https://www.payfast.co.za/eng/query/validate";
					merchant_id = globalSettings.PayfastMerchantId;
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
				PerformSecurityChecks(arrPostedVariables, merchant_id, currentUser.UserHostAddress());

				if (!testing)
				{
					// The request is legitimate. Post back to payment processor to validate the data received
					ValidateData(site, arrPostedVariables);
				}

				// All is valid, process the order
				// Determine from payment status if we are supposed to credit or not
				string paymentStatus = arrPostedVariables["payment_status"];

				switch (paymentStatus)
				{
					case "COMPLETE":
						//OrderHelper.UpdateOrderPaid(Convert.ToInt16(orderId), true);
						//OrderHelper.SoldOrderShopItems(Convert.ToInt16(orderId));
						//await UpdateOrderPaidAndSold(Convert.ToInt16(orderId));
						//MarkOrderShopItemsAsSold(Convert.ToInt32(orderId));

						return PayfastPaymentStatusEnum.Complete;

					case "FAILED":
						return PayfastPaymentStatusEnum.Failed;

					default:
						return PayfastPaymentStatusEnum.Unknown;
				}



		}

		private void PerformSecurityChecks(NameValueCollection arrPostedVariables, string merchantId, string requestIp)
		{
			// Verify that we are the intended merchant
			string receivedMerchant = arrPostedVariables["merchant_id"];

			if (receivedMerchant != merchantId)

				throw new Exception("Mechant ID mismatch");

			// Verify that the request comes from the payment processor's servers.

			// Get all valid websites from payment processor
			string[] validSites = new string[] { "www.payfast.co.za", "sandbox.payfast.co.za", "w1w.payfast.co.za", "w2w.payfast.co.za" };

			// Get the requesting ip address
			//string requestIp = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

			if (string.IsNullOrEmpty(requestIp))
				throw new Exception("IP address cannot be null");

			// Is address in list of websites
			if (!IsIpAddressInList(validSites, requestIp))
				throw new Exception("IP address invalid");

		}

		private void ValidateData(string site, NameValueCollection arrPostedVariables)
		{

			WebClient webClient = null;

			try
			{
				webClient = new WebClient();

				byte[] responseArray = webClient.UploadValues(site, arrPostedVariables);

				// Get the response and replace the line breaks with spaces
				string result = Encoding.ASCII.GetString(responseArray);

				result = result.Replace("\r\n", " ").Replace("\r", "").Replace("\n", " ");

				// Was the data valid?
				if (result == null || !result.StartsWith("VALID"))
					throw new Exception("Data validation failed");
			}
			catch (Exception ex)
			{
				//Logger.WriteGeneralError(ex);
				throw ex;
			}
			finally
			{
				if (webClient != null)

					webClient.Dispose();

			}
		}
	}
}
