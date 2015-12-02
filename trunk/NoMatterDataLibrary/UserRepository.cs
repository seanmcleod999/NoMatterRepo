using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterDataLibrary.Extensions;
using NoMatterDatabaseModel;
using User = NoMatterWebApiModels.Models.User;

namespace NoMatterDataLibrary
{
	public interface IUserRepository
	{
		Task<User> GetClientUserByEmailAsync(Guid? clientUuid, string email);
		Task<User> GetClientUserByFacebookIdAsync(Guid? clientUuid, string email);
		//Task<User> GetClientUserByTokenAsync(string token);
		Task<User> GetUserByUuidAsync(Guid userUuid);
		Task<string> SaveUserAsync(User user);
		Task UpdateUserAsync(User user);
	}

	public class UserRepository : IUserRepository
	{
		public async Task<User> GetClientUserByEmailAsync(Guid? clientUuid, string email)
		{

			using (var mainDb = new DatabaseEntities())
			{
				var userDb = await mainDb.Users.Include(x => x.Client)
					                  .Include(x => x.UserRoles)
					                  .SingleOrDefaultAsync(
						                  x => x.Client.ClientUUID == clientUuid && x.Email == email && x.CredentialTypeId == 1);

				return userDb.ToDomainUser();
			}
		}

		public async Task<User> GetClientUserByFacebookIdAsync(Guid? clientUuid, string facebookIdentifier)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var userDb = await mainDb.Users.Include(x => x.Client).Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.Client.ClientUUID == clientUuid && x.Identifier == facebookIdentifier && x.CredentialTypeId == 2);
				return userDb.ToDomainUser();
			}
			
		}

		//public async Task<User> GetClientUserByTokenAsync(string token)
		//{
		//	using (var mainDb = new DatabaseEntities())
		//	{
		//		var userDb = await mainDb.Users.Include(x => x.Client).SingleOrDefaultAsync(x => x.PasswordSalt == token && (x.CredentialTypeId == 1 || x.CredentialTypeId == 2));
		//		return userDb.ToDomainUser();
		//	}

		//}

		public async Task<User> GetUserByUuidAsync(Guid userUuid)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var userDb = await mainDb.Users.SingleOrDefaultAsync(x => x.UserUUID == userUuid);
				return userDb.ToDomainUser();
			}

		}

		public async Task<string> SaveUserAsync(User user)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var userDb = user.ToDatabaseUser();

				userDb.UserUUID = Guid.NewGuid();
				userDb.DateAdded = DateTime.Now;
				userDb.DateUpdated = DateTime.Now;

				mainDb.Users.Add(userDb);

				await mainDb.SaveChangesAsync();

				return userDb.UserUUID.ToString();

			}		
		}

		public async Task UpdateUserAsync(User user)
		{
			using (var mainDb = new DatabaseEntities())
			{

				//var userDb = user.to

				//mainDb.Users.Attach(user);

				//user.DateUpdated = DateTime.Now;

				await mainDb.SaveChangesAsync();
			}
			

		}
		

	}
}
