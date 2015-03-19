using System;
using System.Web.Mvc;
using SharedLibrary.Helpers;
using SharedLibrary.Logging;
using SharedLibrary.Models;
using SharedLibrary.Services.CategoryService;
using SharedLibrary.Services.UserService;
using SharedLibrary.ViewModels;

namespace PrettyDamnThriftyWeb.Controllers
{
    public class AdminController : Controller
    {

		private ICategoryService _categoryService;
		private IUserService _userService;

		public AdminController()
		{
			_categoryService = new CategoryService();
			_userService = new UserService();
		}


		[Authorize(Roles = "Admin")]
        public ActionResult Index(string userName)
        {

			//ViewBag.FacebookName = userName;

			//using (var mainDb = new DatabaseModelEntities())
			//{
			//	var soldTotal = mainDb.shopitems.Where(x => x.Sold).Sum(x => x.Price);
			//	var soldCount = mainDb.shopitems.Count(x => x.Sold);

			//	ViewBag.SoldTotal = soldTotal;
			//	ViewBag.SoldCount = soldCount;

			//	var reservedTotal = (from b in mainDb.shopitems where b.Reserved && !b.Sold select b.Price).DefaultIfEmpty(0).Sum();
			//	var reservedCount = mainDb.shopitems.Count(x => x.Reserved && !x.Sold);

			//	ViewBag.ReservedTotal = reservedTotal;
			//	ViewBag.ReservedCount = reservedCount;

			//	var otherTotal = (from b in mainDb.shopitems where !b.Reserved && !b.Sold select b.Price).DefaultIfEmpty(0).Sum();
			//	var otherCount = mainDb.shopitems.Count(x => !x.Reserved && !x.Sold);

			//	ViewBag.OtherTotal = otherTotal;
			//	ViewBag.otherCount = otherCount;
			//}

			return View();

        }

		[Authorize(Roles = "Admin")]
		public ActionResult TechnicalStuff()
		{

			return View();
		}

		[Authorize(Roles = "Admin")]
		public ActionResult BrowserDetails()
		{

			return View();
		}

		[Authorize(Roles = "Admin")]
		public ActionResult EditAboutUs()
		{

			var editAboutUsVm = new EditAboutUsVm();

			return View(editAboutUsVm);

		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult EditAboutUs(EditAboutUsVm editAboutUsVm)
		{

			StringTemplateHelper.UpdateStringTemplateText("AboutPageText", editAboutUsVm.AboutUsText);

			return RedirectToAction("Index", "Admin");
		}

		[Authorize(Roles = "Admin")]
		public ActionResult EditFaq(int id)
		{
			var faqVm = new FaqVm(id);
			return View(faqVm);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public ActionResult EditFaq(FaqVm faqVm)
		{
			SiteHelper.EditFaq(faqVm);
			return RedirectToAction("Faq", "Home");
		}

		[Authorize(Roles = "Admin")]
		public ActionResult AddFaq()
		{
			var faqVm = new FaqVm(0);
			return View(faqVm);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public ActionResult AddFaq(FaqVm faqVm)
		{
			SiteHelper.AddFaq(faqVm);
			return RedirectToAction("Faq", "Home");
		}

		[Authorize(Roles = "Admin")]
		public ActionResult DeleteFaq(int id)
		{
			SiteHelper.DeleteFaq(id);
			return RedirectToAction("Faq", "Home");
		}

		[Authorize(Roles = "Admin")]
		public ActionResult FacebookDetails()
		{
			try
			{
				var facebookAlbums = FacebookHelper.GetFacebookAlbums();

				return View(facebookAlbums);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				ViewBag.ErrorMessage = ex.Message;

				return View("FacebookDetailsFailed");
			}
		}

		[Authorize(Roles = "Admin")]
	    public ActionResult ViewEnquiries()
	    {
		    var enquiries = SiteHelper.GetEquiries();
		
			return View(enquiries);			
	    }

		[Authorize(Roles = "Admin")]
		public ActionResult PagesText()
		{
			var stringTemplates = StringTemplateHelper.GetStringTemplates();

			return View(stringTemplates);
		}

		[Authorize(Roles = "Admin")]
		public ActionResult PageTextAdd()
		{
			
			return View();
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public ActionResult PageTextAdd(string stringTemplateId, string stringTemplateText)
		{
			StringTemplateHelper.AddStringTemplateText(stringTemplateId, stringTemplateText);

			return RedirectToAction("PagesText");
		}

		[Authorize(Roles = "Admin")]
		public ActionResult PageTextEdit(string stringTemplateId)
		{
			var stringTemplate = StringTemplateHelper.GetStringTemplate(stringTemplateId);

			return View(stringTemplate);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost, ValidateInput(false)]
		public ActionResult PageTextEdit(string stringTemplateId, string stringTemplateText)
		{
			StringTemplateHelper.UpdateStringTemplateText(stringTemplateId, stringTemplateText);

			return RedirectToAction("PagesText");
		}

		[Authorize(Roles = "Admin")]
		public ActionResult ClearCache()
		{

			DbSettingsStaticCache.LoadDbGlobalSettingsCache();

			SliderStaticCache.LoadSliderStaticCache();


			return RedirectToAction("Index");
		}

    }
}
