using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApi.ViewModels
{
	public class RunMySqlScriptVm
	{
		[Required(ErrorMessage = "required")]
		public string Script { get; set; }

		public bool ScriptExecuted { get; set; }

		public bool Success { get; set; }

		public string ErrorText { get; set; }

		public DataTable Data { get; set; }
	}
}
