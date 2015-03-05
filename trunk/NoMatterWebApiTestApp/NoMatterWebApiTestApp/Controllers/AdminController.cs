using System;
using System.Collections.Generic;
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
using NoMatterWebApiWebHelper.OtherHelpers;
using NoMatterWebApiWebHelper.WebApiHelpers;
using WebApplication7.ViewModels;

namespace WebApplication7.Controllers
{
    public class AdminController : Controller
    {
	    private IClientHelper _clientHelper;
		private ISectionHelper _sectionHelper;
		private ICategoryHelper _categoryHelper;
		private IProductHelper _productHelper;
		private IGlobalSettings _globalSettings;

		public AdminController()
		{
			_clientHelper = new ClientHelper();
			_sectionHelper = new SectionHelper();
			_categoryHelper = new CategoryHelper();
			_productHelper = new ProductHelper();
			_globalSettings = new GlobalSettings();
		}

		public AdminController(IClientHelper clientHelper, ISectionHelper sectionHelper, ICategoryHelper categoryHelper, IProductHelper productHelper, IGlobalSettings globalSettings)
		{
			_clientHelper = clientHelper;
			_sectionHelper = sectionHelper;
			_categoryHelper = categoryHelper;
			_productHelper = productHelper;
			_globalSettings = globalSettings;
		}

        public ActionResult Index()
        {
            return View();
        }

		public async Task<ActionResult> Products()
		{

			string productStatus = "Active";
			string categoryId = null;

			var products = await _productHelper.GetClientProductsAsync(_globalSettings.DefaultClientId, productStatus, categoryId);

			var viewClientProducts = new ViewClientProducts
				{
					ClientProducts = products
				};

			return View(viewClientProducts);
		}


		public async Task<ActionResult> Sections()
		{
			var clientSections = await _clientHelper.GetClientSectionsAsync(_globalSettings.DefaultClientId);

			return View(clientSections);

		}

		public ActionResult AddSection()
		{
			var addSectionVm = new AddSectionVm
			{
				Section = new Section()
			};

			return View("AddEditSection", addSectionVm);

		}

		[HttpPost]
		public ActionResult AddSection(AddSectionVm addSectionVm)
		{


			return RedirectToAction("Sections");

		}


		public async Task<ActionResult> SectionCategories(string sectionId)
		{
			var section = await _sectionHelper.GetSectionAsync(sectionId);

			var sectionCategories = await _sectionHelper.GetSectionCategoriesAsync(sectionId);

			var sectionCategoriesVm = new SectionCategoriesVm
				{
					Section = section, 
					Categories = sectionCategories
				};

			return View(sectionCategoriesVm);

		}

		public ActionResult AddCategory(string sectionId)
		{
			var addCategoryVm = new AddEditCategoryVm
			{
				SectionId = sectionId,
				Category = new Category()
			};

			return View("AddEditCategory", addCategoryVm);

		}

		[HttpPost]
		public ActionResult AddCategory(AddEditCategoryVm addCategoryVm)
		{


			return RedirectToAction("SectionCategories", new {sectionId = addCategoryVm.SectionId});

		}

		public async Task<ActionResult> EditCategory(string categoryId)
		{
			var category = await _categoryHelper.GetCategoryAsync(categoryId);

			var addCategoryVm = new AddEditCategoryVm
			{
				SectionId = category.SectionId,
				Category = category
			};

			return View("AddEditCategory", addCategoryVm);

		}

		[HttpPost]
		public ActionResult EditCategory(AddEditCategoryVm addCategoryVm)
		{


			return RedirectToAction("SectionCategories", new { sectionId = addCategoryVm.SectionId });

		}

		public async Task<ActionResult> CategoryProducts(string categoryId)
		{
			
			var category = await _categoryHelper.GetCategoryAsync(categoryId);

			var section = await _sectionHelper.GetSectionAsync(category.SectionId);

			var categoryProducts = await _categoryHelper.GetCategoryProductsAsync(categoryId);

			var categoryProductsVm = new CategoryProductsVm
			{
				Section = section,
				Category = category,
				CategoryProducts = categoryProducts
			};

			return View(categoryProductsVm);

		}

		public ActionResult AddProduct(string categoryId)
		{
			var addProductVm = new AddProductVm
			{
				CategoryId = categoryId,
				Product = new Product()
			};

			return View(addProductVm);

		}

		[HttpPost]
		public ActionResult AddProduct(AddProductVm addProductVm)
		{


			return RedirectToAction("CategoryProducts", new { categoryId = addProductVm.CategoryId });

		}

    }
}