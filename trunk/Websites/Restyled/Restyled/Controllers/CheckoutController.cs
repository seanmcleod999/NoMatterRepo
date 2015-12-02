using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NoMatterWebApiModels.Enums;
using NoMatterWebApiModels.Models;
using NoMatterWebApiModels.ViewModels;
using NoMatterWebApiWebHelper.OtherHelpers;
using NoMatterWebApiWebHelper.WebApiHelpers;
using RestyledLiving.Logging;

namespace RestyledLiving.Controllers
{
	public class CheckoutController : Controller
	{
		private ICartHelper _cartHelper;
		private IUserHelper _userHelper;
		private IClientHelper _clientHelper;
		private IOrderHelper _orderHelper;
		private IPayfastHelper _payfastHelper;

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
			_payfastHelper = new PayfastHelper();

		}

		public ActionResult Index()
		{
			var checkoutVm = new CheckoutVm
			{
				//User. = "South Africa",
				Provinces = GeneralHelper.GetProvinces(),
				User = new UserModel() { Country = "South Africa" }
			};

			return View(checkoutVm);
		}

		[HttpPost]
		public async Task<ActionResult> Index(CheckoutVm checkoutVm)
		{
			//Create or update the user details and get the user Id
			var userId = await _userHelper.CreateOrUpdateUser(_globalSettings.SiteClientId, checkoutVm.User);

			//Create an order for that user


			return RedirectToAction("Postage", new { id = userId });
		}

		public async Task<ActionResult> Postage(string id)
		{
			var checkoutPostageVm = new CheckoutPostageVm
			{
				UserId = id,
				DeliveryOptions = await _clientHelper.GetClientDeliveryOptions(_globalSettings.SiteClientId)
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
				PaymentTypes = await _clientHelper.GetClientPaymentTypes(_globalSettings.SiteClientId)
			};

			return View(checkoutSummaryVm);
		}

		[HttpPost]
		public async Task<ActionResult> ProcessOrder(CheckoutSummaryVm checkoutSummaryVm)
		{

			//var order = await _orderHelper.GetOrderAsync(checkoutSummaryVm.Order.OrderId);

			//and redirect to relevant place depending on payment type
			switch (Convert.ToInt16(checkoutSummaryVm.PaymentType))
			{
				case (short)PaymentTypeEnum.EFT:

					var eftOrder = await _orderHelper.ProcessEftOrderAsync(checkoutSummaryVm.Order.OrderId);

					await _cartHelper.EmptyCartAsync(_currentUser.CartId());
					//Session["CartItemCount"] = 0;

					//Just a notification to the user that the payment was a success
					var eftOrderProcessedVm = new EftOrderProcessedVm
					{
						Order = eftOrder,
						BankDetails = GeneralHelper.GetBankDetails()
					};

					return View("EftPaymentDetails", eftOrderProcessedVm);


				case (short)PaymentTypeEnum.Payfast:

					var payFastOrder = await _orderHelper.UpdateOrderPaymentType(checkoutSummaryVm.Order.OrderId, Convert.ToInt16(checkoutSummaryVm.PaymentType));

					var payFastRedirectUrl = _payfastHelper.GeneratePayfastRedirectUrl(checkoutSummaryVm.Order.OrderId.ToString(), payFastOrder.TotalAmount.ToString("#.##"), _globalSettings);

					return new RedirectResult(payFastRedirectUrl, true);

				 default:
					return View("UnknownPaymentType");

			}
		
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
				Logger.WriteGeneralInformationLog("Recieved PayfastNotifyPayment request..");

				await _payfastHelper.ProcessOrder(Request.Form);

				//var orderId = Convert.ToInt32(Request.Form["m_payment_id"]);

				//Logger.WriteGeneralInformationLog("Order id is... " + orderId);

				//var paymentStatus = PayfastHelper.GetPayfastPaymentStatus(Request.Form, _globalSettings, _currentUser);

				//Logger.WriteGeneralInformationLog("Payment Status is... " + paymentStatus.ToString());

				//var payfastOrder = await _orderHelper.ProcessPayfastOrderAsync(orderId, paymentStatus);

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