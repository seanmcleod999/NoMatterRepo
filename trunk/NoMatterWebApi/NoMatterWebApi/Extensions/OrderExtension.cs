using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterDatabaseModel;
using NoMatterWebApiModels.Models;
using Order = NoMatterWebApiModels.Models.Order;
using Product = NoMatterWebApiModels.Models.Product;

namespace NoMatterWebApi.Extensions
{
	public static class OrderExtension
	{
		public static Order ToDomainOrder(this   NoMatterDatabaseModel.Order order)
		{
			return new Order()
			{
				OrderId = order.OrderId,
				Message = order.Message,
				DeliveryDescription = order.ClientDeliveryOption.Description,
				DeliveryAmount = order.DeliveryAmount,
				ProductAmount = order.TotalAmount,
				TotalAmount = order.TotalAmount + order.ClientDeliveryOption.DeliveryAmount,
				OrderStatusId = order.OrderStatusId,
				Paid = order.Paid,
				Products = order.OrderProducts.Select(x => x.Product.ToDomainProduct()).ToList(),
				User = order.User.ToDomainUser()
			};
		}
	}
}