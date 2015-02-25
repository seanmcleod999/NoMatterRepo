using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterDatabaseModel;

namespace NoMatterWebApi.DAL
{
	public interface IProductRepository
	{
		Task<Product> GetProductAsync(int productId);
		Task<Product> GetProductAsync(Guid productUuid);
		Task<int> AddProductAsync(Product product);
		Task UpdateProductAsync(Product product);
		Task DeleteProductAsync(Guid productUuid);
		Task<List<Product>> GetRelatedProductsByKeywordsAsync(Guid productUuid, List<string> keywords, int relatedItemsCount);
	}

	public class ProductRepository : IProductRepository
	{
		private DatabaseEntities databaseConnection;

		public ProductRepository(DatabaseEntities databaseConnection)
		{
			this.databaseConnection = databaseConnection;
		}

		public async Task<Product> GetProductAsync(int productId)
		{
			var product = await databaseConnection.Products
				.Include("Category")
				.Include("DiscountProducts")
				.Include("DiscountProducts.Product")
				.SingleOrDefaultAsync(x => x.ProductId == productId);

			return product;
		}

		public async Task<Product> GetProductAsync(Guid productUuid)
		{
			var product = await databaseConnection.Products
				.Include("Category")
				.Include("DiscountProducts")
				.Include("DiscountProducts.Product")
				.SingleOrDefaultAsync(x => x.ProductUUID == productUuid);

			return product;
		}

		public async Task<int> AddProductAsync(Product product)
		{
			product.DateCreated = DateTime.Now;
			product.Sold = false;

			databaseConnection.Products.Add(product);

			await databaseConnection.SaveChangesAsync();

			return product.ProductId;

		}

		public async Task UpdateProductAsync(Product product)
		{
			//databaseConnection.Products.Attach(product);

			databaseConnection.Entry(product).State = EntityState.Modified;

			await databaseConnection.SaveChangesAsync();

		}

		public async Task DeleteProductAsync(Guid productUuid)
		{
			var product = await databaseConnection.Products.Where(x => x.ProductUUID == productUuid).SingleOrDefaultAsync();

			databaseConnection.Products.Remove(product);
			await databaseConnection.SaveChangesAsync();

		}

		public async Task<List<Product>> GetRelatedProductsByKeywordsAsync(Guid productUuid, List<string> keywords, int relatedItemsCount)
		{
			//Create a Employer list for the dropdown restricted to the employers that the user can see
			var relatedProductIds = await (from e in databaseConnection.ProductKeywords
										  where (keywords.Contains(e.Keyword) && e.Product.ProductUUID != productUuid)
										  select e.ProductId).Distinct().ToListAsync();

			var shuffledAndRestrictedIds = relatedProductIds.OrderBy(a => Guid.NewGuid()).ToList();

			var relatedProductsDb = await (from e in databaseConnection.Products
									  where
										  (shuffledAndRestrictedIds.Contains(e.ProductId) && !e.Hidden && !e.Reserved && !e.Sold)
											select e).Take(relatedItemsCount).ToListAsync();

			return relatedProductsDb;
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
