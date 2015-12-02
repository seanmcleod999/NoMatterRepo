using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NoMatterWebApiModels.Models;
using NoMatterWebApiModels.ViewModels;
using NoMatterWebApiWebHelper.OtherHelpers;
using NoMatterWebApiWebHelper.WebApiHelpers;
using RestyledLiving.Logging;

namespace RestyledLiving.Controllers
{
	public class CartController : Controller
	{
		private ICartHelper _cartHelper;
		private IGlobalSettings _globalSettings;
		private ICurrentUser _currentUser;

		public CartController()
		{
			_cartHelper = new CartHelper();
			_globalSettings = new GlobalSettings();
			_currentUser = new CurrentUser();
		}

		public async Task<ActionResult> ViewCart()
		{
			var shoppingCartDetails = await _cartHelper.GetCartAsync(_currentUser.CartId());

			return View(shoppingCartDetails);
		}

		public async Task<int> AddProductToCart(int productId, int quantity = 1)
		{


			try
			{
				int cartItemCount = await _cartHelper.AddProductToCartAsync(_currentUser.CartId(), productId, quantity);

				Session["CartItemCount"] = cartItemCount;

				return cartItemCount;
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return 0;
			}


		}

		public async Task<ActionResult> RemoveProductFromCart(int productId)
		{
			int cartItemCount = await _cartHelper.DeleteProductFromCartAsync(_currentUser.CartId(), productId);

			Session["CartItemCount"] = cartItemCount;

			return RedirectToAction("ViewCart");
		}

	}
}