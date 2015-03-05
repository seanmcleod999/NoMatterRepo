using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NoMatterDatabaseModel;

namespace NoMatterWebApi.DAL
{

	public interface IOrderRepository
	{
		Task<int> AddOrderAsync(Order order);
		Task<Order> GetOrderAsync(int orderId);
		
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
				.Include("OrderProducts")
				.Include("OrderProducts.Product")
				.Include("User").Where(x => x.OrderId == orderId).SingleOrDefaultAsync();

			return order;
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