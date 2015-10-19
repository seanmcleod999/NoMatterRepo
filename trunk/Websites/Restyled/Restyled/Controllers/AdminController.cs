using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NoMatterWebApiModels.Models;
using NoMatterWebApiModels.ViewModels;
using NoMatterWebApiWebHelper.OtherHelpers;
using NoMatterWebApiWebHelper.WebApiHelpers;
using RestyledLiving.Logging;

namespace RestyledLiving.Controllers
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

		public ActionResult ClearCache()
		{
			ClientStaticCache.LoadClientCache();
			ClientSettingsStaticCache.LoadClientSettingsCache();
			ClientSectionsStaticCache.LoadClientSectionsCache();
			ClientSectionsCategoriesStaticCache.LoadClientSectionsCategoriesCache();

			return View("Index");
		}

		//public async Task<ActionResult> Clients()
		//{
		//	var viewClientsVm = new ViewClientsVm
		//	{
		//		Clients = new List<Client>() { new Client() { ClientUuid = _globalSettings.SiteClientId, ClientName = "This Client" } }
		//	};

		//	return View(viewClientsVm);
		//}

	//	public async Task<ActionResult> Products()
	//	{

	//		string productStatus = "Active";
	//		string categoryId = null;

	//		var products = await _productHelper.GetClientProductsAsync(_globalSettings.SiteClientId, productStatus, categoryId);

	//		var viewClientProducts = new ViewClientProductsVm
	//		{
	//			ClientProducts = products
	//		};

	//		return View(viewClientProducts);
	//	}


	//	public async Task<ActionResult> Sections(string clientId = null)
	//	{
	//		if (string.IsNullOrEmpty(clientId)) clientId = _globalSettings.SiteClientId;

	//		var client = await _clientHelper.GetClientAsync(clientId);

	//		var clientSections = await _clientHelper.GetClientSectionsAsync(_globalSettings.SiteClientId, true, true);

	//		var clientSectionsVm = new ClientSectionsVm
	//		{
	//			Client = client,
	//			Sections = clientSections
	//		};

	//		return View(clientSectionsVm);

	//	}

	//	public ActionResult SectionAdd(string clientId)
	//	{
	//		var addSectionVm = new AddEditSectionVm
	//		{

	//			Section = new Section()
	//			{
	//				ClientId = clientId,
	//				Hidden = false
	//			}
	//		};

	//		return View(addSectionVm);

	//	}

	//	[HttpPost]
	//	[ValidateInput(false)]
	//	public async Task<ActionResult> SectionAdd(AddEditSectionVm addEditSectionVm)
	//	{
	//		var token = ((CustomPrincipal)HttpContext.User).Token;

	//		if (addEditSectionVm.Picture != null) addEditSectionVm.Section.Picture = await _imageHelper.UploadImageAsync(_globalSettings.SiteClientId, addEditSectionVm.Picture);

	//		await _clientHelper.PostClientSectionAsync(_globalSettings.SiteClientId, addEditSectionVm.Section, token);

	//		return RedirectToAction("Sections");
	//	}

	//	public async Task<ActionResult> SectionEdit(string sectionId)
	//	{
	//		var section = await _sectionHelper.GetSectionAsync(_globalSettings.SiteClientId, sectionId);

	//		var addSectionVm = new AddEditSectionVm
	//		{
	//			Section = section
	//		};

	//		return View(addSectionVm);

	//	}

	//	[HttpPost]
	//	[ValidateInput(false)]
	//	public async Task<ActionResult> SectionEdit(AddEditSectionVm addEditSectionVm)
	//	{
	//		var token = ((CustomPrincipal)HttpContext.User).Token;

	//		if (addEditSectionVm.Picture != null) addEditSectionVm.Section.Picture = await _imageHelper.UploadImageAsync(_globalSettings.SiteClientId, addEditSectionVm.Picture);

	//		await _sectionHelper.UpdateSectionAsync(_globalSettings.SiteClientId, addEditSectionVm.Section, token);

	//		return RedirectToAction("Sections");

	//	}

	//	public async Task<ActionResult> SectionDelete(string sectionId)
	//	{
	//		var token = ((CustomPrincipal)HttpContext.User).Token;

	//		await _sectionHelper.DeleteSectionAsync(sectionId, token);

	//		return RedirectToAction("Sections");
	//	}

	//	public async Task<ActionResult> SectionCategories(string sectionId)
	//	{
	//		var section = await _sectionHelper.GetSectionAsync(_globalSettings.SiteClientId, sectionId);

	//		var sectionCategories = await _sectionHelper.GetSectionCategoriesAsync(sectionId, true, true);

	//		var sectionCategoriesVm = new SectionCategoriesVm
	//		{
	//			Section = section,
	//			Categories = sectionCategories
	//		};

	//		return View("Categories", sectionCategoriesVm);

	//	}

	//	public ActionResult CategoryAdd(string sectionId)
	//	{
	//		var addCategoryVm = new AddEditCategoryVm
	//		{
	//			Category = new Category()
	//			{
	//				SectionId = sectionId
	//			}
	//		};

	//		return View("CategoryAdd", addCategoryVm);

	//	}

	//	[HttpPost]
	//	[ValidateInput(false)]
	//	public async Task<ActionResult> CategoryAdd(AddEditCategoryVm addCategoryVm)
	//	{
	//		var token = ((CustomPrincipal)HttpContext.User).Token;

	//		if (addCategoryVm.Picture != null) addCategoryVm.Category.Picture = await _imageHelper.UploadImageAsync(_globalSettings.SiteClientId, addCategoryVm.Picture);

	//		await _sectionHelper.PostSectionCategoryAsync(_globalSettings.SiteClientId, addCategoryVm.Category, token);

	//		return RedirectToAction("SectionCategories", new { sectionId = addCategoryVm.Category.SectionId });

	//	}

	//	public async Task<ActionResult> CategoryEdit(string categoryId)
	//	{
	//		var category = await _categoryHelper.GetCategoryAsync(_globalSettings.SiteClientId, categoryId);

	//		var addCategoryVm = new AddEditCategoryVm
	//		{
	//			Category = category
	//		};

	//		return View("CategoryEdit", addCategoryVm);

	//	}

	//	[HttpPost]
	//	[ValidateInput(false)]
	//	public async Task<ActionResult> CategoryEdit(AddEditCategoryVm addCategoryVm)
	//	{
	//		var token = ((CustomPrincipal)HttpContext.User).Token;

	//		await _categoryHelper.UpdateCategoryAsync(addCategoryVm.Section.ClientId, addCategoryVm.Category, token);

	//		return RedirectToAction("SectionCategories", new { sectionId = addCategoryVm.Category.Section.SectionId });

	//	}

	//	public async Task<ActionResult> CategoryDelete(string clientId, string categoryId, string sectionId)
	//	{
	//		var token = ((CustomPrincipal)HttpContext.User).Token;

	//		await _categoryHelper.DeleteCategoryAsync(clientId, categoryId, token);

	//		return RedirectToAction("SectionCategories", new { sectionId = sectionId });
	//	}

	//	public async Task<ActionResult> CategoryProducts(string categoryId)
	//	{

	//		var category = await _categoryHelper.GetCategoryAsync(_globalSettings.SiteClientId, categoryId);

	//		var section = await _sectionHelper.GetSectionAsync(_globalSettings.SiteClientId, category.Section.SectionId);

	//		var categoryProducts = await _categoryHelper.GetCategoryProductsAsync(_globalSettings.SiteClientId, categoryId);

	//		var categoryProductsVm = new CategoryProductsVm
	//		{
	//			//Section = section,
	//			Category = category,
	//			CategoryProducts = categoryProducts
	//		};

	//		return View(categoryProductsVm);

	//	}

	//	public async Task<ActionResult> ProductAdd(string categoryId)
	//	{
	//		var category = await _categoryHelper.GetCategoryAsync(_globalSettings.SiteClientId, categoryId);

	//		var addEditProductVm = new AddProductVm
	//		{
	//			Category = category,
	//			Product = new Product()
	//			{
	//				ReleaseDate = DateTime.Now
	//			}
	//		};

	//		return View(addEditProductVm);

	//	}

	//	[HttpPost]
	//	[ValidateInput(false)]
	//	public async Task<ActionResult> ProductAdd(AddProductVm addEditProductVm)
	//	{
	//		try
	//		{
	//			var token = ((CustomPrincipal)HttpContext.User).Token;

	//			var clientId = _globalSettings.SiteClientId;

	//			if (addEditProductVm.Picture1 != null) addEditProductVm.Product.Picture1 = await _imageHelper.UploadImageAsync(clientId, addEditProductVm.Picture1);
	//			if (addEditProductVm.Picture2 != null) addEditProductVm.Product.Picture2 = await _imageHelper.UploadImageAsync(clientId, addEditProductVm.Picture2);
	//			if (addEditProductVm.Picture3 != null) addEditProductVm.Product.Picture3 = await _imageHelper.UploadImageAsync(clientId, addEditProductVm.Picture3);
	//			if (addEditProductVm.Picture4 != null) addEditProductVm.Product.Picture4 = await _imageHelper.UploadImageAsync(clientId, addEditProductVm.Picture4);
	//			if (addEditProductVm.Picture5 != null) addEditProductVm.Product.Picture5 = await _imageHelper.UploadImageAsync(clientId, addEditProductVm.Picture5);
	//			if (addEditProductVm.PictureOther != null) addEditProductVm.Product.PictureOther = await _imageHelper.UploadImageAsync(clientId, addEditProductVm.PictureOther);


	//			addEditProductVm.Product.ViewProductUrl = _globalSettings.ShopItemPath;

	//			var product = await _categoryHelper.PostCategoryProductAsync(clientId, addEditProductVm.Category.CategoryId, addEditProductVm.Product, token);

	//			return RedirectToAction("CategoryProducts", new
	//			{
	//				categoryId = addEditProductVm.Category.CategoryId
	//			});
	//		}
	//		catch (Exception ex)
	//		{
	//			Logger.WriteGeneralError(ex);
	//			throw;
	//		}


	//	}


	//	[Authorize]
	//	public async Task<ActionResult> ProductEdit(string productId)
	//	{
	//		try
	//		{
	//			var product = await _productHelper.GetProductNoRelatedProductsAsync(productId);

	//			var addProductVm = new AddProductVm
	//			{
	//				Product = product,
	//			};

	//			return View(addProductVm);
	//		}
	//		catch (Exception ex)
	//		{
	//			Logger.WriteGeneralError(ex);
	//			throw;
	//		}

	//	}

	//	[Authorize]
	//	[HttpPost]
	//	public async Task<ActionResult> ProductEdit(AddProductVm addEditProductVm)
	//	{
	//		try
	//		{
	//			var token = ((CustomPrincipal)HttpContext.User).Token;

	//			var clientId = _globalSettings.SiteClientId;

	//			if (addEditProductVm.Picture1 != null) addEditProductVm.Product.Picture1 = await _imageHelper.UploadImageAsync(clientId, addEditProductVm.Picture1);
	//			if (addEditProductVm.Picture2 != null) addEditProductVm.Product.Picture2 = await _imageHelper.UploadImageAsync(clientId, addEditProductVm.Picture2);
	//			if (addEditProductVm.Picture3 != null) addEditProductVm.Product.Picture3 = await _imageHelper.UploadImageAsync(clientId, addEditProductVm.Picture3);
	//			if (addEditProductVm.Picture4 != null) addEditProductVm.Product.Picture4 = await _imageHelper.UploadImageAsync(clientId, addEditProductVm.Picture4);
	//			if (addEditProductVm.Picture5 != null) addEditProductVm.Product.Picture5 = await _imageHelper.UploadImageAsync(clientId, addEditProductVm.Picture5);
	//			if (addEditProductVm.PictureOther != null) addEditProductVm.Product.PictureOther = await _imageHelper.UploadImageAsync(clientId, addEditProductVm.PictureOther);

	//			//editProductVm.Product.ViewProductUrl = _globalSettings.ShopItemPath;

	//			var product = await _productHelper.UpdateProductAsync(clientId, addEditProductVm.Product, token);

	//			return RedirectToAction("CategoryProducts", "Admin", new { categoryId = addEditProductVm.Product.CategoryId });

	//		}
	//		catch (Exception ex)
	//		{
	//			Logger.WriteGeneralError(ex);
	//			throw;
	//		}
	//	}

	//	[Authorize]
	//	public async Task<ActionResult> DeleteProduct(string productId, string categoryId)
	//	{
	//		try
	//		{
	//			var token = ((CustomPrincipal)HttpContext.User).Token;

	//			await _productHelper.DeleteProductAsync(productId, token);

	//			return RedirectToAction("CategoryProducts", "Admin", new { categoryId = categoryId });
	//		}
	//		catch (Exception ex)
	//		{
	//			Logger.WriteGeneralError(ex);
	//			throw;
	//		}

	//	}

	}
}