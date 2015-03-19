using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace PrettyDamnThriftyWeb.Controllers
{
    public class TempController : Controller
    {
		public ActionResult Index()
		{
			//var homeVm = new HomeVm();
			//{
			//	Slider = _sliderService.GetSlider("Home")
			//};

			return View("Index", "_LayoutTemp");
		}
    }
}
