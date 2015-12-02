using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterDataLibrary.Models
{
	public class ThingField
	{

		public int ThingFieldId { get; set; }

		public Thing Thing { get; set; }

		public string FieldName { get; set; }

		public DateTime DateAdded { get; set; }

	}
}
