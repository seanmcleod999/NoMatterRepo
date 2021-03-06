﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GoogleAPI.UrlShortener;
using ImageResizer;
using NoMatterWebApi.Logging;
using NoMatterWebApi.Models;

namespace NoMatterWebApi.Helpers
{
	public interface IGeneralHelper
	{
		Task<FacebookUser> VerifyFacebookTokenAsync(string accessToken);
		string MakeGoogleShortUrl(string accessToken);
		string SaveImage(string ImageData, string clientUuid);
		void DeleteImage(string imagePath);
	}

	public class GeneralHelper : IGeneralHelper
	{

		public async Task<FacebookUser> VerifyFacebookTokenAsync(string accessToken)
		{
			try
			{

				FacebookUser fbUser = null;
				var path = "https://graph.facebook.com/me?access_token=" + Uri.EscapeDataString(accessToken);// + "&appsecret_proof=" + GenerateAppSecretProof(accessToken);
				var client = new HttpClient();
				var uri = new Uri(path);

				var response = await client.GetAsync(uri);

				// todo: proper error handling, check response is valid etc refer to facebook developer documentation

				if (response.IsSuccessStatusCode)
				{
					var content = response.Content.ReadAsStringAsync().Result;
					fbUser = Newtonsoft.Json.JsonConvert.DeserializeObject<FacebookUser>(content);
				}

				return fbUser;

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return null;
			}
		}

		public string SaveImage(string ImageData, string clientUuid)
		{
			byte[] imageData = Convert.FromBase64String(ImageData);

			var path = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/" + clientUuid + "/");

			if (!Directory.Exists(path)) Directory.CreateDirectory(path);

			var imageGuid = Guid.NewGuid().ToString();

			ImageBuilder.Current.Build(
				new ImageJob(
					imageData,
					path + imageGuid,
					new Instructions("maxwidth=1500&maxheight=1500&format=jpg&quality=95"),
					false,
					true));

			var imageId = string.Format("{0}/{1}.jpg", clientUuid, imageGuid);
			return imageId;
		}

		public void DeleteImage(string imagePath)
		{
			if (File.Exists(imagePath))
			{
				try
				{
					File.Delete(imagePath);
				}
				catch (Exception ex)
				{
					Logger.WriteGeneralError(ex);
					//throw;
				}

			}
		}

		//private static string GenerateAppSecretProof(string accessToken)
		//{
		//	string appSecret = GlobalSettings.FacebookAppSecret;

		//	using (HMACSHA256 algorithm = new HMACSHA256(Encoding.ASCII.GetBytes(appSecret)))
		//	{
		//		byte[] hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(accessToken));
		//		StringBuilder builder = new StringBuilder();
		//		for (int i = 0; i < hash.Length; i++)
		//		{
		//			builder.Append(hash[i].ToString("x2", CultureInfo.InvariantCulture));
		//		}
		//		return builder.ToString();
		//	}
		//}

		public string MakeGoogleShortUrl(string productUrl)
		{
			try
			{
				//Generate a google short url
				var googleShortUrlclient = new UrlResource();

				// Shorten url according the parameter below.
				var response = googleShortUrlclient.Insert(new ShortenRequest { LongUrl = productUrl });

				// Get the short url
				return response.Id;

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return "";
			}

		}

		public static string ConvertPicToBase64String(HttpPostedFileBase file)
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

			return imageBase64String;
		}

		public static ClaimsIdentity GenerateClaimsIdentity(string token = null)
		{

			var claims = new List<Claim>
                {
                    new Claim("UserName", token ?? "test"),
                };

			var claimsIdentity = new ClaimsIdentity(claims, "Basic", "UserName", ClaimTypes.Role);

			return claimsIdentity;
		}
	}
}