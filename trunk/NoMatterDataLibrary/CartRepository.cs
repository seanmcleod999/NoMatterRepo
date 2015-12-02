using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using NoMatterDataLibrary.Extensions;
using NoMatterDatabaseModel;
using CartProduct = NoMatterWebApiModels.Models.CartProduct;

namespace NoMatterDataLibrary
{
	public interface ICartRepository
	{
		Task<CartProduct> GetCartProductAsync(string cartId, int productId);
		Task<List<CartProduct>> GetCartProductsAsync(string cartId);
		Task AddCartProductAsync(CartProduct cartProduct);
		Task DeleteCartProductAsync(int cartProductId);
		Task DeleteCartProductsAsync(string cartId);
		Task<int> GetCartProductCountAsync(string cartId);
	}

	public class CartRepository : ICartRepository
	{

		public async Task<CartProduct> GetCartProductAsync(string cartId, int productId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var cartProduct = await mainDb.CartProducts.Where(x => x.CartId == cartId && x.Product.ProductId == productId).FirstOrDefaultAsync();

				return cartProduct.ToDomainCartProduct();
			}
			
		}

		public async Task<List<CartProduct>> GetCartProductsAsync(string cartId)
		{

			using (var mainDb = new DatabaseEntities())
			{
				var cartProducts = await mainDb.CartProducts
					.Include("Product")
					.Where(x => x.CartId == cartId).OrderBy(x => x.Product.Title).ToListAsync();

				return cartProducts.Select(x => x.ToDomainCartProduct()).ToList();
			}

		}

		public async Task AddCartProductAsync(CartProduct cartProduct)
		{

			using (var mainDb = new DatabaseEntities())
			{
				var cartProductDb = cartProduct.ToDatabaseCartProduct();

				cartProductDb.DateAdded = DateTime.Now;

				mainDb.CartProducts.Add(cartProductDb);

				await mainDb.SaveChangesAsync();
			}
			

		}

		public async Task DeleteCartProductAsync(int cartProductId)
		{

			using (var mainDb = new DatabaseEntities())
			{
				var cartProductDb = await mainDb.CartProducts.Where(x => x.CartProductId == cartProductId).FirstOrDefaultAsync();

				mainDb.CartProducts.Remove(cartProductDb);

				await mainDb.SaveChangesAsync();
			}
			
		}

		public async Task DeleteCartProductsAsync(string cartId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var cartProducts = await mainDb.CartProducts.Where(x => x.CartId == cartId).ToListAsync();

				foreach (var cartProduct in cartProducts)
				{
					mainDb.CartProducts.Remove(cartProduct);
				}

				await mainDb.SaveChangesAsync();
			}
			
		}

		public async Task<int> GetCartProductCountAsync(string cartId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				return await mainDb.CartProducts.Where(x => x.CartId == cartId).CountAsync();
			}
			
		}
	}
}