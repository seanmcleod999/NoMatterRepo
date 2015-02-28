using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Extensions
{
	public static class ClientDeliveryOptionExtension
	{
		public static NoMatterWebApiModels.Models.ClientDeliveryOption ToDomainClientDeliveryOption(this NoMatterDatabaseModel.ClientDeliveryOption clientDeliveryOption)
		{
			return new NoMatterWebApiModels.Models.ClientDeliveryOption
			{
				ClientDeliveryOptionId = clientDeliveryOption.ClientDeliveryOptionId.ToString(),
				Description = clientDeliveryOption.Description,
				DeliveryAmount = clientDeliveryOption.DeliveryAmount,

			};
		}
	}
}