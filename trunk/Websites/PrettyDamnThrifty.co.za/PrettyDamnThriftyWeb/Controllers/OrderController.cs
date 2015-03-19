using System.Web.Mvc;
using SharedLibrary.Services.CheckoutService;
using SharedLibrary.ViewModels;

namespace PrettyDamnThriftyWeb.Controllers
{
    public class OrderController : Controller
    {
		private ICheckoutService _checkoutService;

		public OrderController()
		{
			_checkoutService = new CheckoutService();
		}

		public ActionResult ViewOrders()
		{
			var viewOrdersVm = new ViewOrdersVm();

			return View(viewOrdersVm);
		}

		public ActionResult OrderDetails(int id)
		{
			var viewOrderVm = new ViewOrderVm
				{
					Order = _checkoutService.GetOrder(id)
				};

			return View(viewOrderVm);
		}

		[HttpPost]
		public ActionResult OrderDetails(ViewOrderVm viewOrderVm)
		{
			//OrderHelper.UpdateOrderDetails(viewOrderVm);

			return RedirectToAction("ViewOrders");

		}

    }
}
