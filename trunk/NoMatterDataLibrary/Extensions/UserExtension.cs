using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterDataLibrary.Extensions
{
	public static class UserExtension
	{
		public static NoMatterWebApiModels.Models.User ToDomainUser(this   NoMatterDatabaseModel.User user)
		{
			if (user == null) return null;

			return new NoMatterWebApiModels.Models.User()
			{
				Client = user.Client != null ? user.Client.ToDomainClient() : null,
				UserId = user.UserUUID.ToString(),
				Password = user.Password,
				Email = user.Email,
				Fullname=user.FullName,
				ContactNumber=user.ContactNumber,
				Address=user.Address,
				City = user.City,
				Country = user.Country,
				PostalCode=user.PostalCode,
				Province=user.Province,
				Suburb = user.Suburb,
				UserRolesString = String.Join(",", user.UserRoles.Select(x => x.Role.RoleName)),
				//UserRoles = user.UserRoles.Select(x => x.ToModelUserRole()).ToList()
			};
		}	
	}
}