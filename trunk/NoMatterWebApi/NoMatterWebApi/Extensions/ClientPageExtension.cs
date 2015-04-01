using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Extensions
{
	public static class ClientPageExtension
	{
		public static NoMatterWebApiModels.Models.ClientPage ToDomainClientPage(this NoMatterDatabaseModel.ClientPage clientPage)
		{
			if (clientPage == null) return null;

			return new NoMatterWebApiModels.Models.ClientPage
			{
				PageName = clientPage.PageName,
				PageText = clientPage.PageText,
			};
		}

		public static NoMatterDatabaseModel.ClientPage ToDatabaseClientPage(this  NoMatterWebApiModels.Models.ClientPage clientPage, int clientId)
		{
			return new NoMatterDatabaseModel.ClientPage
			{
				ClientId = clientId,
				PageName = clientPage.PageName,
				PageText = clientPage.PageText,
			};
		}
	}
}