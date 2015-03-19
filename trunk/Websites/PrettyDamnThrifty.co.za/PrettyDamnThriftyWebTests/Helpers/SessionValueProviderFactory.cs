using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NoMatterWebTests.Helpers
{
	public class SessionValueProviderFactory : ValueProviderFactory
	{
		public override IValueProvider GetValueProvider
			(ControllerContext controllerContext)
		{
			return new SessionValueProvider();
		}
	}
}
