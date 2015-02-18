using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;

namespace NoMatterWebApi.Controllers.v1
{
	public class ClientAdminController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Title = "Home Page";

			return View();
		}

	}
}
