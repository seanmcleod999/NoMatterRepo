using System.Collections.Generic;

using System.Net.Mail;
using Mvc.Mailer;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Models;
using NoMatterWebApiModels.Models;


namespace PrettyDamnThriftyWeb.Mailers
{
	public class ApiMailer : MailerBase
	{
		private IGlobalSettings _globalSettings;

		public ApiMailer()
		{
			MasterName = "_Layout";
			_globalSettings = new GlobalSettings();
		}

		//public virtual MvcMailMessage RegisterUser(string emailAddress, string fullname)
		//{
		//	ViewBag.fullname = fullname;

		//	var resources = new Dictionary<string, string>();
		//	resources["logo"] = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/logoemail.png");

		//	return Populate(x =>
		//	{
		//		x.Subject = _globalSettings.SiteNameFriendly + " Registration";
		//		x.ViewName = "UserRegistration";
		//		x.LinkedResources = resources;
		//		x.To.Add(emailAddress);

		//	});
		//}

		//public virtual MvcMailMessage ConfirmPayfastOrder(Order order)
		//{
		//	ViewBag.Order = order;

		//	var resources = new Dictionary<string, string>();
		//	resources["logo"] = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/logoemail.png");

		//	return Populate(x =>
		//	{
		//		x.Subject = _globalSettings.SiteNameFriendly + " Order";
		//		x.ViewName = "ConfirmPayfastOrder";
		//		x.LinkedResources = resources;
		//		x.To.Add(order.User.Email);
		//	});
		//}

		public virtual MvcMailMessage ConfirmEftOrder(string siteFriendlyName, Order order, BankDetails bankDetails,
		                                              string fromEmailAddress, string viewName = "ConfirmEftOrder")
		{
			ViewBag.Order = order;
			ViewBag.BankDetails = bankDetails;

			var resources = new Dictionary<string, string>();

			//TODO: httpContect ?
			//resources["logo"] = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/logoemail.png");

			return Populate(x =>
				{
					x.Subject = siteFriendlyName + " Order";
					x.ViewName = viewName;
					//x.LinkedResources = resources;
					x.To.Add(order.User.Email);
					x.From = new MailAddress(fromEmailAddress);
					x.Sender = new MailAddress(fromEmailAddress);
				});
		}
	}

	//	public virtual MvcMailMessage CustomerOrder(Order order, string salesEmailAddress)
	//	{
	//		ViewBag.Order = order;

	//		var resources = new Dictionary<string, string>();
	//		resources["logo"] = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/logoemail.png");

	//		return Populate(x =>
	//		{
	//			x.Subject = _globalSettings.SiteNameFriendly + " Customer Order";
	//			x.ViewName = "CustomerOrder";
	//			x.LinkedResources = resources;
	//			x.To.Add(salesEmailAddress);
	//		});
	//	}

	//	public virtual MvcMailMessage CustomerQuery(ContactUsVm contactUsVm, string infoEmailAddress)
	//	{
	//		ViewBag.ContactUsVm = contactUsVm;

	//		var resources = new Dictionary<string, string>();
	//		resources["logo"] = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/logoemail.png");

	//		return Populate(x =>
	//		{
	//			x.Subject = _globalSettings.SiteNameFriendly + " Website Enquiry";
	//			x.ViewName = "CustomerQuery";
	//			x.LinkedResources = resources;
	//			x.To.Add(infoEmailAddress);
	//		});
	//	}

	//	public virtual MvcMailMessage CustomerQueryResponse(ContactUsVm contactUsVm)
	//	{
	//		ViewBag.Name = contactUsVm.Name;

	//		var toEmailAddress = new MailAddress(contactUsVm.EmailAddress, contactUsVm.Name);

	//		var resources = new Dictionary<string, string>();
	//		resources["logo"] = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/logoemail.png");

	//		return Populate(x =>
	//		{
	//			x.Subject = _globalSettings.SiteNameFriendly + " Website Enquiry";
	//			x.ViewName = "CustomerQueryResponse";
	//			x.LinkedResources = resources;
	//			x.To.Add(toEmailAddress);
	//		});
	//	}
	//}
}