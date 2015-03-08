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
using WebApplication7.Models;
using NoMatterWebApiModels.ViewModels;

namespace WebApplication7.Controllers
{
    public class ShopController : Controller
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

			//var sections = await _clientHelper.GetClientSectionsAsync(_globalSettings.DefaultClientId);

	        var sections = ClientSectionsStaticCache.GetClientSections();

			return View(sections);
        }

		public async Task<ActionResult> SectionShop(string sectionId)
		{
			//var section = await _sectionHelper.GetSectionAsync(sectionId);
			var section = ClientSectionsStaticCache.GetClientSection(sectionId);

			//Get the categories for the selected section
			//var categories = await _sectionHelper.GetSectionCategoriesAsync(sectionId);
			var categories = await SectionCategoriesSessionCache.GetSectionCategories(_sectionHelper, sectionId);

			//Filter out all the categories with no visible products //TODO: maybe move this to the webapi via querystring parameter
			categories = categories.Where(x => x.VisibleProductCount > 0 || x.Conditional).ToList();
			
			List<Product> products = null;
			Category category = null;

			if (categories.Count > 0)
			{
				//Get the products for the first category
				products = await _categoryHelper.GetCategoryProductsAsync(categories.First().CategoryId);
				category = categories.First();
			}
			
			var categoryShopVm = new CategoryShopVm
				{
					Section = section,
					Category = category,
					Categories = categories, 
					Products = products
				};

			return View("CategoryShop", categoryShopVm);
		}

		public async Task<ActionResult> CategoryShop(string categoryId)
		{
			//TODO: move this to a cache
			//Get the categories details for the selected category
			var category = await _categoryHelper.GetCategoryAsync(categoryId);

			

			//Get all the other categories for this section from the cache
			var categories = await SectionCategoriesSessionCache.GetSectionCategories(_sectionHelper, category.SectionId);

			//TODO: move this to a cache
			//Get the section details from the cache
			//var section = await _sectionHelper.GetSectionAsync(category.SectionId);
			var section = ClientSectionsStaticCache.GetClientSection(category.SectionId);

			//Get the products for the selected category
			var products = (await _categoryHelper.GetCategoryProductsAsync(category.CategoryId));
		
			var categoryShopVm = new CategoryShopVm
			{
				Section = section,
				Category = category,
				Categories = categories,
				Products = products
			};

			return View("CategoryShop", categoryShopVm);
		}

	    public async Task<ActionResult> ViewProduct(string productId, string categoryId = null)
	    {
			var product = await _productHelper.GetProductAsync(productId);

			var viewProductVm = new ViewProductVm
			{
				FromCategoryId = categoryId,
				Product = product,
			};

			return View(viewProductVm);

	    }
    }
}