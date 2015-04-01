using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterDatabaseModel;

namespace NoMatterWebApi.DAL
{
	public interface ICategoryRepository
	{
		Task<Category> GetCategoryAsync(int categoryId);
		Task<Category> GetCategoryAsync(Guid clientUuid, Guid categoryUuid);
		Task<string> AddSectionCategoryAsync(Category category);
		Task UpdateCategoryAsync(Category categoryDb, NoMatterWebApiModels.Models.Category category);
		Task DeleteCategoryAsync(Guid categoryUuid);
		Task<List<Product>> GetCategoryProductsAsync(Guid categoryUuid);
		Task<List<Product>> GetCategoryProductsByTypeAsync(int sectionId, int categoryId, string type);
		Task<List<Category>> GetSectionCategoriesAsync(Guid sectionUuid, bool includeHidden);
	}

	public class CategoryRepository : ICategoryRepository
	{
		private DatabaseEntities databaseConnection;

		public CategoryRepository(DatabaseEntities databaseConnection)
		{
			this.databaseConnection = databaseConnection;
		}

		public async Task<List<Category>> GetSectionCategoriesAsync(Guid sectionUuid, bool includeHidden)
		{
			var categories = databaseConnection.Categories
				.Include("Section")
				.Include("Products")
				.Where(x => x.Section.SectionUUID == sectionUuid);

			if (!includeHidden)
			{
				categories = categories.Where(x => !x.Hidden);
			}

			var categoriesDb = await categories.OrderBy(x => x.CategoryOrder).ToListAsync();

			return categoriesDb;

		}

		public async Task<Category> GetCategoryAsync(int categoryId)
		{
			var category = await databaseConnection.Categories.SingleOrDefaultAsync(x => x.CategoryId == categoryId);

			return category;
		}

		public async Task<Category> GetCategoryAsync(Guid clientUuid, Guid categoryUuid)
		{
			var category = await databaseConnection.Categories.SingleOrDefaultAsync(x => x.Section.Client.ClientUUID == clientUuid && x.CategoryUUID == categoryUuid);

			return category;
		}

		public async Task<string> AddSectionCategoryAsync(Category category)
		{
			category.CategoryUUID = Guid.NewGuid();
			
			databaseConnection.Categories.Add(category);

			await databaseConnection.SaveChangesAsync();

			return category.CategoryUUID.ToString();

		}

		public async Task UpdateCategoryAsync(Category categoryDb, NoMatterWebApiModels.Models.Category category)
		{

			databaseConnection.Categories.Attach(categoryDb);

			categoryDb.CategoryName = category.CategoryName;
			categoryDb.CategoryDescription = category.CategoryDescription;
			categoryDb.CategoryOrder = category.CategoryOrder;
			categoryDb.Hidden = category.Hidden;
			categoryDb.Picture = category.Picture;

			await databaseConnection.SaveChangesAsync();

		}

		public async Task DeleteCategoryAsync(Guid categoryUuid)
		{
			var category = await databaseConnection.Categories.Where(x => x.CategoryUUID == categoryUuid).SingleOrDefaultAsync();

			if (category == null) return;

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

		public async Task<List<Product>> GetCategoryProductsByTypeAsync(int sectionId, int categoryId, string type)
		{
			var todaysDate = DateTime.Now;

			IEnumerable<Product> products = databaseConnection.Products.Where(x=>!x.Hidden && x.ReleaseDate < todaysDate).OrderByDescending(x => x.ReleaseDate);

			if (type != null && (type.ToLower() == "latest" || type.ToLower() == "sale"))
			{
				products = products
					.Where(x => x.Category.SectionId == sectionId);
			}
			else
			{
				products = products
					.Where(x => x.Category.SectionId == sectionId && x.CategoryId == categoryId);
			}

			return products.ToList();
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
