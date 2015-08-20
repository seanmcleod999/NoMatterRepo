using NoMatterWebApiModels.Models;

namespace NoMatterDataLibrary.Extensions
{
	public static class ClientDeliveryOptionExtension
	{
		public static ClientDeliveryOption ToDomainClientDeliveryOption(this NoMatterDatabaseModel.ClientDeliveryOption clientDeliveryOption)
		{
			return new ClientDeliveryOption
			{
				ClientDeliveryOptionId = clientDeliveryOption.ClientDeliveryOptionId,
				Client = clientDeliveryOption.Client.ToDomainClient(),
				Description = clientDeliveryOption.Description,
				DeliveryAmount = clientDeliveryOption.DeliveryAmount,
				OptionOrder = clientDeliveryOption.OptionOrder,
				Enabled = clientDeliveryOption.Enabled

			};
		}

		public static NoMatterDatabaseModel.ClientDeliveryOption ToDatabaseClientDeliveryOption(this  ClientDeliveryOption clientDeliveryOption, int clientId)
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