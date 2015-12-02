using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterDataLibrary.Models
{
	public class ThingAlert
	{
		public int ThingAlertId { get; set; }

		public string ThingName { get; set; }

		public string FieldName { get; set; }

		public short ThingAlertTypeId { get; set; }

		public short ThingAlertFrequencyId { get; set; }

		public int Criteria { get; set; }

		public string AlertSubject { get; set; }

		public string AlertBody { get; set; }

		public DateTime? LastAlertSent { get; set; }
	}
}
