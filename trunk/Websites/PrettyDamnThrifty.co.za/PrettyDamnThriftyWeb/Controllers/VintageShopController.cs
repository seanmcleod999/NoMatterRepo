using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SharedLibrary.Logging;
using SharedLibrary.Models;
using SharedLibrary.Services.CategoryService;
using SharedLibrary.Services.ProductService;
using SharedLibrary.ViewModels;

namespace PrettyDamnThriftyWeb.Controllers
{
    public class VintageShopController : Controller
    {
        private IProductService _productService;
		private ICategoryService _categoryService;

	    private ICurrentUser _currentUser;

	    private short _sectionId = 2;

		public VintageShopController()
		{
			_productService = new ProductService();
			_categoryService = new CategoryService();
			_currentUser = new CurrentUser();
		}


		public ActionResult Index()
        {
        	try
        	{
        		return RedirectToAction("Latest");
        	}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
        }

		public ActionResult Latest(int page = 1)
		{

			Session["SectionId"] = _sectionId;

			ViewBag.PageTitle = "Vintage Store";

			try
			{
				var result = _productService.GetLatestShopItemsForDisplay(_sectionId, page);

				//Store the list of Ids int session varibale for the next/prev functionality
				if (result.ShopItemIds != null)
				{
					Session["ShopItemIds"] = result.ShopItemIds;
				}
				else
				{
					Session.Remove("ShopItemIds");
				}

				var viewLatestShopItemsVm = new ViewLatestShopItemsVm
					{
						ShopItems = result.ShopItems,
						ShopItemIds = result.ShopItemIds,
						HasNextPage = result.HasNextPage,
						HasPreviousPage = result.HasPreviousPage,
						PageCount = result.PageCount,
						PageNumber = result.PageNumber,
						//CategoriesForDisplay = _categoryService.GetCategoriesForDisplay(_sectionId),
						Category =  _categoryService.GetCategoryByActionName(_sectionId, "Latest")
					};

				return View("LatestShopItems", viewLatestShopItemsVm);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		public ActionResult Category(string id)
		{
			try
			{

				Session["SectionId"] = _sectionId;

				var getCategoryShopItemsForDisplayResult = _productService.GetCategoryShopItemsForDisplay(_sectionId, id);

				var category = _categoryService.GetCategoryByName(_sectionId, id);

				ViewBag.PageTitle = category.SectionName + " " + category.CategoryName;

				var viewShopItemsVm = new ViewShopItemsVm
					{
						ShopItems = getCategoryShopItemsForDisplayResult.ShopItems,
						Category = category,
						//CategoriesForDisplay = _categoryService.GetCategoriesForDisplay(_sectionId)
					};

				

				//Store the list of Ids int session varibale for the next/prev functionality
				if (getCategoryShopItemsForDisplayResult.ShopItemIds != null)
				{
					Session["ShopItemIds"] = getCategoryShopItemsForDisplayResult.ShopItemIds;
				}
				else
				{
					Session.Remove("ShopItemIds");
				}

				return View("ViewShopItems", viewShopItemsVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		public ActionResult Sale()
		{
			try
			{
				Session["SectionId"] = _sectionId;

				var category = _categoryService.GetCategoryByActionName(_sectionId, "Sale");

				ViewBag.PageTitle = category.SectionName + " Sale Items";

				var result = _productService.GetSaleShopItemsForDisplay(_sectionId);

				var viewShopItemsVm = new ViewShopItemsVm
				{
					ShopItems = result.ShopItems,
					Category = category,
					//CategoriesForDisplay = _categoryService.GetCategoriesForDisplay(_sectionId)
				};

				//Store the list of Ids int session varibale for the next/prev functionality
				if (result.ShopItemIds != null)
				{
					Session["ShopItemIds"] = result.ShopItemIds;
				}
				else
				{
					Session.Remove("ShopItemIds");
				}

				return View("ViewShopItems", viewShopItemsVm);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

	
		public ActionResult Item(int id, int lastId = 0, string fromLink = null, string lastAction = null, int lastPage = 0, string category = null)
		{
			List<int> shopItemIds = null;

			//Get the shopitemids list so that we can figure out which one it was
			if (Session["ShopItemIds"] != null)
			{
				shopItemIds = (List<int>)Session["ShopItemIds"];
			}

			var result = _productService.GetShopItemForDisplay(id, shopItemIds, _currentUser.IsAdmin());

			if (result.ShopItem == null) return View("ItemNotFound");

			if (result.ShopItem.Hidden && String.IsNullOrEmpty(User.Identity.Name)) return View("ItemNotFound");

			//Store the list of Ids int session varibale for the next/prev functionality
			if (result.ShopItem.RelatedShopItemDetails.RelatedShopItemIds != null)
			{
				Session["RelatedShopItemIds"] = result.ShopItem.RelatedShopItemDetails.RelatedShopItemIds;
			}
			else
			{
				Session.Remove("RelatedShopItemIds");
			}

			var viewShopItemVm = new ViewShopItemVm
				{
					ShopItem = result.ShopItem,
					LastId = lastId == 0 ? id : lastId,
					LastAction = lastAction,
					LastPage = lastPage,
					LastCatgeory=category,
					FromLink = fromLink,
					RelatedShopItems=result.ShopItem.RelatedShopItemDetails.RelatedShopItems,
					ShopItemPagingDetails = new ShopItemPagingDetails()
						{
							CurrentItemNumber = result.CurrentItemNumber,
							ItemCount = result.ItemCount,
							NextShopItemId = result.NextShopItemId,
							PreviousShopItemId = result.PreviousShopItemId,
						}
				};

			return View("ViewShopItem", viewShopItemVm);
		}

    }
}
