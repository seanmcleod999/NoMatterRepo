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
				ClientDeliveryOptionId = clientDeliveryOption.ClientDeliveryOptionId,
				Description = clientDeliveryOption.Description,
				DeliveryAmount = clientDeliveryOption.DeliveryAmount,
				OptionOrder = clientDeliveryOption.OptionOrder,
				Enabled = clientDeliveryOption.Enabled

			};
		}

		public static NoMatterDatabaseModel.ClientDeliveryOption ToDatabaseClientDeliveryOption(this  NoMatterWebApiModels.Models.ClientDeliveryOption clientDeliveryOption, int clientId)
		{
			return new NoMatterDatabaseModel.ClientDeliveryOption
			{
				ClientId = clientId,
				Description = clientDeliveryOption.Description,
				DeliveryAmount = clientDeliveryOption.DeliveryAmount,
				OptionOrder = clientDeliveryOption.OptionOrder,
				Enabled = clientDeliveryOption.Enabled
			};
		}
	}
}