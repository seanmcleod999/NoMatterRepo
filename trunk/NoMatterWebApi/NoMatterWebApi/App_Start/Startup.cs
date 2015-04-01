using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomAuthLib;
using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(NoMatterWebApi.Startup))]

namespace NoMatterWebApi
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			CustomAuthentication.Instance.Initialize(app);
		}
	}
}