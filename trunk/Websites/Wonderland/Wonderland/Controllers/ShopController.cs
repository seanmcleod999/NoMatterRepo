using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NoMatterWebApiModels.Models;
using NoMatterWebApiModels.ViewModels;
using NoMatterWebApiWebHelper;
using NoMatterWebApiWebHelper.OtherHelpers;
using NoMatterWebApiWebHelper.WebApiHelpers;
using RedOrange.Logging;

namespace RedOrange.Controllers
{
	public class ShopController : WebApiController
	{
		private IClientHelper _clientHelper;
		private ISectionHelper _sectionHelper;
		private ICategoryHelper _categoryHelper;
		private IProductHelper _productHelper;
		//private IPictureHelper _pictureHelper;
		private IGlobalSettings _globalSettings;

		public ShopController()
		{
			_clientHelper = new ClientHelper();
			_sectionHelper = new SectionHelper();
			_categoryHelper = new CategoryHelper();
			_productHelper = new ProductHelper();
			_globalSettings = new GlobalSettings();

		}

		public async Task<ActionResult> Index()
		{
			try
			{	
				var defaultSectionName = _globalSettings.DefaultSectionName;

				var categories = ClientSectionsCategoriesStaticCache.GetSectionCategories(defaultSectionName).OrderBy(x=>x.CategoryOrder).ToList();

				List<Product> products = null;
				Category category = null;

				if (categories.Count > 0)
				{
					//Get the products for the first category
					products = await _categoryHelper.GetCategoryProductsAsync(_globalSettings.SiteClientId, categories.First().CategoryId);
					category = categories.First();
				}

				var categoryShopVm = new CategoryShopVm
				{
					//Section = section,
					Category = category,
					Categories = categories,
					Products = products
				};

				return View("CategoryProducts", categoryShopVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		public async Task<ActionResult> Category(string id)
		{
			try
			{
				var defaultSectionName = _globalSettings.DefaultSectionName;

				var category = ClientSectionsCategoriesStaticCache.GetSectionCategoryByName(defaultSectionName, id);

				//Get all the other categories for this section from the cache
				var categories = ClientSectionsCategoriesStaticCache.GetSectionCategories(defaultSectionName).OrderBy(x => x.CategoryOrder).ToList();

				//Get the products for the selected category
				var products = (await _categoryHelper.GetCategoryProductsAsync(_globalSettings.SiteClientId, category.CategoryId));

				var categoryShopVm = new CategoryShopVm
				{
					//Section = section,
					Category = category,
					Categories = categories,
					Products = products
				};

				return View("CategoryProducts", categoryShopVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}


		public async Task<ActionResult> ShopCategoryPartial(string id)
		{
			try
			{		
				ViewBag.PageTitle = "Shop";

				var defaultSectionName = _globalSettings.DefaultSectionName;

				var category = ClientSectionsCategoriesStaticCache.GetSectionCategoryByName(defaultSectionName, id);

				//Get the products for the selected category
				var products = await _categoryHelper.GetCategoryProductsAsync(_globalSettings.SiteClientId, category.CategoryId);

				var categoryShopVm = new CategoryShopVm
				{
					//Section = section,
					Category = category,
					//Categories = categories,
					Products = products
				};

				return PartialView("partialCategoryProducts", categoryShopVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		public async Task<ActionResult> Product(string id, string category = null)
		{
			try
			{
				ViewBag.PageTitle = "Shop";

				var product = await _productHelper.GetProductAsync(id);

				var viewProductVm = new ViewProductVm
				{
					FromCategory = category,
					Product = product,
				};

				return View("ViewProduct", viewProductVm);
			}
			catch (ApiException ex)
			{
				HandleBadRequest(ex);

				return View("ApiError", ModelState);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return View("GeneralError");
			}
			
		}
	}
}