using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class Supplier
	{
		public int SupplierId { get; set; }

		public string Name { get; set; }

		public string ContactNumber { get; set; }

		public string Email { get; set; }
	}
}
