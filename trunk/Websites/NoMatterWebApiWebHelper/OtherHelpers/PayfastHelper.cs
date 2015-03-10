using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiWebHelper.OtherHelpers
{
	public class PayfastHelper
	{

		public static void PerformSecurityChecks(NameValueCollection arrPostedVariables, string merchantId, string requestIp)
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

		public static bool IsIpAddressInList(string[] validSites, string ipAddress)
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

		public static void ValidateData(string site, NameValueCollection arrPostedVariables)
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
