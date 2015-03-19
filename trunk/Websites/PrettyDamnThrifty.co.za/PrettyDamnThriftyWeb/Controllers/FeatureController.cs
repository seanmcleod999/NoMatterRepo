using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharedLibrary.Helpers;
using SharedLibrary.Logging;

namespace PrettyDamnThriftyWeb.Controllers
{
    public class FeatureController : Controller
    {
        //
        // GET: /Feature/

        public ActionResult Index(string id)
        {
	       
	        string temp = id;
            return View();
        }

		public ActionResult Features()
		{


			string temp = "123";
			return View();
		}

    }
}
