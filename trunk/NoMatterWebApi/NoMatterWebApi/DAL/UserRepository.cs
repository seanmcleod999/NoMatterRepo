using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterDatabaseModel;

namespace NoMatterWebApi.DAL
{
	public interface IUserRepository
	{
		Task<User> GetClientUserByEmailAsync(Guid clientUuid, string email);
		Task<User> GetClientUserByFacebookIdAsync(Guid clientUuid, string email);
		Task<User> GetUserByUuidAsync(Guid userUuid);
		Task<string> SaveUserAsync(User user);
	}

	public class UserRepository : IUserRepository
	{
		private DatabaseEntities databaseConnection;

		public UserRepository(DatabaseEntities databaseConnection)
		{
			this.databaseConnection = databaseConnection;
		}

		public async Task<User> GetClientUserByEmailAsync(Guid clientUuid, string email)
		{
			var user = await databaseConnection.Users.SingleOrDefaultAsync(x => x.Client.ClientUUID == clientUuid && x.Email == email && x.CredentialTypeId == 1);
			return user;
		}

		public async Task<User> GetClientUserByFacebookIdAsync(Guid clientUuid, string facebookIdentifier)
		{
			var user = await databaseConnection.Users.SingleOrDefaultAsync(x => x.Client.ClientUUID == clientUuid && x.Identifier == facebookIdentifier && x.CredentialTypeId == 2);
			return user;
		}

		public async Task<User> GetUserByUuidAsync(Guid userUuid)
		{
			var user = await databaseConnection.Users.SingleOrDefaultAsync(x => x.UserUUID == userUuid);
			return user;
		}

		public async Task<string> SaveUserAsync(User user)
		{
			user.UserUUID = Guid.NewGuid();
			user.DateAdded = DateTime.Now;
			user.DateUpdated = DateTime.Now;

			databaseConnection.Users.Add(user);

			await databaseConnection.SaveChangesAsync();

			return user.UserUUID.ToString();
		}

		public void Save()
		{
			databaseConnection.SaveChanges();
		}

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					databaseConnection.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
