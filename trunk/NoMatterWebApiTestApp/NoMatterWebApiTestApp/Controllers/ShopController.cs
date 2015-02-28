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
    public class ShopController : Controller
    {
		private IClientHelper _clientHelper;
		private ISectionHelper _sectionHelper;
		private IProductHelper _productHelper;
		private IPictureHelper _pictureHelper;
		private IGlobalSettings _globalSettings;

		public ShopController()
		{
			_clientHelper = new ClientHelper();
			_sectionHelper = new SectionHelper();
			_productHelper = new ProductHelper();
			_pictureHelper = new PictureHelper();
			_globalSettings = new GlobalSettings();
			
		}

        public async Task<ActionResult> Index()
        {

			var sections = await _clientHelper.GetClientSectionsAsync(_globalSettings.DefaultClientId);

			//var sectionCategories = await _sectionHelper.GetSectionCategoriesAsync(sectionId);

			//var categoryProductsTask = _categoryHelper.GetCategoryProductsAsync(categoryId);

			return View(sections);
        }

	   

    }
}