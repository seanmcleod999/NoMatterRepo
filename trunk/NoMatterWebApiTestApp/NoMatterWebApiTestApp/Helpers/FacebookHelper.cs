using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using WebApplication7.Logging;
using WebApplication7.Models;

namespace WebApplication7.Helpers
{
	public class FacebookHelper
	{

		public static FacebookUser VerifyFacebookToken(string accessToken)
		{
			try
			{
				Logger.WriteGeneralInformationLog("About to GenerateAppSecretProof");
			
				FacebookUser fbUser = null;
				var path = "https://graph.facebook.com/me?access_token=" + Uri.EscapeDataString(accessToken) + "&appsecret_proof=" + GenerateAppSecretProof(accessToken);
				var client = new HttpClient();
				var uri = new Uri(path);

				Logger.WriteGeneralInformationLog("About to connect to FB");

				var response = client.GetAsync(uri).Result;

				Logger.WriteGeneralInformationLog("Gotten the response");

				// todo: proper error handling, check response is valid etc refer to facebook developer documentation

				if (response.IsSuccessStatusCode)
				{
					var content = response.Content.ReadAsStringAsync().Result;
					fbUser = Newtonsoft.Json.JsonConvert.DeserializeObject<FacebookUser>(content);

					fbUser.AccessToken = accessToken;
					fbUser.AvatarUrl = new Uri("https://graph.facebook.com/" + fbUser.Id + "/picture");
				}

				Logger.WriteGeneralInformationLog("returning FB User");

				return fbUser;

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		private static string GenerateAppSecretProof(string accessToken)
		{
			string appSecret = GlobalSettings.FacebookAppSecret;

			using (HMACSHA256 algorithm = new HMACSHA256(Encoding.ASCII.GetBytes(appSecret)))
			{
				byte[] hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(accessToken));
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < hash.Length; i++)
				{
					builder.Append(hash[i].ToString("x2", CultureInfo.InvariantCulture));
				}
				return builder.ToString();
			}
		}

	}
}