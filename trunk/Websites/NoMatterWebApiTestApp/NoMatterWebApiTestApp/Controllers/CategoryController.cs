﻿using System;
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
			_productPictureUploadSettings = new PictureUploadSettings(PictureTypeEnum.ProductPicture, _globalSettings);
		}

		//public CategoryController(ICurrentUser currentUser)
		//{
		//	_productService = new ProductService();
		//	_currentUser = currentUser;
		//}

		[Authorize]
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






    }
}