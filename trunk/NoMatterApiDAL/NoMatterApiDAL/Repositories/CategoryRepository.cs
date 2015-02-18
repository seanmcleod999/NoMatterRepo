using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterApiDAL.DatabaseModel;

namespace NoMatterApiDAL.Repositories
{
	public interface ICategoryRepository
	{
		Task<Category> GetCategoryAsync(int categoryId);
		Task<Category> GetCategoryAsync(Guid categoryUuid);
		Task<string> AddCategoryAsync(Category category);
		void UpdateCategoryAsync(Category category);
		void DeleteCategoryAsync(Guid categoryUuid);
		Task<List<Product>> GetCategoryProductsAsync(Guid categoryUuid);
	}

	public class CategoryRepository : ICategoryRepository
	{
		private DatabaseEntities databaseConnection;

		public CategoryRepository(DatabaseEntities databaseConnection)
		{
			this.databaseConnection = databaseConnection;
		}

		public async Task<Category> GetCategoryAsync(int categoryId)
		{
			var category = await databaseConnection.Categories.SingleOrDefaultAsync(x => x.CategoryId == categoryId);

			return category;
		}

		public async Task<Category> GetCategoryAsync(Guid categoryUuid)
		{
			var category = await databaseConnection.Categories.SingleOrDefaultAsync(x => x.CategoryUUID == categoryUuid);

			return category;
		}

		public async Task<string> AddCategoryAsync(Category category)
		{
			category.CategoryUUID = Guid.NewGuid();
			
			databaseConnection.Categories.Add(category);

			await databaseConnection.SaveChangesAsync();

			return category.CategoryUUID.ToString();

		}

		public async void UpdateCategoryAsync(Category category)
		{
			databaseConnection.Categories.Attach(category);

			await databaseConnection.SaveChangesAsync();

		}

		public async void DeleteCategoryAsync(Guid categoryUuid)
		{
			var category = await databaseConnection.Categories.Where(x => x.CategoryUUID == categoryUuid).SingleOrDefaultAsync();

			databaseConnection.Categories.Remove(category);
			await databaseConnection.SaveChangesAsync();

		}

		public async Task<List<Product>> GetCategoryProductsAsync(Guid categoryUuid)
		{
			var products = await databaseConnection.Products
				.Where(x => x.Category.CategoryUUID == categoryUuid)
				.OrderByDescending(x=>x.ReleaseDate).ToListAsync();

			return products;
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
