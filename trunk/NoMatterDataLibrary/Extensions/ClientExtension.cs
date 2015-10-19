using NoMatterWebApiModels.Models;

namespace NoMatterDataLibrary.Extensions
{
	public static class ClientExtension
	{
		public static Client ToDomainClient(this   NoMatterDatabaseModel.Client client)
		{
			return new Client
			{
				ClientUuid = client.ClientUUID.ToString(),
				ClientName = client.ClientName,
				Enabled = client.Enabled,
				Logo = client.Logo,
				SiteUrl = client.SiteUrl,
				DomainName = client.DomainName,
				FacebookAppId = client.FacebookAppId
			};
		}

		public static NoMatterDatabaseModel.Client ToDatabaseClient(this Client client)
		{
			return new NoMatterDatabaseModel.Client
			{
				ClientName = client.ClientName,
				Enabled = client.Enabled,
				Logo = client.Logo,
				SiteUrl = client.SiteUrl,
				DomainName = client.DomainName
			};
		}
	}
}