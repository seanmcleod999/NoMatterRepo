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
using NoMatterWebApiWebHelper.WebApiHelpers;
using WebApplication7.Logging;
using WebApplication7.Models;
using NoMatterWebApiModels.ViewModels;

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
			_productPictureUploadSettings = new PictureUploadSettings(PictureTypeEnum.ProductPicture, _globalSettings);
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