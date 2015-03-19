using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using SharedLibrary.Helpers;
using SharedLibrary.ViewModels;

namespace PrettyDamnThriftyWeb.Controllers
{
    public class SuperuserController : Controller
    {
		private static readonly string s_ConnectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

	    public ActionResult Index()
	    {
			return View("Index", "_SuperUserLayout");
	    }

	    public ActionResult ViewMySqlResults()
        {
			var runMySqlScriptVm = new RunMySqlScriptVm();

			runMySqlScriptVm.Script = "select * from information_schema.tables";

			return View("ViewMySqlResults", "_SuperUserLayout", runMySqlScriptVm);
        }

	    [HttpPost]
	    public ActionResult ViewMySqlResults(RunMySqlScriptVm runMySqlScriptVm)
	    {
		    try
		    {
				string query = runMySqlScriptVm.Script;

				var myConn = new MySqlConnection(s_ConnectionString);

				//var myCmd = new MySqlCommand(query, myConn);
				var returnVal = new MySqlDataAdapter(query, myConn);

				runMySqlScriptVm.ScriptExecuted = true;

				myConn.Open();

				var dt = new DataTable("CharacterInfo");
				returnVal.Fill(dt);

				myConn.Close();

			    runMySqlScriptVm.Data = dt;

				runMySqlScriptVm.Success = true;

				return View("ViewMySqlResults", "_SuperUserLayout", runMySqlScriptVm);


		    }
		    catch (Exception ex)
		    {
				runMySqlScriptVm.Success = false;
				runMySqlScriptVm.ErrorText = ex.Message;

				return View("RunMySqlScript", "_SuperUserLayout", runMySqlScriptVm);
		    }

	    }


	    public ActionResult RunMySqlScript()
	    {
		    var runMySqlScriptVm = new RunMySqlScriptVm();

			return View("RunMySqlScript", "_SuperUserLayout", runMySqlScriptVm);

	    }

		[HttpPost]
		public ActionResult RunMySqlScript(RunMySqlScriptVm runMySqlScriptVm)
		{
			try
			{
			
				string query = runMySqlScriptVm.Script;

				var myConn = new MySqlConnection(s_ConnectionString);

				var myCmd = new MySqlCommand(query, myConn);
				//MySqlDataReader myReader;

				myConn.Open();

				runMySqlScriptVm.ScriptExecuted = true;

				myCmd.ExecuteNonQuery();     // Here our query will be executed and data saved into the database.

				//while (myReader.Read())
				//{
				//}
				myConn.Close();

				
				runMySqlScriptVm.Success = true;

				return View("RunMySqlScript", "_SuperUserLayout", runMySqlScriptVm);

			}
			catch (Exception ex)
			{
				runMySqlScriptVm.Success = false;
				runMySqlScriptVm.ErrorText = ex.Message;

				return View("RunMySqlScript", "_SuperUserLayout", runMySqlScriptVm);
			}

		}

	    public ActionResult ReloadSettingsCache()
	    {
			DbSettingsStaticCache.LoadDbGlobalSettingsCache();

		    var settings = DbSettingsStaticCache.GetGlobalDbSettings();

			return View("ClearSettingsCache", "_SuperUserLayout", settings);
	    }

    }
}
