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
    public class ProductController : Controller
    {
		private IProductHelper _productHelper;
		private IGlobalSettings _globalSettings;

		public ProductController()
		{
			_productHelper = new ProductHelper();
			_globalSettings = new GlobalSettings();
		}

		//public CategoryController(ICurrentUser currentUser)
		//{
		//	_productService = new ProductService();
		//	_currentUser = currentUser;
		//}

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

	  
    }
}