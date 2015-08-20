using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using NoMatterWebApiModels.Models;
using NoMatterWebApiModels.ViewModels;
using NoMatterWebApiWebHelper.Logging;
using NoMatterWebApiWebHelper.OtherHelpers;
using NoMatterWebApiWebHelper.WebApiHelpers;

namespace RedOrange.Controllers
{
	public class ShopController : WebApiController
	{
		private readonly ICategoryHelper _categoryHelper;
		private readonly IProductHelper _productHelper;
		private readonly IGlobalSettings _globalSettings;

		public ShopController()
		{
			_categoryHelper = new CategoryHelper();
			_productHelper = new ProductHelper();
			_globalSettings = new GlobalSettings();
		}

		public async Task<ActionResult> Index()
		{
			try
			{	
				//This site only has one section.. so that that sectionId
				var sectionName = _globalSettings.DefaultSectionName;

				//Get the categories for this section from the cache
				var categories = ClientSectionsCategoriesStaticCache.GetSectionCategories(sectionName).OrderBy(x=>x.CategoryOrder).ToList();

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
				//This site only has one section.. so that that sectionId
				var sectionName = _globalSettings.DefaultSectionName;

				//Get all the other categories for this section from the cache
				var categories = ClientSectionsCategoriesStaticCache.GetSectionCategories(sectionName).OrderBy(x => x.CategoryOrder).ToList();

				//Get the selected category
				var category = ClientSectionsCategoriesStaticCache.GetSectionCategoryByName(sectionName, id);

				if (category == null) throw new Exception("Category not found");
				
				//Get the products for the selected category
				var products = await _categoryHelper.GetCategoryProductsAsync(_globalSettings.SiteClientId, category.CategoryId);

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


		//public async Task<ActionResult> ShopCategoryPartial(string id)
		//{
		//	try
		//	{		
		//		ViewBag.PageTitle = "Shop";

		//		var defaultSectionName = _globalSettings.DefaultSectionName;

		//		var category = ClientSectionsCategoriesStaticCache.GetSectionCategoryByName(defaultSectionName, id);

		//		//Get the products for the selected category
		//		var products = await _categoryHelper.GetCategoryProductsAsync(_globalSettings.SiteClientId, category.CategoryId);

		//		var categoryShopVm = new CategoryShopVm
		//		{
		//			//Section = section,
		//			Category = category,
		//			//Categories = categories,
		//			Products = products
		//		};

		//		return PartialView("partialCategoryProducts", categoryShopVm);

		//	}
		//	catch (Exception ex)
		//	{
		//		Logger.WriteGeneralError(ex);
		//		throw;
		//	}
		//}

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
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
			
		}
	}
}