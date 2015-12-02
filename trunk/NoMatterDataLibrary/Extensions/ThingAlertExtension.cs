using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterDataLibrary.Extensions
{
	public static class ThingAlertExtension
	{
		public static NoMatterDataLibrary.Models.ThingAlert ToDomainThingAlert(this   NoMatterDatabaseModel.ThingAlert thingAlert)
		{
			return new NoMatterDataLibrary.Models.ThingAlert
			{
				ThingAlertId = thingAlert.ThingAlertId,
				ThingName = thingAlert.ThingName,
				FieldName = thingAlert.FieldName,
				ThingAlertTypeId = thingAlert.ThingAlertTypeId,
				ThingAlertFrequencyId = thingAlert.ThingAlertFrequencyId,
				Criteria = thingAlert.Criteria,
				AlertSubject = thingAlert.AlertSubject,
				AlertBody = thingAlert.AlertBody,
				LastAlertSent = thingAlert.LastAlertSent
			};
		}

	}
}
