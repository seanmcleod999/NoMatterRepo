using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterDataLibrary.Models
{
	public class Thing
	{
		public int ThingId { get; set; }

		public string ThingName { get; set; }

		public DateTime DateAdded { get; set; }

		public List<ThingField> ThingFields { get; set; }
	}
}
