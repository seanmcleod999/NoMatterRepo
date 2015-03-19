using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper;
using NoMatterWebApiWebHelper.Enums;
using NoMatterWebApiWebHelper.OtherHelpers;
using NoMatterWebApiWebHelper.WebApiHelpers;
using WebApplication7.Logging;
using NoMatterWebApiModels.ViewModels;

namespace WebApplication7.Controllers
{
    public class CheckoutController : Controller
    {
		private ICartHelper _cartHelper;
		private IUserHelper _userHelper;
		private IClientHelper _clientHelper;
		private IOrderHelper _orderHelper;
		
		private IGlobalSettings _globalSettings;
		private ICurrentUser _currentUser;

		public CheckoutController()
		{
			_cartHelper = new CartHelper();
			_userHelper = new UserHelper();
			_clientHelper = new ClientHelper();
			_orderHelper = new OrderHelper();
			_globalSettings = new GlobalSettings();
			_currentUser = new CurrentUser();
			
		}

		public ActionResult Index()
		{
			var checkoutVm = new CheckoutVm
				{
					//User. = "South Africa",
					Provinces = GeneralHelper.GetProvinces(),
					User = new UserModel() {Country = "South Africa"}
				};

			return View(checkoutVm);
		}

		[HttpPost]
		public async Task<ActionResult> Index(CheckoutVm checkoutVm)
		{
			//Create or update the user details and get the user Id
			var userId = await _userHelper.CreateOrUpdateUser(_globalSettings.DefaultClientId, checkoutVm.User);

			//Create an order for that user


			return RedirectToAction("Postage", new { id = userId });
		}

	    public async Task<ActionResult> Postage(string id)
		{
			var checkoutPostageVm = new CheckoutPostageVm
				{
					UserId = id,
					DeliveryOptions = await _clientHelper.GetClientDeliveryOptions(_globalSettings.DefaultClientId)
				};

			return View(checkoutPostageVm);
		}

		[HttpPost]
		public async Task<ActionResult> Postage(CheckoutPostageVm checkoutPostageVm)
		{
			var userId = checkoutPostageVm.UserId;

			var generateUserOrder = new GenerateUserOrder
				{
					CartId = _currentUser.CartId(),
					Message = checkoutPostageVm.OrderComments,
					ClientDeliveryOptionId = Convert.ToInt16(checkoutPostageVm.DeliveryOption)
				};

			//Create the order without the payment details
			var orderId = await _orderHelper.GenerateUserOrderAsync(checkoutPostageVm.UserId, generateUserOrder);

			return RedirectToAction("Summary", new { id = orderId });
	    }

	    public async Task<ActionResult> Summary(int id)
	    {
		    var checkoutSummaryVm = new CheckoutSummaryVm
			    {
					Order = await _orderHelper.GetOrderAsync(id),
				    PaymentTypes = await _clientHelper.GetClientPaymentTypes(_globalSettings.DefaultClientId)
			    };

		    return View(checkoutSummaryVm);
	    }

		[HttpPost]
		public async Task<ActionResult> Summary(CheckoutSummaryVm checkoutSummaryVm)
		{

			var order = await _orderHelper.GetOrderAsync(checkoutSummaryVm.Order.OrderId);

			//and redirect to relevant place depending on payment type
			switch (Convert.ToInt16(checkoutSummaryVm.PaymentType))
			{
				case (short)PaymentTypeEnum.EFT:
					//return RedirectToAction("ProcessEftPayment", new { OrderId = orderId});

					//Process the eft order
					//TODO: move the emailing to the ProcessEftOrder function
					//_orderHelper.ProcessEftOrder(checkoutSummaryVm.Order.OrderId);

					//var bankDetails = _globalSettings.BankDetails;

					//var mailer = new PDTMailer();

					////Send an EFT Related email to the user
					//mailer.ConfirmEftOrder(orderResult, bankDetails).Send();

					////Send an email to the administrator
					//mailer.CustomerOrder(orderResult, _globalSettings.EmailAddressSales).Send();

					//Just a notification to the user that the payment was a success
					//var eftPaymentVm = new EftPaymentVm
					//{
					//	Order = orderResult,
					//	BankDetails = bankDetails
					//};

					//_cartHelper.EmptyCart(_currentUser.CartId());
					//Session["CartItemCount"] = 0;

					//return View("ProcessEftPayment", eftPaymentVm);
					
					return View("ProcessEftPayment");

				//break;

				case (short)PaymentTypeEnum.Payfast:
					//return RedirectToAction("ProcessPayfastPayment", new { OrderId = orderId, Total = cart.CartTotal });

					var payFastRedirectUrl = _orderHelper.GeneratePayfastRedirectUrl(order.OrderId.ToString(), order.TotalAmount.ToString("#.##"));

					return new RedirectResult(payFastRedirectUrl, true);

				//break;
			}

			return RedirectToAction("Complete");
	    }

		public async Task<ActionResult> PayfastPaymentSuccessful()
		{
			await _cartHelper.EmptyCartAsync(_currentUser.CartId());
			Session["CartItemCount"] = 0;

			return View();
		}

		public ActionResult PayfastCancelPayment()
		{
			//string orderId = Request.Form["m_payment_id"];

			//ViewBag.OrderId = orderId;

			//update the order and set the order to cancelled
			//TODO: do something to mark the order as cancelled
			//var paymentCancelledVm = new PaymentCancelledVm();
			return View();
		}

		public async Task PayfastNotifyPayment()
		{
			try
			{
				var order = await _orderHelper.ProcessPayfastOrderAsync(Request.Form);

				////Send the emails
				//var mailer = new PDTMailer();

				////Send an email response to the user
				//mailer.ConfirmPayfastOrder(order).Send();

				////Send an email to the administrator
				//mailer.CustomerOrder(order, _globalSettings.EmailAddressSales).Send();

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				// An error occurred.. cant do much because payfast send this request via a different process
			}
		}
    }
}