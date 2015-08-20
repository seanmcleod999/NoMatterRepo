using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterDataLibrary.Extensions;
using NoMatterDatabaseModel;
using Category = NoMatterWebApiModels.Models.Category;
using Product = NoMatterWebApiModels.Models.Product;

namespace NoMatterDataLibrary
{
	public interface ICategoryRepository
	{
		Task<Category> GetCategoryAsync(int categoryId);
		Task<Category> GetCategoryAsync(Guid clientUuid, int categoryId);
		Task<string> AddSectionCategoryAsync(Category category);
		Task UpdateCategoryAsync(Category category);
		Task DeleteCategoryAsync(int categoryId);
		Task<List<Product>> GetCategoryProductsAsync(int categoryId);
		Task<List<Product>> GetCategoryProductsByTypeAsync(int sectionId, int categoryId, string type);
		Task<List<Category>> GetSectionCategoriesAsync(int sectionId, bool includeHidden);
	}

	public class CategoryRepository : ICategoryRepository
	{

		public async Task<List<Category>> GetSectionCategoriesAsync(int sectionId, bool includeHidden)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var categories = mainDb.Categories
				.Include("Section")
				.Include("Products")
				.Where(x => x.Section.SectionId == sectionId);

				if (!includeHidden)
				{
					categories = categories.Where(x => !x.Hidden);
				}

				var categoriesDb = await categories.OrderBy(x => x.CategoryOrder).ToListAsync();

				return categoriesDb.Select(x => x.ToDomainCategory()).ToList();
			}
			

		}

		public async Task<Category> GetCategoryAsync(int categoryId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var category = await mainDb.Categories.SingleOrDefaultAsync(x => x.CategoryId == categoryId);

				return category.ToDomainCategory();
			}

		}

		public async Task<Category> GetCategoryAsync(Guid clientUuid, int categoryId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var category = await mainDb.Categories.Include(x=>x.Section).Include(x=>x.Section.Client).SingleOrDefaultAsync(x => x.Section.Client.ClientUUID == clientUuid && x.CategoryId == categoryId);

				return category.ToDomainCategory();
			}

		}

		public async Task<string> AddSectionCategoryAsync(Category category)
		{
			

			using (var mainDb = new DatabaseEntities())
			{
				var sectionDb = await mainDb.Sections.Where(x => x.SectionId == category.Section.SectionId).FirstOrDefaultAsync();

				var categoryDb = category.ToDatabaseCategory(sectionDb.SectionId);

				categoryDb.CategoryUUID = Guid.NewGuid();

				mainDb.Categories.Add(categoryDb);

				await mainDb.SaveChangesAsync();

				return categoryDb.CategoryUUID.ToString();

			}
			

		}

		public async Task UpdateCategoryAsync(Category category)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var categoryDb = await mainDb.Categories.Where(x => x.CategoryId == category.CategoryId).FirstOrDefaultAsync();

				categoryDb.CategoryName = category.CategoryName;
				categoryDb.CategoryFriendlyName = category.CategoryFriendlyName;
				categoryDb.CategoryDescription = category.CategoryDescription;
				categoryDb.CategoryOrder = category.CategoryOrder;
				categoryDb.Hidden = category.Hidden;
				categoryDb.Picture = category.Picture;

				await mainDb.SaveChangesAsync();
			}
		
		}

		public async Task DeleteCategoryAsync(int categoryId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var category = await mainDb.Categories.Where(x => x.CategoryId == categoryId).SingleOrDefaultAsync();

				if (category == null) return;

				mainDb.Categories.Remove(category);
				await mainDb.SaveChangesAsync();
			}
			

		}

		public async Task<List<Product>> GetCategoryProductsAsync(int categoryId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var products = await mainDb.Products
				.Where(x => x.Category.CategoryId == categoryId)
				.OrderByDescending(x => x.ReleaseDate).ToListAsync();

				return products.Select(x=>x.ToDomainProduct()).ToList();
			}
			
		}

		public async Task<List<Product>> GetCategoryProductsByTypeAsync(int sectionId, int categoryId, string type)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var todaysDate = DateTime.Now;

				IEnumerable<NoMatterDatabaseModel.Product> productsQuery = mainDb.Products.Where(x => !x.Hidden && x.ReleaseDate < todaysDate).OrderByDescending(x => x.ReleaseDate);

				if (type != null && (type.ToLower() == "latest" || type.ToLower() == "sale"))
				{
					productsQuery = productsQuery.Where(x => x.Category.SectionId == sectionId);
				}
				else
				{
					productsQuery = productsQuery.Where(x => x.Category.SectionId == sectionId && x.CategoryId == categoryId);
				}

				var productsDb =  productsQuery.ToList();

				return productsDb.Select(x => x.ToDomainProduct()).ToList();
			}
			
		}

	}
}
