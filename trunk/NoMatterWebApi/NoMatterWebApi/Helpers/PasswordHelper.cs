using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NoMatterWebApi.Helpers
{
	public class PasswordHelper
	{
		public static string CreateSalt()
		{
			var rng = new RNGCryptoServiceProvider();
			var buff = new byte[32];
			rng.GetBytes(buff);

			return Convert.ToBase64String(buff);
		}

		public static string CreatePasswordHash(string pwd, string salt)
		{
			string saltAndPwd = String.Concat(pwd, salt);
			string hashedPwd = CreateHash(saltAndPwd);
			return hashedPwd;
		}

		private static string CreateHash(string value)
		{
			SHA1 algorithm = SHA1.Create();
			byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
			string sh1 = "";
			for (int i = 0; i < data.Length; i++)
			{
				sh1 += data[i].ToString("x2").ToUpperInvariant();
			}
			return sh1;
		}

		// Convert a string to a byte array.
		public static byte[] StrToByteArray(string str)
		{
			var encoding = new UTF8Encoding();
			return encoding.GetBytes(str);
		}
	}
}