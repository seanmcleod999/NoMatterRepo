using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class WebApiResult
	{
		public object ResultObject { get; set; }

		public int ResultCode { get; set; }

		public string ResultDescription { get; set; }
	}
}
