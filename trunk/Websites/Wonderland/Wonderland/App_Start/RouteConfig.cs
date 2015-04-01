using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RedOrange
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Shop",
				"Shop",
				new { Controller = "Shop", action = "Index" });

			//routes.MapRoute(
			//	"ShopCategoryPartial",
			//	"Shop/ShopCategoryPartial/{id}",
			//	new { Controller = "Shop", action = "ShopCategoryPartial", id = UrlParameter.Optional });

			routes.MapRoute(
				"ShopCategory",
				"Shop/{id}",
				new { Controller = "Shop", action = "Category", id = UrlParameter.Optional });

			

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
