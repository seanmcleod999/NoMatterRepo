using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NoMatterWebApiWebHelper.OtherHelpers;
using NoMatterWebApiModels.ViewModels;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

		public async Task<ActionResult> GetGlobalSettings(string clientId)
		{

			var clientSettingsVm = new GlobalSettingsVm
			{
				GlobalSettings = GlobalSettingsStaticCache.GetGlobalSettings()
			};

			return View(clientSettingsVm);
		}

		public async Task<ActionResult> ResetGlobalSettings(string clientId)
		{
			GlobalSettingsStaticCache.LoadGlobalSettingsCache();

			var clientSettingsVm = new GlobalSettingsVm
			{
				GlobalSettings = GlobalSettingsStaticCache.GetGlobalSettings()
			};

			return View("GetGlobalSettings", clientSettingsVm);
		}
    }
}