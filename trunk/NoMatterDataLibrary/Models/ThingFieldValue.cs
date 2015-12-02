using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterDataLibrary.Models
{
	public class ThingFieldValue
	{
		public int ThingFieldId { get; set; }

		public ThingField ThingField { get; set; }

		public int IntValue { get; set; }

		public bool BoolValue { get; set; }

		public string StringValue { get; set; }
	}
}
