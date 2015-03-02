﻿using System;
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
using WebApplication7.Models;
using WebApplication7.ViewModels;

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
					Suburbs = GeneralHelper.GetSuburbs(),
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

			var generateUserOrder = new GenerateUserOrder();

			generateUserOrder.CartId = _currentUser.CartId();
			generateUserOrder.Message = checkoutPostageVm.OrderComments;
			generateUserOrder.ClientDeliveryOptionId = Convert.ToInt16(checkoutPostageVm.DeliveryOption);

			//Create the order without the payment details
			var orderId = await _orderHelper.GenerateUserOrder(checkoutPostageVm.UserId, generateUserOrder);

			return RedirectToAction("Summary", new { id = orderId });
	    }

	    public async Task<ActionResult> Summary(string id)
	    {
		    var checkoutSummaryVm = new CheckoutSummaryVm
			    {
				    ShoppingCartDetails = await _cartHelper.GetCartAsync(_currentUser.CartId()),
				    PaymentTypes = await _clientHelper.GetClientPaymentTypes(_globalSettings.DefaultClientId)
			    };

		    return View(checkoutSummaryVm);
	    }

		[HttpPost]
		public async Task<ActionResult> Summary(CheckoutSummaryVm checkoutSummaryVm)
	    {
			return RedirectToAction("Complete");
	    }
    }
}