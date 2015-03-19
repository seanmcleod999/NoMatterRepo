using System;
using System.Web.Mvc;
using SharedLibrary.Helpers;
using SharedLibrary.Logging;
using SharedLibrary.Models;
using SharedLibrary.Services.CategoryService;
using SharedLibrary.ViewModels;

namespace PrettyDamnThriftyWeb.Controllers
{
    public class CategoryController : Controller
    {
		private ICategoryService _categoryService;

		public CategoryController()
		{
			_categoryService = new CategoryService();
		}

		[Authorize(Roles = "Admin")]
		public ActionResult Index()
		{
			var categories = _categoryService.GetCategoriesForEditing();

			return View(categories);
		}

		[Authorize(Roles = "Admin")]
		public ActionResult CategoryAdd()
		{
			var categoryVm = new CategoryVm
			{
				Sections = _categoryService.GetSections(),
				Category = new Category { Order = 1, Hidden = false }
			};

			return View(categoryVm);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost, ValidateInput(false)]
		public ActionResult CategoryAdd(CategoryVm categoryVm)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (categoryVm.File != null) categoryVm.Category.PictureUpload = new UploadedPictureFileBase(categoryVm.File.InputStream, categoryVm.File.FileName);

					_categoryService.AddCategory(categoryVm.Category);

					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					//error msg for failed insert in XML file
					ModelState.AddModelError("", "Error creating record. " + ex.Message);
				}
			}

			categoryVm.Sections = _categoryService.GetSections();

			return View(categoryVm);
		}


		[Authorize(Roles = "Admin")]
		public ActionResult CategoryEdit(short id)
		{
			var categoryVm = new CategoryVm
			{
				Category = _categoryService.GetCategory(id)
			};

			return View(categoryVm);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost, ValidateInput(false)]
		public ActionResult CategoryEdit(CategoryVm categoryVm)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (categoryVm.File != null) categoryVm.Category.PictureUpload = new UploadedPictureFileBase(categoryVm.File.InputStream, categoryVm.File.FileName);

					_categoryService.UpdateCategory(categoryVm.Category);

					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					//error msg for failed insert in XML file
					ModelState.AddModelError("", "Error creating record. " + ex.Message);
				}
			}

			return View(categoryVm);
		}

		[Authorize(Roles = "Admin")]
		public ActionResult CategoryDelete(short id)
		{
			try
			{
				_categoryService.DeleteCategory(id);

				return RedirectToAction("Index");

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}


    }
}
