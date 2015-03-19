using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrettyDamnThriftyWeb.Mailers;
using SharedLibrary.Enums;
using SharedLibrary.Helpers;
using SharedLibrary.Logging;
using SharedLibrary.Models;
using SharedLibrary.Services;
using SharedLibrary.Services.CheckoutService;
using SharedLibrary.Services.ProductService;
using SharedLibrary.Services.ProductService;
using SharedLibrary.Services.ShoppingCartService;
using SharedLibrary.Services.UserService;
using SharedLibrary.ViewModels;

namespace PrettyDamnThriftyWeb.Controllers
{
    public class CheckoutController : Controller
    {
		private ICheckoutService _checkoutService;
		private IUserService _userService;
		private IShoppingCartService _shoppingCartService;
		private IProductService _productService;

		private ICurrentUser _currentUser;
		private IGlobalSettings _globalSettings;

		public CheckoutController()
		{
			_currentUser = new CurrentUser();
			_globalSettings = new GlobalSettings();

			_checkoutService = new CheckoutService();
			_userService = new UserService();			
			_shoppingCartService = new ShoppingCartService();
			_productService = new ProductService();
		}

		[Authorize]
		public ActionResult Index()
		{

			var user = _userService.GetUser(_currentUser.UserId());

			user.Country = "South Africa";
	
			var checkoutCartVm = new CheckoutCartVm
			{
				PaymentTypes = _checkoutService.GetPaymentTypes(),
				User = user,
				};

			return View(checkoutCartVm);
		}

		[HttpPost]
		[Authorize]
		public ActionResult Index(CheckoutCartVm checkoutCartVm)
		{
			try
			{		
				_userService.UpdateUser(checkoutCartVm.User);
	
				var cart = _shoppingCartService.GetContents(_currentUser.CartId());

				var paymentTypeId = checkoutCartVm.SelectedPaymentType;

				var message = checkoutCartVm.Message;

				var shopItemIds = cart.ShopItems.Select(x => x.ShopItemId).ToList();

				var orderResult = _checkoutService.CreateOrder(shopItemIds, _currentUser.UserId(), message, cart.CartTotal, cart.ShippingTotal, paymentTypeId);

				var orderId = orderResult.OrderId;

				//and redirect to relevant place depending on payment type
				switch (paymentTypeId)
				{
					case (short)PaymentTypeEnum.Eft:
						//return RedirectToAction("ProcessEftPayment", new { OrderId = orderId});

						//Process the eft order
						//TODO: move the emailing to the ProcessEftOrder function
						_checkoutService.ProcessEftOrder(orderId);
	
						var bankDetails = _globalSettings.BankDetails;

						var mailer = new PDTMailer();

						//Send an EFT Related email to the user
						mailer.ConfirmEftOrder(orderResult, bankDetails).Send();

						//Send an email to the administrator
						mailer.CustomerOrder(orderResult, _globalSettings.EmailAddressSales).Send();

						//Just a notification to the user that the payment was a success
						var eftPaymentVm = new EftPaymentVm
						{
							Order = orderResult,
							BankDetails = bankDetails
						};

						_shoppingCartService.EmptyCart(_currentUser.CartId());
						Session["CartItemCount"] = 0;

						return View("ProcessEftPayment", eftPaymentVm);

						//break;

					case (short)PaymentTypeEnum.Payfast:
						//return RedirectToAction("ProcessPayfastPayment", new { OrderId = orderId, Total = cart.CartTotal });

						var payFastRedirectUrl = _checkoutService.GeneratePayfastRedirectUrl(orderId, cart.CartTotal.ToString("#.##"));

						return new RedirectResult(payFastRedirectUrl, true);

						//break;
				}

				return View(checkoutCartVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

	
		public ActionResult PayfastPaymentSuccessful()
		{
			_shoppingCartService.EmptyCart(_currentUser.CartId());
			Session["CartItemCount"] = 0;

			return View("PaymentSuccessful");
		}

		public ActionResult PayfastCancelPayment()
		{
			//string orderId = Request.Form["m_payment_id"];

			//ViewBag.OrderId = orderId;

			//update the order and set the order to cancelled
			//TODO: do something to mark the order as cancelled
			var paymentCancelledVm = new PaymentCancelledVm();
			return View("PaymentCancelled", paymentCancelledVm);
		}

		[HttpPost]
		public ActionResult PayfastCancelPayment(PaymentCancelledVm paymentCancelledVm)
		{
			var paymentTypeId = paymentCancelledVm.SelectedPaymentType;

			////and redirect to relevant place depending on payment type
			//switch (paymentTypeId)
			//{
			//	case 1:
			//		return RedirectToAction("ProcessEftPayment", new { OrderId = orderId });

			//	case 2:
			//		return RedirectToAction("ProcessPayfastPayment", new { OrderId = orderId, Total = total });
			//}
			return View("PaymentCancelled", paymentCancelledVm);
		}

		public void PayfastNotifyPayment()
		{
			try
			{
				var order = _checkoutService.ProcessPayfastOrder(Request.Form);

				//Send the emails
				var mailer = new PDTMailer();

				//Send an email response to the user
				mailer.ConfirmPayfastOrder(order).Send();

				//Send an email to the administrator
				mailer.CustomerOrder(order, _globalSettings.EmailAddressSales).Send();

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				// An error occurred.. cant do much because payfast send this request via a different process
			}
		}
    }
}
