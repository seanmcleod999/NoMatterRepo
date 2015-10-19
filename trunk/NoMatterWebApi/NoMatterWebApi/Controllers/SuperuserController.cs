using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NoMatterDataLibrary;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApi.ViewModels;

namespace NoMatterWebApi.Controllers.v1
{
    public class SuperuserController : Controller
    {
		private static readonly string s_ConnectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

	   private IGlobalRepository _globalRepository;
	   private IClientRepository _clientRepository;
	   private IGeneralHelper _generalHelper;

		public SuperuserController()
		{
			_globalRepository = new GlobalRepository();
			_clientRepository = new ClientRepository();
			_generalHelper = new GeneralHelper();
		}

		public SuperuserController(IGlobalRepository productRepository, IClientRepository clientRepository, IGeneralHelper generalHelper)
		{
			_globalRepository = productRepository;
			_clientRepository = clientRepository;
			_generalHelper = generalHelper;
		}

        public ActionResult Index()
        {
            return View();
        }

		

		public async Task<ActionResult> Clients()
		{
			var clients = await _clientRepository.GetClientsAsync();

			var viewClientsVm = new ViewClientsVm
			{
				Clients = clients
			};

			return View(viewClientsVm);
		}

		public ActionResult ClientAdd()
		{
			var addClientVm = new AddEditClientVm
			{
				Client = new NoMatterWebApiModels.Models.Client()
				{
					Enabled = true
				}
			};

			return View(addClientVm);
		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> ClientAdd(AddEditClientVm addEditClientVm)
		{
			try
			{

				if (addEditClientVm.Logo != null)
					addEditClientVm.Client.Logo = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addEditClientVm.Logo), addEditClientVm.Client.ClientUuid);			

				string clientUuid = await _clientRepository.AddClientAsync(addEditClientVm.Client);

				await ClientHelper.ClientSettingsAddMissing(_clientRepository, new Guid(clientUuid));

				var password = PasswordCrypto.HashPassword("@dmin123");

				await _clientRepository.AddClientDefaultAdminUserAsync(new Guid(clientUuid), addEditClientVm.Client.DomainName, password); 

				return RedirectToAction("ClientSettings", "Admin", new { ClientUuid = clientUuid, fromCreateClient = true });

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		public async Task<ActionResult> ClientEdit(string clientUuid)
		{

			var client = await _clientRepository.GetClientAsync(new Guid(clientUuid));

			var editClientVm = new AddEditClientVm
			{
				Client = client
			};

			return View(editClientVm);

		}

		[HttpPost]
		public async Task<ActionResult> ClientEdit(AddEditClientVm addEditClientVm)
		{
			try
			{
				if (addEditClientVm.Logo != null)
					addEditClientVm.Client.Logo = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addEditClientVm.Logo), addEditClientVm.Client.ClientUuid);

				await _clientRepository.UpdateClientAsync(addEditClientVm.Client);

				return RedirectToAction("Clients");
			}
			
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
			
		}

		public async Task<ActionResult> SiteSettings()
		{
			try
			{
				var settings = await _globalRepository.GetSettingsAsync();

				var settingsVm = new SettingsVm
				{
					Settings = settings
				};

				return View(settingsVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		public async Task<ActionResult> SiteSettingEdit(short settingId)
		{
			
			var setting = await _globalRepository.GetSettingAsync(settingId);

			var settingVm = new SettingVm
			{
				Setting = setting
			};

			return View(settingVm);
		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> SiteSettingEdit(SettingVm settingVm)
		{
			try
			{
				//var settingDb = await _globalRepository.GetSettingAsync(settingVm.Setting.SettingId);

				await _globalRepository.UpdateSettingAsync(settingVm.Setting);

				return RedirectToAction("SiteSettings");
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
			
		}


		public ActionResult ViewSqlResults()
		{
			var runMySqlScriptVm = new RunMySqlScriptVm();

			runMySqlScriptVm.Script = "select * from information_schema.tables";

			return View("ViewSqlResults", runMySqlScriptVm);
		}

		[HttpPost]
		public ActionResult ViewSqlResults(RunMySqlScriptVm runMySqlScriptVm)
		{
			try
			{
				string query = runMySqlScriptVm.Script;

				var myConn = new SqlConnection(s_ConnectionString);

				//var myCmd = new MySqlCommand(query, myConn);
				var returnVal = new SqlDataAdapter(query, myConn);

				runMySqlScriptVm.ScriptExecuted = true;

				myConn.Open();

				var dt = new DataTable("CharacterInfo");
				returnVal.Fill(dt);

				myConn.Close();

				runMySqlScriptVm.Data = dt;

				runMySqlScriptVm.Success = true;

				return View("ViewSqlResults", runMySqlScriptVm);


			}
			catch (Exception ex)
			{
				runMySqlScriptVm.Success = false;
				runMySqlScriptVm.ErrorText = ex.Message;

				return View("RunSqlScript", runMySqlScriptVm);
			}

		}

		public ActionResult RunSqlScript()
		{
			var runMySqlScriptVm = new RunMySqlScriptVm();

			return View("RunSqlScript", runMySqlScriptVm);

		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult RunSqlScript(RunMySqlScriptVm runMySqlScriptVm)
		{
			try
			{

				string query = runMySqlScriptVm.Script;

				var myConn = new SqlConnection(s_ConnectionString);

				SuperuserHelper.ExecuteBatchNonQuery(query, myConn);

				myConn.Close();

				ModelState.Clear();
				runMySqlScriptVm.Success = true;

				return View("RunSqlScriptResult", runMySqlScriptVm);

			}
			catch (Exception ex)
			{
				ModelState.Clear();
				runMySqlScriptVm.Success = false;
				runMySqlScriptVm.ErrorText = ex.Message;

				return View("RunSqlScriptResult", runMySqlScriptVm);
			}

		}	
		
    }
}
