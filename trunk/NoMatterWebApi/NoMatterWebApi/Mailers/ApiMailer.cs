using System.Collections.Generic;
using System.Net.Mail;
using Mvc.Mailer;
using NoMatterWebApi.Helpers;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApi.Mailers
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

		public virtual MvcMailMessage CustomerPaidOrder(string clientUuid, string siteFriendlyName, Order order, string salesEmailAddress, string viewName = "CustomerPaidOrder")
		{
			ViewBag.Order = order;
			ViewBag.SiteFriendlyName = siteFriendlyName;
			ViewBag.SalesEmailAddress = salesEmailAddress;

			var resources = new Dictionary<string, string>();

			resources["logo"] = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/" + clientUuid + "/logo.jpg");

			return Populate(x =>
			{
				x.Subject = siteFriendlyName + " Order";
				x.ViewName = viewName;
				x.LinkedResources = resources;
				x.To.Add(order.User.Email);
				x.From = new MailAddress(salesEmailAddress);
				x.Sender = new MailAddress(salesEmailAddress);
			});
		}

		public virtual MvcMailMessage CustomerEftOrder(string clientUuid, string siteFriendlyName, Order order, BankDetails bankDetails,
													  string salesEmailAddress, string viewName = "CustomerEftOrder")
		{
			ViewBag.Order = order;
			ViewBag.BankDetails = bankDetails;
			ViewBag.SiteFriendlyName = siteFriendlyName;
			ViewBag.SalesEmailAddress = salesEmailAddress;

			var resources = new Dictionary<string, string>();

			//TODO: httpContect ?
			//resources["logo"] = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/logoemail.png");

			resources["logo"] = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/" + clientUuid + "/logo.jpg");

			return Populate(x =>
				{
					x.Subject = siteFriendlyName + " Order";
					x.ViewName = viewName;
					x.LinkedResources = resources;
					x.To.Add(order.User.Email);
					x.From = new MailAddress(salesEmailAddress);
					x.Sender = new MailAddress(salesEmailAddress);
				});
		}


		public virtual MvcMailMessage ClientOrder(string clientUuid, string siteFriendlyName, Order order, string salesEmailAddress, string viewName = "ClientOrder")
		{
			ViewBag.Order = order;
			ViewBag.SiteFriendlyName = siteFriendlyName;
			//ViewBag.SalesEmailAddress = salesEmailAddress;

			var resources = new Dictionary<string, string>();

			//resources["logo"] = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/logoemail.png");

			resources["logo"] = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/" + clientUuid + "/logo.jpg");

			return Populate(x =>
			{
				x.Subject = siteFriendlyName + " Customer Order";
				x.ViewName = viewName;
				x.LinkedResources = resources;
				x.To.Add(salesEmailAddress);
				x.From = new MailAddress(salesEmailAddress);
				x.Sender = new MailAddress(salesEmailAddress);
			});
		}

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
	}
}