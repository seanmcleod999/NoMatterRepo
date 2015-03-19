using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrettyDamnThriftyWeb.Providers
{
	public interface IAuthProvider
	{
		bool IsLoggedIn { get; }
		bool Login(string username, string password);
		void Logout();
	}
}