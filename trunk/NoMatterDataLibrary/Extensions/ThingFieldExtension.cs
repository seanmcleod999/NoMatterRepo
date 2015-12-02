using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterDataLibrary.Extensions
{
	public static class ThingFieldExtension
	{

		public static NoMatterDataLibrary.Models.ThingField ToDomainThingField(this   NoMatterDatabaseModel.ThingField thingField)
		{
			return new NoMatterDataLibrary.Models.ThingField
			{
				ThingFieldId = thingField.ThingFieldId,
				//Thing = thingField.Thing.ToDomainThing(),
				FieldName = thingField.FieldName,
				DateAdded = thingField.DateAdded


			};
		}

		public static NoMatterDatabaseModel.ThingField ToDatabaseThingField(this  NoMatterDataLibrary.Models.ThingField thingField)
		{
			return new NoMatterDatabaseModel.ThingField
			{
				FieldName = thingField.FieldName,
				DateAdded = thingField.DateAdded
			};
		}
	}


}
