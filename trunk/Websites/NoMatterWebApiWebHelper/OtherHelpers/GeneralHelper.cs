
using System.Collections.Generic;

using System.Web.Mvc;

namespace NoMatterWebApiWebHelper.OtherHelpers
{
	public class GeneralHelper
	{
		public static List<SelectListItem> GetProvinces()
		{
			var suburbs = new List<SelectListItem>()
			{
				new SelectListItem() { Text="Eastern Cape", Value="Eastern Cape"},
				new SelectListItem() { Text="Free State", Value="Free State"},
				new SelectListItem() { Text="Gauteng", Value="Gauteng"},
				new SelectListItem() { Text="KwaZulu-Natal", Value="KwaZulu-Natal"},
				new SelectListItem() { Text="Limpopo", Value="Limpopo"},
				new SelectListItem() { Text="Mpumalanga", Value="Mpumalanga"},
				new SelectListItem() { Text="Northern Cape", Value="Northern Cape"},
				new SelectListItem() { Text="North-West", Value="North-West"},
				new SelectListItem() { Text="Western Cape", Value="Western Cape"},
        
			};

			return suburbs;

		}
	}
}
