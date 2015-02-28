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
using NoMatterWebApiWebHelper.OtherHelpers;
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

		public ActionResult GetClientCacheSettings()
		{
			//ClientSettingsStaticCache.LoadClientSettingsCache(clientId);

			var clientSettingsVm = new ClientSettingsVm
				{
					//ClientId = clientId,
					ClientSettings = ClientSettingsStaticCache.GetClientSettings()
				};

			return View(clientSettingsVm);
		}

		public ActionResult GetClientSettings(string clientId)
		{
			//ClientSettingsStaticCache.LoadClientSettingsCache(clientId);

			var clientSettingsVm = new ClientSettingsVm
			{
				ClientId = clientId,
				ClientSettings = _clientHelper.GetClientSettings(clientId)
			};

			return View(clientSettingsVm);
		}

		public ActionResult ResetClientCacheSettings()
		{
			ClientSettingsStaticCache.LoadClientSettingsCache();

			var clientSettingsVm = new ClientSettingsVm
			{
				//ClientId = clientId,
				ClientSettings = ClientSettingsStaticCache.GetClientSettings()
			};

			return View("GetClientCacheSettings", clientSettingsVm);
		}

    }
}