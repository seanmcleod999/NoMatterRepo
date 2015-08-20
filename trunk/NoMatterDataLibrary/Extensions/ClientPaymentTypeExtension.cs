using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterDataLibrary.Extensions
{
	public static class ClientPaymentTypeExtension
	{
		public static NoMatterWebApiModels.Models.ClientPaymentType ToDomainClientPaymentType(this NoMatterDatabaseModel.ClientPaymentType clientPaymentType)
		{
			return new NoMatterWebApiModels.Models.ClientPaymentType
			{
				ClientPaymentTypeId = clientPaymentType.ClientPaymentTypeId.ToString(),
				PaymentTypeId = clientPaymentType.PaymentType.PaymentTypeId,
				PaymentTypeName = clientPaymentType.PaymentType.Name,
				PaymentTypeDetails = clientPaymentType.PaymentType.Details,
				PaymentTypePicture = clientPaymentType.PaymentType.Picture

			};
		}
	}
}