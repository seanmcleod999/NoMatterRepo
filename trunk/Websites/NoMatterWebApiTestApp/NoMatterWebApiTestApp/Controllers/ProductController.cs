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
		private IGlobalSettings _globalSettings;

		public ProductController()
		{
			_productHelper = new ProductHelper();
			_globalSettings = new GlobalSettings();
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

    }
}