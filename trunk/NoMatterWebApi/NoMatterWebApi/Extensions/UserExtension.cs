using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Extensions
{
	public static class UserExtension
	{
		public static NoMatterWebApiModels.Models.User ToDomainUser(this   NoMatterDatabaseModel.User user)
		{
			if (user == null) return null;

			return new NoMatterWebApiModels.Models.User()
			{
				UserId = user.UserUUID.ToString(),
				Email = user.Email,
				Fullname=user.FullName,
				ContactNumber=user.ContactNumber,
				Address=user.Address,
				City = user.City,
				Country = user.Country,
				PostalCode=user.PostalCode,
				Province=user.Province,
				Suburb = user.Suburb

			};
		}
	}
}