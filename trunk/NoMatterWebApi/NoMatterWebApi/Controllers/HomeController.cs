﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Security;
using NoMatterDataLibrary;
using NoMatterWebApi.Controllers.V1;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApi.ViewModels;
using NoMatterWebApiModels.ViewModels;
using Client = NoMatterWebApiModels.Models.Client;

namespace NoMatterWebApi.Controllers.v1
{
    public class HomeController : Controller
    {

		private IClientRepository _clientRepository;

		public HomeController()
		{

			_clientRepository = new ClientRepository();
		}

		public HomeController(IClientRepository clientRepository)
		{
			_clientRepository = clientRepository;
		}

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

		
		public async Task<ActionResult> Portfolio()
		{
			var clients = await _clientRepository.GetClientsAsync();

			var viewClientsVm = new ViewClientsVm
			{
				Clients = clients
			};

			return View(viewClientsVm);
		}
     }
}
