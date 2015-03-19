using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NoMatterWebTests.Helpers
{
	public class SessionValueProvider : IValueProvider
	{
		public bool ContainsPrefix(string prefix)
		{
			return HttpContext.Current.Session[prefix] != null;
		}

		public ValueProviderResult GetValue(string key)
		{
			if (HttpContext.Current.Session[key] == null)
				return null;

			return new ValueProviderResult(HttpContext.Current.Session[key],
				HttpContext.Current.Session[key].ToString(), CultureInfo.CurrentCulture);
		}
	}
}
