using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NoMatterWebApiWebHelper;
using WebApplication7.Logging;
using WebApplication7.Models;
using WebApplication7.Helpers;
using WebApplication7.ViewModels;

namespace WebApplication7.Controllers
{
    public class ClientController : Controller
    {
		private IClientHelper _clientHelper;
		
		public ClientController()
		{
			_clientHelper = new ClientHelper();
		}

		public ClientController(IClientHelper clientHelper)
		{
			_clientHelper = clientHelper;
		}

		// GET: User
		public async Task<ActionResult> GetClients()
		{
			var clients = await _clientHelper.GetClientsAsync();

			return View(clients);
		}


		public async Task<ActionResult> GetClientSections(string clientId)
		{
			var clientSections = await _clientHelper.GetClientSectionsAsync(clientId);

			return View(clientSections);
		}

    }
}