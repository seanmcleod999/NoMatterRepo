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
	[Authorize]
    public class AdminController : Controller
    {
	    private IClientHelper _clientHelper;
		private ISectionHelper _sectionHelper;
		private ICategoryHelper _categoryHelper;
		private IProductHelper _productHelper;
		private IGlobalSettings _globalSettings;
		private IImageHelper _imageHelper;

		public AdminController()
		{
			_clientHelper = new ClientHelper();
			_sectionHelper = new SectionHelper();
			_categoryHelper = new CategoryHelper();
			_productHelper = new ProductHelper();
			_imageHelper = new ImageHelper();
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

		public async Task<ActionResult> Clients()
		{
			var viewClientsVm = new ViewClientsVm
				{
					Clients = new List<Client>() {new Client() {ClientId = _globalSettings.DefaultClientId, ClientName = "This Client"}}
				};

			return View(viewClientsVm);
		}

		public async Task<ActionResult> Products()
		{

			string productStatus = "Active";
			string categoryId = null;

			var products = await _productHelper.GetClientProductsAsync(_globalSettings.DefaultClientId, productStatus, categoryId);

			var viewClientProducts = new ViewClientProductsVm
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

			if (addEditSectionVm.Picture != null) addEditSectionVm.Section.Picture = await _imageHelper.UploadImageAsync(addEditSectionVm.Picture);

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

			if (addEditSectionVm.Picture != null) addEditSectionVm.Section.Picture = await _imageHelper.UploadImageAsync(addEditSectionVm.Picture);

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

			if (addCategoryVm.Picture != null) addCategoryVm.Category.Picture = await _imageHelper.UploadImageAsync(addCategoryVm.Picture);

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

		public ActionResult ProductAdd(string categoryId)
		{
			var addEditProductVm = new AddEditProductVm
			{
				CategoryId = categoryId,
				Product = new Product()
					{
						ReleaseDate = DateTime.Now
					}
			};

			return View(addEditProductVm);

		}

		[HttpPost]
		public async Task<ActionResult> ProductAdd(AddEditProductVm addEditProductVm)
		{
			try
			{
				var token = ((CustomPrincipal)HttpContext.User).Token;

				if (addEditProductVm.Picture1 != null) addEditProductVm.Product.Picture1 = await _imageHelper.UploadImageAsync(addEditProductVm.Picture1);
				if (addEditProductVm.Picture2 != null) addEditProductVm.Product.Picture2 = await _imageHelper.UploadImageAsync(addEditProductVm.Picture2);
				if (addEditProductVm.Picture3 != null) addEditProductVm.Product.Picture3 = await _imageHelper.UploadImageAsync(addEditProductVm.Picture3);
				if (addEditProductVm.Picture4 != null) addEditProductVm.Product.Picture4 = await _imageHelper.UploadImageAsync(addEditProductVm.Picture4);
				if (addEditProductVm.Picture5 != null) addEditProductVm.Product.Picture5 = await _imageHelper.UploadImageAsync(addEditProductVm.Picture5);
				if (addEditProductVm.PictureOther != null) addEditProductVm.Product.PictureOther = await _imageHelper.UploadImageAsync(addEditProductVm.PictureOther);


				addEditProductVm.Product.ViewProductUrl = _globalSettings.ShopItemPath;

				var product = await _categoryHelper.PostCategoryProductsAsync(addEditProductVm.CategoryId, addEditProductVm.Product, token);

				return RedirectToAction("CategoryProducts", new
				{
					categoryId = addEditProductVm.CategoryId
				});
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
			

		}


		[Authorize]
		public async Task<ActionResult> ProductEdit(string productId)
		{
			try
			{
				var product = await _productHelper.GetProductNoRelatedProductsAsync(productId);

				var addEditProductVm = new AddEditProductVm
				{
					Product = product,
				};

				return View(addEditProductVm);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
			
		}

		[Authorize]
		[HttpPost]
		public async Task<ActionResult> ProductEdit(AddEditProductVm addEditProductVm)
		{
			try
			{
				var token = ((CustomPrincipal)HttpContext.User).Token;

				if (addEditProductVm.Picture1 != null) addEditProductVm.Product.Picture1 = await _imageHelper.UploadImageAsync(addEditProductVm.Picture1);
				if (addEditProductVm.Picture2 != null) addEditProductVm.Product.Picture2 = await _imageHelper.UploadImageAsync(addEditProductVm.Picture2);
				if (addEditProductVm.Picture3 != null) addEditProductVm.Product.Picture3 = await _imageHelper.UploadImageAsync(addEditProductVm.Picture3);
				if (addEditProductVm.Picture4 != null) addEditProductVm.Product.Picture4 = await _imageHelper.UploadImageAsync(addEditProductVm.Picture4);
				if (addEditProductVm.Picture5 != null) addEditProductVm.Product.Picture5 = await _imageHelper.UploadImageAsync(addEditProductVm.Picture5);
				if (addEditProductVm.PictureOther != null) addEditProductVm.Product.PictureOther = await _imageHelper.UploadImageAsync(addEditProductVm.PictureOther);
			
				//editProductVm.Product.ViewProductUrl = _globalSettings.ShopItemPath;

				var product = await _productHelper.UpdateProductAsync(addEditProductVm.Product, token);

				return RedirectToAction("GetCategoryProducts", "Category", new { categoryId = addEditProductVm.Product.CategoryId });

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		[Authorize]
		public async Task<ActionResult> DeleteProduct(string productId, string clientId)
		{
			try
			{
				var token = ((CustomPrincipal)HttpContext.User).Token;

				await _productHelper.DeleteProductAsync(productId, token);

				return RedirectToAction("GetCategoryProducts", "Category", new { clientId = clientId });
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}

		}

    }
}