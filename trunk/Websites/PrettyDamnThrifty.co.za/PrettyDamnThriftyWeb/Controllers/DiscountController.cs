using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharedLibrary.Logging;
using SharedLibrary.Services;
using SharedLibrary.Services.DiscountService;
using SharedLibrary.ViewModels;

namespace PrettyDamnThriftyWeb.Controllers
{
    public class DiscountController : Controller
    {
		private IDiscountService _discountService;

		public DiscountController()
		{
			_discountService = new DiscountService();
		}

		[Authorize]
		public ActionResult Discounts()
		{
			try
			{
				var discountsVm = new DiscountsVm
				{
					Discounts = _discountService.GetDiscounts()
				};

				return View(discountsVm);

			}
			catch (SharedLibraryServiceException ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		[Authorize]
		public ActionResult AddDiscount()
		{
			try
			{
				var discountVm = new DiscountVm();
;
				discountVm.Discount = new Discount();

				discountVm.Discount.StartDate = DateTime.Today;
				discountVm.Discount.EndDate = DateTime.Today.AddDays(7);
				discountVm.DiscountTypeList = _discountService.GetDiscountTypes();

				return View(discountVm);

			}
			catch (SharedLibraryServiceException ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		[Authorize]
		[HttpPost]
		public ActionResult AddDiscount(DiscountVm discountVm)
		{
			try
			{
				var discountId = _discountService.AddDiscount(discountVm.Discount, discountVm.SelectedDiscountShopItems);

				return RedirectToAction("Discounts");
			}
			catch (SharedLibraryServiceException ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}

		}

		[Authorize]
		public ActionResult EditDiscount(int id)
		{
			try
			{			
				var discountVm = new DiscountVm
					{
						Discount = _discountService.GetDiscountDetails(id)
					};

				return View(discountVm);

			}
			catch (SharedLibraryServiceException ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		[Authorize]
		[HttpPost]
		public ActionResult EditDiscount(DiscountVm discountVm)
		{
			try
			{
				_discountService.UpdateDiscount(discountVm.Discount, discountVm.SelectedDiscountShopItems);

				return RedirectToAction("EditDiscount", new { id = discountVm.Discount.DiscountId });
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}

		}

		[Authorize]
		public ActionResult DeleteDiscount(int id)
		{
			try
			{
				_discountService.DeleteDiscount(id);

				return RedirectToAction("Discounts");
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}

		}

		[Authorize]
		public ActionResult RemoveDiscountShopItem(int id, int discountId)
		{
			try
			{
				_discountService.DeleteDiscountShopItem(discountId, id);
				return RedirectToAction("EditDiscount", new { id = discountId });
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		[Authorize]
		public ActionResult GetDiscountShopItemChoices()
		{
			try
			{
				var shopItems = _discountService.GetDiscountShopItemChoices();
				return PartialView("partialSelectShopItems", shopItems);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

    }
}
