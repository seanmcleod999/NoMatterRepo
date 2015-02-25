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
using System.Web.Routing;
using System.Web.Security;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper;
using NoMatterWebApiWebHelper.Enums;
using NoMatterWebApiWebHelper.OtherHelpers;
using WebApplication7.Logging;
using WebApplication7.Models;
using WebApplication7.ViewModels;

namespace WebApplication7.Controllers
{
    public class ProductController : Controller
    {
		private IProductHelper _productHelper;
		private IPictureHelper _pictureHelper;
		private IGlobalSettings _globalSettings;
		private IPictureUploadSettings _productPictureUploadSettings;

		public ProductController()
		{
			_productHelper = new ProductHelper();
			_pictureHelper = new PictureHelper();
			_globalSettings = new GlobalSettings();
			_productPictureUploadSettings = new PictureUploadSettings(PictureTypeEnum.ShopItemPicture, _globalSettings);
		}

        public async Task<ActionResult> ViewProduct(string productId)
        {
			var product = await _productHelper.GetProductAsync(productId);

	        var viewProductVm = new ViewProductVm
		        {
					Product = product,
		        };

			return View(viewProductVm);
        }

		public async Task<ActionResult> EditProduct(string productId)
		{
			var product = await _productHelper.GetProductNoRelatedProductsAsync(productId);

			var editProductVm = new EditProductVm
			{
				Product = product,
			};

			return View(editProductVm);
		}

		[HttpPost]
		public async Task<ActionResult> EditProduct(EditProductVm editProductVm)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			if (editProductVm.Picture1 != null) editProductVm.Product.Picture1 = _pictureHelper.UploadPicture(Image.FromStream(editProductVm.Picture1.InputStream), _productPictureUploadSettings);
			if (editProductVm.Picture2 != null) editProductVm.Product.Picture2 = _pictureHelper.UploadPicture(Image.FromStream(editProductVm.Picture2.InputStream), _productPictureUploadSettings);
			if (editProductVm.Picture3 != null) editProductVm.Product.Picture3 = _pictureHelper.UploadPicture(Image.FromStream(editProductVm.Picture3.InputStream), _productPictureUploadSettings);
			if (editProductVm.Picture4 != null) editProductVm.Product.Picture4 = _pictureHelper.UploadPicture(Image.FromStream(editProductVm.Picture4.InputStream), _productPictureUploadSettings);
			if (editProductVm.Picture5 != null) editProductVm.Product.Picture5 = _pictureHelper.UploadPicture(Image.FromStream(editProductVm.Picture5.InputStream), _productPictureUploadSettings);
			if (editProductVm.PictureOther != null) editProductVm.Product.PictureOther = _pictureHelper.UploadPicture(Image.FromStream(editProductVm.PictureOther.InputStream), _productPictureUploadSettings);

			//editProductVm.Product.ViewProductUrl = _globalSettings.ShopItemPath;

			var product = await _productHelper.UpdateProductAsync(editProductVm.Product, token);

			return RedirectToAction("GetCategoryProducts", "Category", new { categoryId = editProductVm.Product.CategoryId });
		}

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