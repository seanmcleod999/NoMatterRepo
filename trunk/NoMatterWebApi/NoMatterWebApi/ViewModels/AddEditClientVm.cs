using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApi.ViewModels
{
	public class AddEditClientVm
	{
		[Required(ErrorMessage = "required")]
		[Display(Name = "Logo")]
		public HttpPostedFileBase Logo { get; set; }

		public Client Client { get; set; }
	}
}
