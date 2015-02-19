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
using WebApplication7.Models;
using WebApplication7.ViewModels;

namespace WebApplication7.Controllers
{
    public class CategoryController : Controller
    {
		private ICategoryHelper _categoryHelper;
		private IPictureHelper _pictureHelper;
		private IGlobalSettings _globalSettings;
		private IPictureUploadSettings _productPictureUploadSettings;

		public CategoryController()
		{
			_categoryHelper = new CategoryHelper();
			_pictureHelper = new PictureHelper();
			_globalSettings = new GlobalSettings();
			_productPictureUploadSettings = new PictureUploadSettings(PictureTypeEnum.ShopItemPicture, _globalSettings);
		}

		//public CategoryController(ICurrentUser currentUser)
		//{
		//	_productService = new ProductService();
		//	_currentUser = currentUser;
		//}

        public async Task<ActionResult> GetCategoryProducts(string categoryId)
        {
			var categoryTask = _categoryHelper.GetCategoryAsync(categoryId);

			var categoryProductsTask = _categoryHelper.GetCategoryProductsAsync(categoryId);

			//Wait for all to complete
			await Task.WhenAll(categoryTask, categoryProductsTask);

			var category = await categoryTask;
			var categoryProducts = await categoryProductsTask;

	        var viewCategoryProductsVm = new ViewCategoryProductsVm
		        {
			        Category = category,
			        CategoryProducts = categoryProducts
		        };

			return View(viewCategoryProductsVm);
        }

	    public ActionResult NewCategoryProduct(string categoryId)
	    {
			var newProductVm = new NewProductVm
				{
					CategoryId = categoryId,
					Product = new NewProduct()
						{
							//CategoryId = categoryId, 
							Price = 100,
							ReleaseDate = DateTime.Now.ToShortDateString()
						}
				};

		    return View(newProductVm);
	    }

		[HttpPost]
		public async Task<ActionResult> NewCategoryProduct(NewProductVm newProductVm)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			if (newProductVm.Picture1 != null) newProductVm.Product.Picture1 = _pictureHelper.UploadPicture(Image.FromStream(newProductVm.Picture1.InputStream), _productPictureUploadSettings);
			if (newProductVm.Picture2 != null) newProductVm.Product.Picture2 = _pictureHelper.UploadPicture(Image.FromStream(newProductVm.Picture2.InputStream), _productPictureUploadSettings);
			if (newProductVm.Picture3 != null) newProductVm.Product.Picture3 = _pictureHelper.UploadPicture(Image.FromStream(newProductVm.Picture3.InputStream), _productPictureUploadSettings);
			if (newProductVm.Picture4 != null) newProductVm.Product.Picture4 = _pictureHelper.UploadPicture(Image.FromStream(newProductVm.Picture4.InputStream), _productPictureUploadSettings);
			if (newProductVm.Picture5 != null) newProductVm.Product.Picture5 = _pictureHelper.UploadPicture(Image.FromStream(newProductVm.Picture5.InputStream), _productPictureUploadSettings);
			if (newProductVm.PictureOther != null) newProductVm.Product.PictureOther = _pictureHelper.UploadPicture(Image.FromStream(newProductVm.PictureOther.InputStream), _productPictureUploadSettings);

			newProductVm.Product.ViewProductUrl = _globalSettings.ShopItemPath;

			var product = await _categoryHelper.PostCategoryProductsAsync(newProductVm.CategoryId, newProductVm.Product, token);

			return RedirectToAction("GetCategoryProducts", new {categoryId = newProductVm.CategoryId});
		}

	    public async Task<ActionResult> AddSectionCategory(string sectionId)
        {
            return View();
        }


    }
}