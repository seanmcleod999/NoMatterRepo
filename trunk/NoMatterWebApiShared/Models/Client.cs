using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApiModels.Models
{
	public class Client
	{
		public string ClientUuid { get; set; }

		public string ClientName { get; set; }

		public bool Enabled { get; set; }

		public string Logo { get; set; }

		public string SiteUrl { get; set; }

		public string DomainName { get; set; }

		public string FacebookAppId { get; set; }

	}
}