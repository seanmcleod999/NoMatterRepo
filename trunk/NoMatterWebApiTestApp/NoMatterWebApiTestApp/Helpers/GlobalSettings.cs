using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace WebApplication7.Helpers
{
    public static class GlobalSettings
    {

        public static string BaseAddress { get { return ConfigurationManager.AppSettings["BaseAddress"]; } }
		public static string AdminEndpointBaseAddress { get { return ConfigurationManager.AppSettings["AdminEndpointBaseAddress"]; } }
		public static string FacebookAppId { get { return ConfigurationManager.AppSettings["FacebookAppId"]; } }
		public static string FacebookAppSecret { get { return ConfigurationManager.AppSettings["FacebookAppSecret"]; } }

		public static string DefaultClientId { get { return ConfigurationManager.AppSettings["DefaultClientId"]; } }


    }
}