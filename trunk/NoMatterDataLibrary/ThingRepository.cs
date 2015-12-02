using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterDatabaseModel;
using NoMatterDataLibrary.Extensions;
using Thing = NoMatterDataLibrary.Models.Thing;
using ThingField = NoMatterDataLibrary.Models.ThingField;
using ThingAlert = NoMatterDataLibrary.Models.ThingAlert;

namespace NoMatterDataLibrary
{


	public interface IThingRepository
	{
		Task<Thing> GetOrInsertThing(string thingName);
		Task<int> InsertThingField(int thingId, string thingFieldName);
		Task InsertThingFieldValue(int thingFieldId, int? intValue, bool? boolValue, string stringValue);
		Task<List<ThingAlert>> GetThingAlert(string thingName, string thingFieldName);
		Task UpdateThingAlertSent(int thingAlertId);
	}

	public class ThingRepository : IThingRepository
	{

		public async Task<Thing> GetOrInsertThing(string thingName)
		{
			using (var mainDb = new DatabaseEntities())
			{

				var thingDb = mainDb.Things.Include("ThingFields").FirstOrDefault(x => x.ThingName == thingName);

				if (thingDb == null)
				{
					thingDb = mainDb.Things.Create();

					thingDb.ThingName = thingName;
					thingDb.DateAdded = DateTime.Now;

					mainDb.Things.Add(thingDb);

					await mainDb.SaveChangesAsync();

				}

				return thingDb.ToDomainThing();
			}

		}

		public async Task<int> InsertThingField(int thingId, string thingFieldName)
		{
			using (var mainDb = new DatabaseEntities())
			{

				//var thingFieldDb = mainDb.ThingFields.FirstOrDefault(x => x.FieldName == thingFieldName);

				//if (thingFieldDb == null)
				//{

				var thingFieldDb = mainDb.ThingFields.Create();

				thingFieldDb.ThingId = thingId;
				thingFieldDb.FieldName = thingFieldName;
				thingFieldDb.DateAdded = DateTime.Now;

				mainDb.ThingFields.Add(thingFieldDb);

				await mainDb.SaveChangesAsync();

				//}


				return thingFieldDb.ThingFieldId;


			}
		}

		public async Task InsertThingFieldValue(int thingFieldId, int? intValue, bool? boolValue, string stringValue)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var thingFieldValue = mainDb.ThingFieldValues.Create();

				thingFieldValue.ThingFieldId = thingFieldId;
				thingFieldValue.DateAdded = DateTime.Now;
				thingFieldValue.IntValue = intValue;
				thingFieldValue.BoolValue = boolValue;
				thingFieldValue.StringValue = stringValue;

				mainDb.ThingFieldValues.Add(thingFieldValue);

				await mainDb.SaveChangesAsync();
			}
		}

		public async Task<List<ThingAlert>> GetThingAlert(string thingName, string fieldName)
		{
			using (var mainDb = new DatabaseEntities())
			{

				var thingAlertsDb = await mainDb.ThingAlerts.Where(x => x.ThingName == thingName && x.FieldName == fieldName).ToListAsync();

				return thingAlertsDb.Select(x => x.ToDomainThingAlert()).ToList();

			}
		}

		public async Task UpdateThingAlertSent(int thingAlertId)
		{
			using (var mainDb = new DatabaseEntities())
			{

				var thingAlertDb = await mainDb.ThingAlerts.Where(x => x.ThingAlertId == thingAlertId).SingleOrDefaultAsync();

				if (thingAlertDb != null)
				{
					thingAlertDb.LastAlertSent = DateTime.Now;

					await mainDb.SaveChangesAsync();
				}

			}
		}
	}
}
