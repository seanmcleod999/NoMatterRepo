using System;
using System.Web.Mvc;
using PrettyDamnThriftyWeb.Enums;
using PrettyDamnThriftyWeb.Mailers;
using SharedLibrary.Enums;
using SharedLibrary.Helpers;
using SharedLibrary.Logging;
using SharedLibrary.Models;
using SharedLibrary.Services.PictureService;
using SharedLibrary.Services.SliderService;
using SharedLibrary.ViewModels;

namespace PrettyDamnThriftyWeb.Controllers
{
    public class HomeController : Controller
    {
		private IGlobalSettings _globalSettings;

		public HomeController()
		{
			_globalSettings = new GlobalSettings();
		}

        public ActionResult Index()
        {
	        var homeVm = new HomeVm();

	        return View(homeVm);
        }


		public ActionResult About()
		{
			var aboutPageText = StringTemplateHelper.GetStringTemplateText("AboutPageText");

			ViewBag.AboutPageText = aboutPageText;

			return View();

		}

		public ActionResult Faq()
		{
			var faqVm = new FaqsVm
				{
					Faqs = SiteHelper.GetFaqs()
				};

			return View(faqVm);			
		}

		public ActionResult ContactUs()
		{
			var contactUsVm = new ContactUsVm();

			return View(contactUsVm);
		}

		[HttpPost]
		public ActionResult ContactUs(ContactUsVm contactUsVm)
		{

			if (!SiteHelper.ValidateEmailAdress(contactUsVm.EmailAddress))
			{
				ViewBag.ErrorMessage = "The supplied email address was invalid (" + contactUsVm.EmailAddress + ").";
				return View("ContactUsFailed");
			}

			try
			{
				//Save the enquiry to the database
				SiteHelper.SaveContactUsDetail(contactUsVm);

				var mailer = new PDTMailer();

				//Send an email response to the user
				mailer.CustomerQueryResponse(contactUsVm).Send();

				//Send an email response to the administrator
				mailer.CustomerQuery(contactUsVm, _globalSettings.EmailAddressInfo).Send();

				return View("ContactUsSuccessful");

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				ViewBag.ErrorMessage = ex.Message;
				return View("ContactUsFailed");
			}
		}

	    public ActionResult Shipping()
	    {
			var aboutPageText = StringTemplateHelper.GetStringTemplateText("ShippingText");

			ViewBag.AboutPageText = aboutPageText;

		    return View();
	    }

	    public ActionResult Returns()
	    {
			var aboutPageText = StringTemplateHelper.GetStringTemplateText("ReturnsText");

			ViewBag.AboutPageText = aboutPageText;

			return View();
	    }

		public ActionResult Payments()
		{
			var aboutPageText = StringTemplateHelper.GetStringTemplateText("PaymentsText");

			ViewBag.AboutPageText = aboutPageText;

			return View();
		}

		public ActionResult SizingGuide()
		{
			var aboutPageText = StringTemplateHelper.GetStringTemplateText("SizingGuideText");

			ViewBag.AboutPageText = aboutPageText;

			return View();
		}

	    

    }
}
