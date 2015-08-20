using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NoMatterDatabaseModel;
using NoMatterWebApi.Enums;

namespace NoMatterWebApi.DAL
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
		private DatabaseEntities databaseConnection;

		public OrderRepository(DatabaseEntities databaseConnection)
		{
			this.databaseConnection = databaseConnection;
		}

		public async Task<int> AddOrderAsync(Order order)
		{
			order.DateCreated = DateTime.Now;
			order.Paid = false;

			databaseConnection.Orders.Add(order);

			await databaseConnection.SaveChangesAsync();

			return order.OrderId;
		}

		public async Task<Order> GetOrderAsync(int orderId)
		{
			var order = await databaseConnection.Orders
				.Include(x=>x.PaymentType1)
				.Include("OrderProducts")
				.Include("OrderProducts.Product")
				.Include("User").Where(x => x.OrderId == orderId).SingleOrDefaultAsync();

			return order;
		}

		public async Task UpdateOrderPaymentTypeAsync(Order order, short paymentTypeId)
		{
			databaseConnection.Orders.Attach(order);

			order.PaymentTypeId = paymentTypeId;

			await databaseConnection.SaveChangesAsync();
		}

		public async Task UpdateOrderPaid(Order order, bool paid)
		{
			databaseConnection.Orders.Attach(order);

			order.Paid = paid;
			order.OrderStatusId = (byte)OrderStatusEnum.Complete;

			await databaseConnection.SaveChangesAsync();
		}

		public async Task UpdateOrderFailed(Order order)
		{
			databaseConnection.Orders.Attach(order);

			order.OrderStatusId = (byte)OrderStatusEnum.Failed;

			await databaseConnection.SaveChangesAsync();
		}

		public async Task UpdateOrderProductsAsSold(Order order)
		{
			foreach(var orderProduct in order.OrderProducts)
			{
				var product = orderProduct.Product;

				databaseConnection.Products.Attach(product);

				orderProduct.Product.DateSold = DateTime.Today;
				orderProduct.Product.Sold = true;

				await databaseConnection.SaveChangesAsync();
			}
		}

		public void Save()
		{
			databaseConnection.SaveChanges();
		}

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					databaseConnection.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	
	}
}