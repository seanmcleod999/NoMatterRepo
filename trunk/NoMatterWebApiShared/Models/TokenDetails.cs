using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApiModels.Models
{
	public class TokenDetails
	{
		public string AccessToken;
		public DateTimeOffset AccessTokenExpiryUtc;
		public string RfreshToken;
	}
}