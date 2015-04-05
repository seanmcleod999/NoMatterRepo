using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NoMatterWebApiModels.Models;
using NoMatterWebApiModels.ViewModels;
using NoMatterWebApiWebHelper;
using NoMatterWebApiWebHelper.OtherHelpers;
using NoMatterWebApiWebHelper.WebApiHelpers;
using RedOrange.Logging;

namespace RedOrange.Controllers
{
	[Authorize]
	public class AdminController : WebApiController
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

		public ActionResult ClearCache()
		{

			ClientSectionsStaticCache.LoadClientSectionsCache();
			ClientSettingsStaticCache.LoadClientSettingsCache();

			return View("CacheCleared");
		}

		public async Task<ActionResult> Products()
		{
			try
			{

				string productStatus = "Active";
				string categoryId = null;

				var products = await _productHelper.GetClientProductsAsync(_globalSettings.SiteClientId, productStatus, categoryId);

				var viewClientProducts = new ViewClientProductsVm
					{
						ClientProducts = products
					};

				return View(viewClientProducts);

			}
			catch (ApiException ex)
			{
				HandleBadRequest(ex);

				return View("ApiError");
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}


		public async Task<ActionResult> Sections()
		{

			var clientSections = await _clientHelper.GetClientSectionsAsync(_globalSettings.SiteClientId, true, true);

			var clientSectionsVm = new ClientSectionsVm
			{
				//Client = client,
				Sections = clientSections
			};

			return View(clientSectionsVm);

		}

		public ActionResult SectionAdd()
		{
			var addSectionVm = new AddEditSectionVm
			{

				Section = new Section()
				{
					ClientId = _globalSettings.SiteClientId,
					Hidden = false
				}
			};

			return View(addSectionVm);

		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> SectionAdd(AddEditSectionVm addEditSectionVm)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			if (addEditSectionVm.Picture != null) addEditSectionVm.Section.Picture = await _imageHelper.UploadImageAsync(_globalSettings.SiteClientId, addEditSectionVm.Picture);

			await _clientHelper.PostClientSectionAsync(_globalSettings.SiteClientId, addEditSectionVm.Section, token);

			return RedirectToAction("Sections");
		}

		public async Task<ActionResult> SectionEdit(string sectionId)
		{
			var section = await _sectionHelper.GetSectionAsync(_globalSettings.SiteClientId, sectionId);

			var addSectionVm = new AddEditSectionVm
			{
				Section = section
			};

			return View(addSectionVm);

		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> SectionEdit(AddEditSectionVm addEditSectionVm)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			if (addEditSectionVm.Picture != null) addEditSectionVm.Section.Picture = await _imageHelper.UploadImageAsync(_globalSettings.SiteClientId, addEditSectionVm.Picture);

			await _sectionHelper.UpdateSectionAsync(_globalSettings.SiteClientId, addEditSectionVm.Section, token);

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
			var section = await _sectionHelper.GetSectionAsync(_globalSettings.SiteClientId, sectionId);

			var sectionCategories = await _sectionHelper.GetSectionCategoriesAsync(sectionId, true, true);

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
		[ValidateInput(false)]
		public async Task<ActionResult> CategoryAdd(AddEditCategoryVm addCategoryVm)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			if (addCategoryVm.Picture != null) addCategoryVm.Category.Picture = await _imageHelper.UploadImageAsync(_globalSettings.SiteClientId, addCategoryVm.Picture);

			await _sectionHelper.PostSectionCategoryAsync(_globalSettings.SiteClientId, addCategoryVm.Category, token);

			return RedirectToAction("SectionCategories", new { sectionId = addCategoryVm.Category.SectionId });

		}

		public async Task<ActionResult> CategoryEdit(string categoryId)
		{
			var category = await _categoryHelper.GetCategoryAsync(_globalSettings.SiteClientId, categoryId);

			var addCategoryVm = new AddEditCategoryVm
			{
				Category = category
			};

			return View("CategoryEdit", addCategoryVm);

		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> CategoryEdit(AddEditCategoryVm addCategoryVm)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			await _categoryHelper.UpdateCategoryAsync(addCategoryVm.Section.ClientId, addCategoryVm.Category, token);

			return RedirectToAction("SectionCategories", new { sectionId = addCategoryVm.Category.SectionId });

		}

		public async Task<ActionResult> CategoryDelete(string categoryId, string sectionId)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			await _categoryHelper.DeleteCategoryAsync(_globalSettings.SiteClientId, categoryId, token);

			return RedirectToAction("SectionCategories", new { sectionId = sectionId });
		}

