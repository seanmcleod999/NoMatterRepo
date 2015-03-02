using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NoMatterDatabaseModel;

namespace NoMatterWebApi.DAL
{

	public interface IOrderRepository
	{
		Task<string> AddOrderAsync(Order order);
		
	}

	public class OrderRepository : IOrderRepository
	{
		private DatabaseEntities databaseConnection;

		public OrderRepository(DatabaseEntities databaseConnection)
		{
			this.databaseConnection = databaseConnection;
		}

		public async Task<string> AddOrderAsync(Order order)
		{
			order.OrderUUID = Guid.NewGuid();
			order.DateCreated = DateTime.Now;
			order.Paid = false;

			databaseConnection.Orders.Add(order);

			await databaseConnection.SaveChangesAsync();

			return order.OrderUUID.ToString();
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