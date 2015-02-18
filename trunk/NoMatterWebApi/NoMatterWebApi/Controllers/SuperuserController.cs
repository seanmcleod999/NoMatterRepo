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
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApi.ViewModels;

namespace NoMatterWebApi.Controllers.v1
{
    public class SuperuserController : Controller
    {
		private static readonly string s_ConnectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

        public ActionResult Index()
        {

            return View();
        }

		public ActionResult ViewMySqlResults()
		{
			var runMySqlScriptVm = new RunMySqlScriptVm();

			runMySqlScriptVm.Script = "select * from information_schema.tables";

			return View("ViewMySqlResults", runMySqlScriptVm);
		}

		[HttpPost]
		public ActionResult ViewMySqlResults(RunMySqlScriptVm runMySqlScriptVm)
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

				return View("ViewMySqlResults", runMySqlScriptVm);


			}
			catch (Exception ex)
			{
				runMySqlScriptVm.Success = false;
				runMySqlScriptVm.ErrorText = ex.Message;

				return View("RunMySqlScript", runMySqlScriptVm);
			}

		}


		public ActionResult RunMySqlScript()
		{
			var runMySqlScriptVm = new RunMySqlScriptVm();

			return View("RunMySqlScript", runMySqlScriptVm);

		}

		[HttpPost]
		public ActionResult RunMySqlScript(RunMySqlScriptVm runMySqlScriptVm)
		{
			try
			{

				string query = runMySqlScriptVm.Script;

				var myConn = new SqlConnection(s_ConnectionString);

				var myCmd = new SqlCommand(query, myConn);
				//MySqlDataReader myReader;

				myConn.Open();

				runMySqlScriptVm.ScriptExecuted = true;

				myCmd.ExecuteNonQuery();     // Here our query will be executed and data saved into the database.

				//while (myReader.Read())
				//{
				//}
				myConn.Close();


				runMySqlScriptVm.Success = true;

				return View("RunMySqlScript", runMySqlScriptVm);

			}
			catch (Exception ex)
			{
				runMySqlScriptVm.Success = false;
				runMySqlScriptVm.ErrorText = ex.Message;

				return View("RunMySqlScript", runMySqlScriptVm);
			}

		}

		
    }
}
