using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NoMatterDatabaseModel;

namespace NoMatterWebApi.DAL
{
	public interface ICartRepository
	{
		Task<CartProduct> GetCartProductAsync(string cartId, Guid productId);
		Task<List<CartProduct>> GetCartProductsAsync(string cartId);
		Task AddCartProductAsync(CartProduct cartProduct);
		Task DeleteCartProductAsync(CartProduct cartProduct);		
	}

	public class CartRepository : ICartRepository
	{
		private DatabaseEntities databaseConnection;

		public CartRepository(DatabaseEntities databaseConnection)
		{
			this.databaseConnection = databaseConnection;
		}

		public async Task<CartProduct> GetCartProductAsync(string cartId, Guid productId)
		{
			var cartProduct =
				await
				databaseConnection.CartProducts.Where(x => x.CartId == cartId && x.Product.ProductUUID == productId).FirstOrDefaultAsync();

			return cartProduct;
		}

		public async Task<List<CartProduct>> GetCartProductsAsync(string cartId)
		{
			var cartProducts = await databaseConnection.CartProducts.Include("Product").Where(x => x.CartId == cartId).ToListAsync();

			return cartProducts;
		}

		public async Task AddCartProductAsync(CartProduct cartProduct)
		{
			cartProduct.DateAdded = DateTime.Now;

			databaseConnection.CartProducts.Add(cartProduct);

			await databaseConnection.SaveChangesAsync();

		}

		public async Task DeleteCartProductAsync(CartProduct cartProduct)
		{
			databaseConnection.CartProducts.Remove(cartProduct);

			await databaseConnection.SaveChangesAsync();
		}
	}
}