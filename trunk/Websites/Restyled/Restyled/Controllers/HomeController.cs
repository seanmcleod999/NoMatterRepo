﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoMatterWebApiWebHelper.OtherHelpers;
using NoMatterWebApiWebHelper.WebApiHelpers;
using RestyledLiving.Logging;

namespace RestyledLiving.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

	        try
	        {
				var globalSettimgs = new GlobalSettings();
				var clientHelper = new ClientHelper();

				var sections = clientHelper.GetClientSections(globalSettimgs.SiteClientId, true, true);

				return View(sections);
				return View();
	        }
	        catch (Exception ex)
	        {
		        Logger.WriteGeneralError(ex);
		        throw;
	        }

           
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}