using System.Collections.Generic;

using System.Net.Mail;
using Mvc.Mailer;
using SharedLibrary.DatabaseModel;
using SharedLibrary.Helpers;
using SharedLibrary.Models;
using SharedLibrary.Services;
using SharedLibrary.Services.CheckoutService;
using SharedLibrary.ViewModels;

namespace PrettyDamnThriftyWeb.Mailers
{ 
    public class PDTMailer : MailerBase 	
	{
		private IGlobalSettings _globalSettings;

		public PDTMailer()
		{
			MasterName="_Layout";
			_globalSettings = new GlobalSettings();
		}

		public virtual MvcMailMessage RegisterUser(string emailAddress, string fullname)
		{
			ViewBag.fullname = fullname;
			
			var resources = new Dictionary<string, string>();
			resources["logo"] = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/logoemail.png");

			return Populate(x =>
			{
				x.Subject = _globalSettings.SiteNameFriendly + " Registration";
				x.ViewName = "UserRegistration";
				x.LinkedResources = resources;
				x.To.Add(emailAddress);
			});
		}

		public virtual MvcMailMessage ConfirmPayfastOrder(Order order)
		{
			ViewBag.Order = order;

			var resources = new Dictionary<string, string>();
			resources["logo"] = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/logoemail.png");

			return Populate(x =>
			{
				x.Subject = _globalSettings.SiteNameFriendly + " Order";
				x.ViewName = "ConfirmPayfastOrder";
				x.LinkedResources = resources;
				x.To.Add(order.User.Email);
			});
		}

		public virtual MvcMailMessage ConfirmEftOrder(Order order, BankDetails bankDetails)
		{
			ViewBag.Order = order;
			ViewBag.BankDetails = bankDetails;

			var resources = new Dictionary<string, string>();
			resources["logo"] = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/logoemail.png");

			return Populate(x =>
			{
				x.Subject = _globalSettings.SiteNameFriendly + " Order";
				x.ViewName = "ConfirmEftOrder";
				x.LinkedResources = resources;
				x.To.Add(order.User.Email);
			});
		}

		public virtual MvcMailMessage CustomerOrder(Order order, string salesEmailAddress)
		{
			ViewBag.Order = order;

			var resources = new Dictionary<string, string>();
			resources["logo"] = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/logoemail.png");

			return Populate(x =>
			{
				x.Subject = _globalSettings.SiteNameFriendly + " Customer Order";
				x.ViewName = "CustomerOrder";
				x.LinkedResources = resources;
				x.To.Add(salesEmailAddress);
			});
		}

		public virtual MvcMailMessage CustomerQuery(ContactUsVm contactUsVm, string infoEmailAddress)
		{
			ViewBag.ContactUsVm = contactUsVm;

			var resources = new Dictionary<string, string>();
			resources["logo"] = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/logoemail.png");

			return Populate(x =>
			{
				x.Subject = _globalSettings.SiteNameFriendly + " Website Enquiry";
				x.ViewName = "CustomerQuery";
				x.LinkedResources = resources;
				x.To.Add(infoEmailAddress);
			});
		}

		public virtual MvcMailMessage CustomerQueryResponse(ContactUsVm contactUsVm)
		{
			ViewBag.Name = contactUsVm.Name;

			var toEmailAddress = new MailAddress(contactUsVm.EmailAddress, contactUsVm.Name);

			var resources = new Dictionary<string, string>();
			resources["logo"] = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/logoemail.png");

			return Populate(x =>
			{
				x.Subject = _globalSettings.SiteNameFriendly + " Website Enquiry";
				x.ViewName = "CustomerQueryResponse";
				x.LinkedResources = resources;
				x.To.Add(toEmailAddress);
			});
		}
 	}
}