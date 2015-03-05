using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using NoMatterWebApi.CustomAuth;
using NoMatterWebApi.Logging;


namespace NoMatterWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			config.EnableCors();

            config.SuppressHostPrincipal();

			
			config.Filters.Add(new CustomAuthenticationFilter());
	      
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

			config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

			config.Formatters.Clear();
			
			//config.Formatters.Add(new BrowserJsonFormatter());
			config.Formatters.Add(new JsonMediaTypeFormatter());
			config.Formatters.Add(new XmlMediaTypeFormatter());  
        }

	    
    }
}
