using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Extensions
{
	public static class ClientExtension
	{
		public static NoMatterWebApiModels.Models.Client ToDomainClient(this   NoMatterDatabaseModel.Client client)
		{
			return new NoMatterWebApiModels.Models.Client
			{
				ClientId = client.ClientUUID.ToString(),
				ClientName = client.ClientName,
				Enabled = client.Enabled,
				Logo = client.Logo
			};
		}

		public static NoMatterDatabaseModel.Client ToDatabaseClient(this  NoMatterWebApiModels.Models.Client client)
		{
			return new NoMatterDatabaseModel.Client
			{
				ClientName = client.ClientName,
				Enabled = client.Enabled,
				Logo = client.Logo
			};
		}
	}
}