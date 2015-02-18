using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterApiDAL.DatabaseModel;

namespace NoMatterApiDAL.Repositories
{
	public interface IUserRepository
	{
		User GetClientUserByEmail(Guid clientUuid, string email);
		User GetClientUserByFacebookId(Guid clientUuid, string email);
		User GetUserByUuid(Guid userUuid);
		string SaveUser(User user);
	}

	public class UserRepository : IUserRepository
	{
		private DatabaseEntities databaseConnection;

		public UserRepository(DatabaseEntities databaseConnection)
		{
			this.databaseConnection = databaseConnection;
		}

		public User GetClientUserByEmail(Guid clientUuid, string email)
		{
			var user = databaseConnection.Users.SingleOrDefault(x => x.Client.ClientUUID == clientUuid && x.Email == email && x.CredentialTypeId == 1);
			return user;
		}

		public User GetClientUserByFacebookId(Guid clientUuid, string facebookIdentifier)
		{
			var user = databaseConnection.Users.SingleOrDefault(x => x.Client.ClientUUID == clientUuid && x.Identifier == facebookIdentifier && x.CredentialTypeId == 2);
			return user;
		}

		public User GetUserByUuid(Guid userUuid)
		{
			var user = databaseConnection.Users.SingleOrDefault(x => x.UserUUID == userUuid);
			return user;
		}

		public string SaveUser(User user)
		{
			user.UserUUID = Guid.NewGuid();
			user.DateAdded = DateTime.Now;
			user.DateUpdated = DateTime.Now;

			databaseConnection.Users.Add(user);

			databaseConnection.SaveChanges();

			return user.UserUUID.ToString();
		}

		

		public Client GetClient(Guid clientUuid)
		{
			var requestDb = databaseConnection.Clients.SingleOrDefault(x => x.ClientUUID == clientUuid);

			return requestDb;
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
