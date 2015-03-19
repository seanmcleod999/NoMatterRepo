using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NoMatterWebApiWebHelper.OtherHelpers
{
	public class CurrentUser : ICurrentUser
	{

		public virtual int UserId()
		{
			return 0;
		}

		//public virtual string EmailAddress()
		//{
		//	return HttpContext.Current.User.Identity.IsAuthenticated ? ((CustomPrincipal)HttpContext.Current.User).EmailAddress : null;
		//}

		public virtual string Name()
		{
			return HttpContext.Current.User.Identity.Name;
		}

		public bool IsLoggedIn()
		{
			return HttpContext.Current.User.Identity.IsAuthenticated;
		}

		public bool IsGuest()
		{
			return HttpContext.Current.User.IsInRole("Guest");
		}

		public bool IsAdmin()
		{
			return HttpContext.Current.User.Identity.IsAuthenticated && HttpContext.Current.User.IsInRole("Admin");
		}

		public bool IsSuperuser()
		{
			return HttpContext.Current.User.Identity.IsAuthenticated && HttpContext.Current.User.IsInRole("Superuser");
		}

		public string UserHostAddress()
		{
			return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
		}

		public int CartItemCount()
		{
			//if (HttpContext.Current.Session["CartItemCount"] == null)
			//{
			//	var shoppingCartService = new ShoppingCartService();

			//	var itemCount = shoppingCartService.GetItemCount(CartId());

			//	HttpContext.Current.Session["CartItemCount"] = itemCount;

			//	return itemCount;
			//}

			return Convert.ToInt32(HttpContext.Current.Session["CartItemCount"]);
		}

		public string CartId()
		{

			//if (IsLoggedIn()) return UserId().ToString();

			const string cartSessionKey = "CartId";

			if (HttpContext.Current.Session[cartSessionKey] == null)
			{
				//if (UserId().ToString() != "0")
				//{
				//	HttpContext.Current.Session[cartSessionKey] = UserId();
				//}
				//else
				//{
				// Generate a new random GUID using System.Guid class
				var tempCartId = Guid.NewGuid();

				// Send tempCartId back to client as a cookie
				HttpContext.Current.Session[cartSessionKey] = tempCartId.ToString();
				//}
			}
			return HttpContext.Current.Session[cartSessionKey].ToString();
		}

		public void ClearCartSession()
		{
			HttpContext.Current.Session["CartId"] = null;
			HttpContext.Current.Session.Remove("CartId");
			HttpContext.Current.Session["CartItemCount"] = 0;
		}
	}

	public interface ICurrentUser
	{
		int UserId();
		//string EmailAddress();
		string Name();
		bool IsLoggedIn();
		bool IsGuest();
		bool IsAdmin();
		bool IsSuperuser();
		string CartId();
		int CartItemCount();
		string UserHostAddress();
		void ClearCartSession();
	}
}
