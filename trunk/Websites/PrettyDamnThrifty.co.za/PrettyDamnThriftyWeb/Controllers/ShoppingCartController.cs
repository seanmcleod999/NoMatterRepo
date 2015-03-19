using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharedLibrary.Helpers;
using SharedLibrary.Logging;
using SharedLibrary.Models;
using SharedLibrary.Services;
using SharedLibrary.Services.ShoppingCartService;
using SharedLibrary.ViewModels;

namespace PrettyDamnThriftyWeb.Controllers
{
    public class ShoppingCartController : Controller
    {
		private IShoppingCartService _shoppingCartService;
		private ICurrentUser _currentUser;

		public ShoppingCartController()
		{
			_shoppingCartService = new ShoppingCartService();
			_currentUser = new CurrentUser();
		}

		//Use this constructor when testing to inject the dependency
		public ShoppingCartController(ICurrentUser currentUser)
		{
			_currentUser = currentUser;
			_shoppingCartService = new ShoppingCartService();
		}

		//Use this constructor when testing to inject the dependency
		public ShoppingCartController(IShoppingCartService shoppingCartService, ICurrentUser currentUser)
		{
			_shoppingCartService = shoppingCartService;
			_currentUser = currentUser;
		}

		public ActionResult GetCart()
		{
			var shoppingCart = _shoppingCartService.GetContents(_currentUser.CartId());

			var shoppingCartVm = new ShoppingCartVm {ShoppingCart = shoppingCart};

			return View("GetCart", shoppingCartVm);
		}

		public string AddItemToCart(int shopItemId, int quantity)
		{
			try
			{			
				_shoppingCartService.AddItemToCart(_currentUser.CartId(), shopItemId, quantity);

				Session["CartItemCount"] = _shoppingCartService.GetItemCount(_currentUser.CartId());
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return "False";
			}

			return "Success";
		}

		public JsonResult RemoveItemFromCart(int shopItemId)
		{
			var shoppingCart = _shoppingCartService.RemoveItemFromCart(_currentUser.CartId(), shopItemId);

			if (shoppingCart.ShopItems.Count == 0)
			{
				_currentUser.ClearCartSession();
			}

			Session["CartItemCount"] = shoppingCart.ShopItems.Count;

			// Display the confirmation message
			var results = new ShoppingCartRemoveVm
			{
				Message = "The item has been removed from your shopping cart.",
				CartTotal = shoppingCart.CartTotal,
				DeleteId = shopItemId,
				ItemCount = shoppingCart.ShopItems.Count
			};

			return Json(results);
		}

		public string EmptyCart()
		{
			_shoppingCartService.EmptyCart(_currentUser.CartId());
			_currentUser.ClearCartSession();

			return "Success";
		}
    }
}
