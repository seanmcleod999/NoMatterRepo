using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PrettyDamnThriftyWeb
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Products",
				"Products",
				new { Controller = "Product", action = "Index" });

			routes.MapRoute(
				"Categories",
				"Categories",
				new { Controller = "Category", action = "Index" });

			routes.MapRoute(
				"Shop1",
				"Shop",
				new { Controller = "Shop", action = "Latest" });

			routes.MapRoute(
				"ShopSale",
				"Shop/Sale",
				new { Controller = "Shop", action = "Sale" });

			routes.MapRoute(
				"ShopLatest",
				"Shop/Latest",
				new { Controller = "Shop", action = "Latest" });

			routes.MapRoute(
				"Shop",
				"Shop/{id}",
				new { Controller = "Shop", action = "Category", id = UrlParameter.Optional });

			routes.MapRoute(
				"Vintage1",
				"VintageShop",
				new { Controller = "VintageShop", action = "Latest" });

			routes.MapRoute(
				"VintageShopSale",
				"VintageShop/Sale",
				new { Controller = "VintageShop", action = "Sale" });

			routes.MapRoute(
				"VintageShopLatest",
				"VintageShop/Latest",
				new { Controller = "VintageShop", action = "Latest" });

			routes.MapRoute(
				"VintageShop",
				"VintageShop/{id}",
				new { Controller = "VintageShop", action = "Category", id = UrlParameter.Optional });

			routes.MapRoute(
				"Feature",
				"Feature/{id}",
				new { Controller = "Feature", action = "Index", id = UrlParameter.Optional });

			routes.MapRoute(
				"Features",
				"Features",
				new { Controller = "Feature", action = "Features", id = UrlParameter.Optional });

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Temp", action = "Index", id = UrlParameter.Optional }
			);
			
		}
	}
}