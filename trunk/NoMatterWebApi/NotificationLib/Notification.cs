using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Web.Mvc;
using Postal;

namespace NotificationLib
{
    public class Notification
    {

		public bool SendEftEmail(string to, string from, string viewName)
		{
			// Get the path to the directory containing views
			//var viewsPath = Path.GetFullPath(@"..\Views");

			var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath("~/Views/Emails"));

			var engines = new ViewEngineCollection();
			engines.Add(new FileSystemRazorViewEngine(viewsPath));

			var service = new EmailService(engines);

			dynamic email = new Email("Test");

			// Will look for Test.cshtml or Test.vbhtml in Views directory.
			
			email.To = "sean@dm.co.za";
			email.From = "mcleod.sean@gmail.com";
			email.Subject = "Yes the subject";
			email.Message = "Hello, non-asp.net world!";

			service.Send(email);


			return true;
		}
    }
}
