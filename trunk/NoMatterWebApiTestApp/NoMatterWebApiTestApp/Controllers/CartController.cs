using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NoMatterWebApiWebHelper.OtherHelpers;
using NoMatterWebApiWebHelper.WebApiHelpers;

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

	    public async Task<ActionResult> AddProductToCart(string productId)
        {
			await _cartHelper.AddProductToCartAsync(_currentUser.CartId(), productId, 1);

		    ViewBag.ProductId = productId;

			return View();
        }

		public async Task<ActionResult> RemoveProductFromCart(string productId)
		{
			await _cartHelper.DeleteProductFromCartAsync(_currentUser.CartId(), productId);

			return RedirectToAction("ViewCart");
		}



    }
}