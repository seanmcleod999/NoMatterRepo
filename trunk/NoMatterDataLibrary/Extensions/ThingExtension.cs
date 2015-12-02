using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterDataLibrary.Extensions
{

	public static class ThingExtension
	{
		public static NoMatterDataLibrary.Models.Thing ToDomainThing(this   NoMatterDatabaseModel.Thing thing)
		{
			return new NoMatterDataLibrary.Models.Thing
			{
				ThingId = thing.ThingId,
				ThingName = thing.ThingName,
				DateAdded = thing.DateAdded,
				ThingFields = thing.ThingFields.Select(x=>x.ToDomainThingField()).ToList()
			};
		}

		//public static NoMatterDatabaseModel.Thing ToDatabaseThing(this  NoMatterWebApiModels.Models.Thing thing)
		//{
		//	return new NoMatterDatabaseModel.Thing
		//	{
		//		ThingName = thing.ThingName,
		//		DateAdded = thing.DateAdded
		//	};
		//}
	}
}
