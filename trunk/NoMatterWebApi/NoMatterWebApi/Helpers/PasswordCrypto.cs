using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NoMatterWebApi.Helpers
{
	public class PasswordCrypto
	{
		private const int SALT_LENGTH = 4;

		public static byte[] HashPassword(string password)
		{
			byte[] passwordData = UnicodeEncoding.Unicode.GetBytes(password);

			//create Salt
			byte[] salt = new byte[SALT_LENGTH];
			new System.Security.Cryptography.RNGCryptoServiceProvider().GetNonZeroBytes(salt);

			//merge the two
			byte[] saltyPasswordData = new byte[passwordData.Length + SALT_LENGTH];
			Array.Copy(passwordData, saltyPasswordData, passwordData.Length);
			Array.Copy(salt, 0, saltyPasswordData, passwordData.Length, SALT_LENGTH);

			//compute hash
			System.Security.Cryptography.SHA1Managed sha1 = new System.Security.Cryptography.SHA1Managed();
			passwordData = sha1.ComputeHash(saltyPasswordData);

			//append salt to hash
			byte[] hashResult = new byte[passwordData.Length + SALT_LENGTH];
			Array.Copy(passwordData, hashResult, passwordData.Length);
			Array.Copy(salt, 0, hashResult, passwordData.Length, SALT_LENGTH);

			return hashResult;
		}

		public static bool CheckPassword(byte[] userPasswordHash, string password)
		{
			byte[] passwordData = UnicodeEncoding.Unicode.GetBytes(password);

			//get Salt
			byte[] salt = new byte[SALT_LENGTH];
			for (int i = 0; i < SALT_LENGTH; i++)
			{
				salt[i] = userPasswordHash[userPasswordHash.Length - (SALT_LENGTH - i)];
			}

			//merge the two
			byte[] saltyPasswordData = new byte[passwordData.Length + SALT_LENGTH];
			Array.Copy(passwordData, saltyPasswordData, passwordData.Length);
			Array.Copy(salt, 0, saltyPasswordData, passwordData.Length, SALT_LENGTH);

			//compute hash
			System.Security.Cryptography.SHA1Managed sha1 = new System.Security.Cryptography.SHA1Managed();
			passwordData = sha1.ComputeHash(saltyPasswordData);

			//append salt to hash
			byte[] hashResult = new byte[passwordData.Length + SALT_LENGTH];
			Array.Copy(passwordData, hashResult, passwordData.Length);
			Array.Copy(salt, 0, hashResult, passwordData.Length, SALT_LENGTH);

			for (int i = 0; i < userPasswordHash.Length; i++)
			{
				if (hashResult[i] != userPasswordHash[i])
					return false;
			}
			return true;
		}
	}
}