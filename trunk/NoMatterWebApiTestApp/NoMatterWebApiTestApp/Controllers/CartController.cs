using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NoMatterWebApiWebHelper.OtherHelpers;
using NoMatterWebApiWebHelper.WebApiHelpers;
using WebApplication7.Logging;

namespace WebApplication7.Controllers
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

	    public async Task<string> AddProductToCart(string productId, int quantity = 1)
        {
			

		    //ViewBag.ProductId = productId;

			//return View();

			try
			{
				await _cartHelper.AddProductToCartAsync(_currentUser.CartId(), productId, quantity);

				//Session["CartItemCount"] = _shoppingCartService.GetItemCount(_currentUser.CartId());
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return "False";
			}

			return "Success";
        }

		public async Task<ActionResult> RemoveProductFromCart(string productId)
		{
			await _cartHelper.DeleteProductFromCartAsync(_currentUser.CartId(), productId);

			return RedirectToAction("ViewCart");
		}



    }
}