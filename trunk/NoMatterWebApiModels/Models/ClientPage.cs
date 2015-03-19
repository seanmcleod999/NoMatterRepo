using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class ClientPage
	{
		[Required(ErrorMessage = "required")]
		[StringLength(50, ErrorMessage = "Must be under 50 characters")]
		public string PageName { get; set; }

		public string PageText { get; set; }
	}
}
