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
using NoMatterDatabaseModel;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Extensions;
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

		public SuperuserController()
		{
			var databaseEntity = new DatabaseEntities();

			_globalRepository = new GlobalRepository(databaseEntity);
			_clientRepository = new ClientRepository(databaseEntity);
		}

		public SuperuserController(IGlobalRepository productRepository, IClientRepository clientRepository)
		{
			_globalRepository = productRepository;
			_clientRepository = clientRepository;
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
				Clients = clients.Select(x=>x.ToDomainClient()).ToList()
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

			//var imageController = new ImageController();

			//if (addEditSectionVm.Picture != null)
			//{
			//	var imageBase64String = WebApiGeneralHelper.ConvertPicToBase64String(addEditSectionVm.Picture);
			//	var uploadImageResult = await imageController.UploadImageAsync(addEditSectionVm.Section.ClientId, imageBase64String);
			//	var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

			//	addEditSectionVm.Section.Picture = pictureResult.Content;
			//}

			await _clientRepository.AddClientAsync(addEditClientVm.Client.ToDatabaseClient());

			return RedirectToAction("Clients");
		}

		public async Task<ActionResult> ClientEdit(string clientId)
		{

			var client = await _clientRepository.GetClientAsync(new Guid(clientId));

			var editClientVm = new AddEditClientVm
			{
				Client = client.ToDomainClient()
			};

			return View(editClientVm);

		}

		[HttpPost]
		public async Task<ActionResult> ClientEdit(AddEditClientVm addEditClientVm)
		{
			//if (addEditClientVm.Client.Logo != null)
			//{
			//	var imageBase64String = WebApiGeneralHelper.ConvertPicToBase64String(addEditSectionVm.Picture);
			//	var uploadImageResult = await _imageController.UploadImageAsync(addEditSectionVm.Section.ClientId, imageBase64String);
			//	var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

			//	addEditSectionVm.Section.Picture = pictureResult.Content;
			//}

			var clientDb = await _clientRepository.GetClientAsync(new Guid(addEditClientVm.Client.ClientId));

			await _clientRepository.UpdateClientAsync(clientDb, addEditClientVm.Client);

			return RedirectToAction("Clients");

		}

		public async Task<ActionResult> SiteSettings()
		{
			try
			{
				var settings = await _globalRepository.GetSettingsAsync();

				var settingsVm = new SettingsVm
				{
					Settings = settings.Select(x=>x.ToDomainSetting()).ToList()
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
				Setting = setting.ToDomainSetting()
			};

			return View(settingVm);
		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> SiteSettingEdit(SettingVm settingVm)
		{
			var settingDb = await _globalRepository.GetSettingAsync(settingVm.Setting.SettingId);

			await _globalRepository.UpdateSettingAsync(settingDb, settingVm.Setting);

			return RedirectToAction("SiteSettings");
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
