﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NoMatterWebApiModels.Models
{
	public class SignInResult
	{
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		//Included to show all the available properties, but unused in this sample
		[JsonProperty("token_type")]
		public string TokenType { get; set; }

		[JsonProperty("expires_in")]
		public uint ExpiresIn { get; set; }

		[JsonProperty("userName")]
		public string UserName { get; set; }

		[JsonProperty(".issued")]
		public DateTimeOffset Issued { get; set; }

		[JsonProperty(".expires")]
		public DateTimeOffset Expires { get; set; }

	}
}