		public async Task<ActionResult> CategoryProducts(string categoryId)
		{

			var category = await _categoryHelper.GetCategoryAsync(_globalSettings.SiteClientId, categoryId);

			var section = await _sectionHelper.GetSectionAsync(_globalSettings.SiteClientId, category.SectionId);

			var categoryProducts = await _categoryHelper.GetCategoryProductsAsync(_globalSettings.SiteClientId, categoryId);

			var categoryProductsVm = new CategoryProductsVm
			{
				Section = section,
				Category = category,
				CategoryProducts = categoryProducts
			};

			return View(categoryProductsVm);

		}

		public async Task<ActionResult> ProductAdd(string categoryId)
		{
			var category = ClientSectionsCategoriesStaticCache.GetSectionCategoryById(categoryId);

			var addProductVm = new AddProductVm
			{
				Category = category,
				Product = new Product()
				{
					ReleaseDate = DateTime.Now
				}
			};

			return View(addProductVm);

		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> ProductAdd(AddProductVm editProductVm)
		{
			try
			{
				var token = ((CustomPrincipal)HttpContext.User).Token;

				var clientId = _globalSettings.SiteClientId;

				if (editProductVm.Picture1 != null) editProductVm.Product.Picture1 = await _imageHelper.UploadImageAsync(clientId, editProductVm.Picture1);
				if (editProductVm.Picture2 != null) editProductVm.Product.Picture2 = await _imageHelper.UploadImageAsync(clientId, editProductVm.Picture2);
				if (editProductVm.Picture3 != null) editProductVm.Product.Picture3 = await _imageHelper.UploadImageAsync(clientId, editProductVm.Picture3);
				if (editProductVm.Picture4 != null) editProductVm.Product.Picture4 = await _imageHelper.UploadImageAsync(clientId, editProductVm.Picture4);
				if (editProductVm.Picture5 != null) editProductVm.Product.Picture5 = await _imageHelper.UploadImageAsync(clientId, editProductVm.Picture5);
				if (editProductVm.PictureOther != null) editProductVm.Product.PictureOther = await _imageHelper.UploadImageAsync(clientId, editProductVm.PictureOther);


				editProductVm.Product.ViewProductUrl = _globalSettings.ShopItemPath;

				await _categoryHelper.PostCategoryProductAsync(clientId, editProductVm.Category.CategoryId, editProductVm.Product, token);

				return RedirectToAction("CategoryProducts", new
				{
					categoryId = editProductVm.Category.CategoryId
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

				var category = ClientSectionsCategoriesStaticCache.GetSectionCategoryById(product.CategoryId);

				var editProductVm = new EditProductVm
				{
					Category = category,
					Product = product,
				};

				return View(editProductVm);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}

		}

		[Authorize]
		[HttpPost]
		public async Task<ActionResult> ProductEdit(EditProductVm editProductVm)
		{
			try
			{
				var token = ((CustomPrincipal)HttpContext.User).Token;

				var clientId = _globalSettings.SiteClientId;

				if (editProductVm.Picture1 != null) editProductVm.Product.Picture1 = await _imageHelper.UploadImageAsync(clientId, editProductVm.Picture1);
				if (editProductVm.Picture2 != null) editProductVm.Product.Picture2 = await _imageHelper.UploadImageAsync(clientId, editProductVm.Picture2);
				if (editProductVm.Picture3 != null) editProductVm.Product.Picture3 = await _imageHelper.UploadImageAsync(clientId, editProductVm.Picture3);
				if (editProductVm.Picture4 != null) editProductVm.Product.Picture4 = await _imageHelper.UploadImageAsync(clientId, editProductVm.Picture4);
				if (editProductVm.Picture5 != null) editProductVm.Product.Picture5 = await _imageHelper.UploadImageAsync(clientId, editProductVm.Picture5);
				if (editProductVm.PictureOther != null) editProductVm.Product.PictureOther = await _imageHelper.UploadImageAsync(clientId, editProductVm.PictureOther);

				//editProductVm.Product.ViewProductUrl = _globalSettings.ShopItemPath;

				await _productHelper.UpdateProductAsync(clientId, editProductVm.Product, token);

				return RedirectToAction("CategoryProducts", "Admin", new { categoryId = editProductVm.Product.CategoryId });

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		[Authorize]
		public async Task<ActionResult> DeleteProduct(string productId, string categoryId)
		{
			try
			{
				var token = ((CustomPrincipal)HttpContext.User).Token;

				await _productHelper.DeleteProductAsync(productId, token);

				return RedirectToAction("CategoryProducts", "Admin", new { categoryId = categoryId });
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}

		}

	}
}