using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NoMatterWebApiModels.ViewModels;
using NoMatterWebApiWebHelper.Logging;
using NoMatterWebApiWebHelper.OtherHelpers;
using NoMatterWebApiWebHelper.WebApiHelpers;

namespace RedOrange.Controllers
{
    public class HomeController : Controller
    {
		private IClientHelper _clientHelper;
		private IGlobalSettings _globalSettings;

		public HomeController()
		{
			_clientHelper = new ClientHelper();
			_globalSettings = new GlobalSettings();
		}

        public ActionResult Index()
        {

	        return RedirectToAction("Index", "Shop");
	        //return View();
        }

        public async Task<ActionResult> About()
        {
	        try
	        {
				var page = await _clientHelper.GetClientPage(_globalSettings.SiteClientId, "AboutPage");

		        var viewClientPageVm = new ViewClientPageVm
			        {
				        PageText = page != null ? page.PageText : ""
			        };

				//ViewBag.ClientLongitude = _globalSettings.ClientLongitude;
				//ViewBag.ClientLatitude = _globalSettings.ClientLatitude;

				return View(viewClientPageVm);
	        }
	        catch (Exception ex)
	        {
		        Logger.WriteGeneralError(ex);
		        throw;
	        }

           
        }

		public async Task<ActionResult> ContactUs()
        {
			try
			{
				var page = await _clientHelper.GetClientPage(_globalSettings.SiteClientId, "ContactPage");

				var contactsUsVm = new ContactUsVm();

				contactsUsVm.PageText = page.PageText;

				//if (page != null)
				//{
				//	//String html = "<p>[SLIDER]Something[\\SLIDER]</p>";

				//	String html = page.PageText;

				//	//String replacementHtml = "<iframe src=\"//www.youtube.com/embed/$1\" frameborder=\"0\" allowfullscreen></iframe>";
				//	String replacementHtml = "@{Html.RenderPartial(\"partialTest\");}";

				//	Regex shortcodeRegex = new Regex(@"\[SLIDER\]([^\[\\]+)\[/SLIDER\]");

				//	String result = shortcodeRegex.Replace(html, replacementHtml);


				//	contactsUsVm.PageText = result;
				//}


				return View(contactsUsVm);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
        }

		public async Task<ActionResult> Shipping()
		{
			try
			{
				var page = await _clientHelper.GetClientPage(_globalSettings.SiteClientId, "ShippingPage");

				var viewClientPageVm = new ViewClientPageVm
				{
					PageText = page != null ? page.PageText : ""
				};


				return View(viewClientPageVm);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}
    }
}