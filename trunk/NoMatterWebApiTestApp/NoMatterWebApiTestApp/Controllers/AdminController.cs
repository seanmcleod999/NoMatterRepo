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
	[Authorize]
    public class AdminController : Controller
    {
	    private IClientHelper _clientHelper;
		private ISectionHelper _sectionHelper;
		private ICategoryHelper _categoryHelper;
		private IProductHelper _productHelper;
		private IGlobalSettings _globalSettings;
		private IPictureHelper _pictureHelper;
		private IPictureUploadSettings _sectionPictureUploadSettings;
		private IPictureUploadSettings _categoryPictureUploadSettings;

		public AdminController()
		{
			_clientHelper = new ClientHelper();
			_sectionHelper = new SectionHelper();
			_categoryHelper = new CategoryHelper();
			_productHelper = new ProductHelper();
			_pictureHelper = new PictureHelper();
			_globalSettings = new GlobalSettings();
			_sectionPictureUploadSettings = new PictureUploadSettings(PictureTypeEnum.SectionPicture, _globalSettings);
			_categoryPictureUploadSettings = new PictureUploadSettings(PictureTypeEnum.CategoryPicture, _globalSettings);
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


		public async Task<ActionResult> Sections(string clientId = null)
		{
			if (string.IsNullOrEmpty(clientId)) clientId = _globalSettings.DefaultClientId;

			var clientSections = await _clientHelper.GetClientSectionsAsync(_globalSettings.DefaultClientId, true, true);

			var clientSectionsVm = new ClientSectionsVm
				{
					ClientId = clientId, 
					Sections = clientSections
				};

			return View(clientSectionsVm);

		}

		public ActionResult SectionAdd(string clientId)
		{
			var addSectionVm = new AddEditSectionVm
			{

				Section = new Section()
					{
						ClientId = clientId,
						Hidden = false
					}
			};

			return View(addSectionVm);

		}

		[HttpPost]
		public async Task<ActionResult> SectionAdd(AddEditSectionVm addEditSectionVm)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			if (addEditSectionVm.Picture != null) addEditSectionVm.Section.Picture = _pictureHelper.UploadPicture(Image.FromStream(addEditSectionVm.Picture.InputStream), _sectionPictureUploadSettings);

			await _sectionHelper.PostSectionAsync(addEditSectionVm.Section, token);

			return RedirectToAction("Sections");
		}

		public async Task<ActionResult> SectionEdit(string sectionId)
		{
			var section = await _sectionHelper.GetSectionAsync(sectionId);

			var addSectionVm = new AddEditSectionVm
			{
				Section = section
			};

			return View(addSectionVm);

		}

		[HttpPost]
		public async Task<ActionResult> SectionEdit(AddEditSectionVm addEditSectionVm)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			if (addEditSectionVm.Picture != null) addEditSectionVm.Section.Picture = _pictureHelper.UploadPicture(Image.FromStream(addEditSectionVm.Picture.InputStream), _sectionPictureUploadSettings);

			await _sectionHelper.UpdateSectionAsync(addEditSectionVm.Section, token);

			return RedirectToAction("Sections");

		}

		public async Task<ActionResult> SectionDelete(string sectionId)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			await _sectionHelper.DeleteSectionAsync(sectionId, token);

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

			return View("Categories", sectionCategoriesVm);

		}

		public ActionResult CategoryAdd(string sectionId)
		{
			var addCategoryVm = new AddEditCategoryVm
			{
				Category = new Category()
					{
						SectionId = sectionId
					}
			};

			return View("CategoryAdd", addCategoryVm);

		}

		[HttpPost]
		public async Task<ActionResult> CategoryAdd(AddEditCategoryVm addCategoryVm)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			if (addCategoryVm.Picture != null) addCategoryVm.Category.Picture = _pictureHelper.UploadPicture(Image.FromStream(addCategoryVm.Picture.InputStream), _categoryPictureUploadSettings);

			await _categoryHelper.PostCategoryAsync(addCategoryVm.Category, token);

			return RedirectToAction("SectionCategories", new {sectionId = addCategoryVm.Category.SectionId});

		}

		public async Task<ActionResult> CategoryEdit(string categoryId)
		{
			var category = await _categoryHelper.GetCategoryAsync(categoryId);

			var addCategoryVm = new AddEditCategoryVm
			{
				Category = category
			};

			return View("CategoryEdit", addCategoryVm);

		}

		[HttpPost]
		public async Task<ActionResult> CategoryEdit(AddEditCategoryVm addCategoryVm)
		{
			//await _categoryHelper.UpdateCategory(addCategoryVm.Category);

			return RedirectToAction("SectionCategories", new { sectionId = addCategoryVm.Category.SectionId });

		}

		public async Task<ActionResult> CategoryDelete(string categoryId, string sectionId)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			await _categoryHelper.DeleteCategoryAsync(categoryId, token);

			return RedirectToAction("SectionCategories", new { sectionId = sectionId} );
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