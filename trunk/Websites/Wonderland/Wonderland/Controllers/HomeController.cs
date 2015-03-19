using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NoMatterWebApiModels.ViewModels;
using NoMatterWebApiWebHelper.OtherHelpers;
using NoMatterWebApiWebHelper.WebApiHelpers;
using RedOrange.Logging;

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
            return View();
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