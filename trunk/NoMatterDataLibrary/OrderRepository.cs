using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NoMatterDataLibrary.Enums;
using NoMatterDataLibrary.Extensions;
using NoMatterDatabaseModel;
using Order = NoMatterWebApiModels.Models.Order;

namespace NoMatterDataLibrary
{

	public interface IOrderRepository
	{
		Task<int> AddOrderAsync(Order order);
		Task<Order> GetOrderAsync(int orderId);
		Task UpdateOrderPaymentTypeAsync(Order order, short paymentTypeId);
		Task UpdateOrderPaid(Order order, bool paid);
		Task UpdateOrderFailed(Order order);
		Task UpdateOrderProductsAsSold(Order order);
	}

	public class OrderRepository : IOrderRepository
	{


		public async Task<int> AddOrderAsync(Order order)
		{
			//var orderDb = order.ToOrderDatabase();

			using (var mainDb = new DatabaseEntities())
			{

				//orderDb.DateCreated = DateTime.Now;
				//orderDb.Paid = false;

				//mainDb.Orders.Add(orderDb);

				//await mainDb.SaveChangesAsync();

				//return orderDb.OrderId;

				return 0;
			}
		}

		public async Task<Order> GetOrderAsync(int orderId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var orderDb = await mainDb.Orders
				                    .Include(x => x.PaymentType1)
				                    .Include("OrderProducts")
				                    .Include("OrderProducts.Product")
				                    .Include("User").Where(x => x.OrderId == orderId).SingleOrDefaultAsync();

				return orderDb.ToDomainOrder();

			}
		}

		public async Task UpdateOrderPaymentTypeAsync(Order order, short paymentTypeId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var orderDb = await mainDb.Orders.Where(x => x.OrderId == order.OrderId).FirstOrDefaultAsync();

				orderDb.PaymentTypeId = paymentTypeId;

				await mainDb.SaveChangesAsync();
			}
			
		}

		public async Task UpdateOrderPaid(Order order, bool paid)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var orderDb = await mainDb.Orders.Where(x => x.OrderId == order.OrderId).FirstOrDefaultAsync();

				orderDb.Paid = paid;
				orderDb.OrderStatusId = (byte)OrderStatusEnum.Complete;

				await mainDb.SaveChangesAsync();
			}
		}

		public async Task UpdateOrderFailed(Order order)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var orderDb = await mainDb.Orders.Where(x => x.OrderId == order.OrderId).FirstOrDefaultAsync();

				orderDb.OrderStatusId = (byte)OrderStatusEnum.Failed;

				await mainDb.SaveChangesAsync();
			}

		}

		public async Task UpdateOrderProductsAsSold(Order order)
		{
			//foreach(var orderProduct in order.OrderProducts)
			//{
			//	var product = orderProduct.Product;

			//	databaseConnection.Products.Attach(product);

			//	orderProduct.Product.DateSold = DateTime.Today;
			//	orderProduct.Product.Sold = true;

			//	await databaseConnection.SaveChangesAsync();
			//}
		}

	
	
	}
}