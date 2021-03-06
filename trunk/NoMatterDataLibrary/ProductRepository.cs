﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterDataLibrary.Extensions;
using NoMatterDatabaseModel;
using Product = NoMatterWebApiModels.Models.Product;
using ProductType = NoMatterWebApiModels.Models.ProductType;

namespace NoMatterDataLibrary
{
	public interface IProductRepository
	{
		Task<Product> GetProductAsync(int productId);
		Task<Product> GetProductAsync(Guid productUuid);
		Task<string> AddProductAsync(Product product);
		Task UpdateClientShortUrlAsync(Guid productUuid, string shortUrl);
		Task UpdateProductAsync(Product product);
		Task DeleteProductAsync(int productId);
		Task<List<Product>> GetRelatedProductsByKeywordsAsync(Guid productUuid, List<string> keywords, int relatedItemsCount);
		Task<List<ProductType>> GetProductTypes();
	}

	public class ProductRepository : IProductRepository
	{
		
		public async Task<Product> GetProductAsync(int productId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var product = await mainDb.Products
				.Include("Category")
				.Include("DiscountProducts")
				.Include("DiscountProducts.Product")
				.SingleOrDefaultAsync(x => x.ProductId == productId);

				return product.ToDomainProduct();
			}
			
		}

		public async Task<Product> GetProductAsync(Guid productUuid)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var product = await mainDb.Products
								.Include("Category")
								.Include("DiscountProducts")
								.Include("DiscountProducts.Product")
								.SingleOrDefaultAsync(x => x.ProductUUID == productUuid);

				return product.ToDomainProduct();
			}
			
		}

		public async Task<string> AddProductAsync(Product product)
		{
			var productDb = product.ToDatabaseProduct();

			using (var mainDb = new DatabaseEntities())
			{
				var client = await mainDb.Clients.Where(x => x.ClientUUID == new Guid(product.ClientUuid)).FirstOrDefaultAsync();

				productDb.Client = client;
				productDb.ProductUUID = Guid.NewGuid();
				productDb.DateCreated = DateTime.Now;
				productDb.Sold = false;

				mainDb.Products.Add(productDb);

				await mainDb.SaveChangesAsync();

				return productDb.ProductUUID.ToString();

			}
			
		}

		public async Task UpdateClientShortUrlAsync(Guid productUuid, string shortUrl)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var productDb = await mainDb.Products.Where(x=>x.ProductUUID == productUuid).FirstOrDefaultAsync();

				productDb.ProductShortUrl = shortUrl;

				await mainDb.SaveChangesAsync();
			} 
		}

		public async Task UpdateProductAsync(Product product)
		{

			using (var mainDb = new DatabaseEntities())
			{
				var productDb = await mainDb.Products.FindAsync(product.ProductId);

				productDb.Title = product.Title;
				productDb.Description = product.Description;
				productDb.Price = product.Price;
				productDb.Size = product.Size;

				//TODO: the rest of the fields

				//TODO: handle the keywords

				await mainDb.SaveChangesAsync();
			}
			

		}

		public async Task DeleteProductAsync(int productId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var product = await mainDb.Products.Where(x => x.ProductId == productId).SingleOrDefaultAsync();

				mainDb.Products.Remove(product);
				await mainDb.SaveChangesAsync();
			}
		}

		

		public async Task<List<Product>> GetRelatedProductsByKeywordsAsync(Guid productUuid, List<string> keywords, int relatedItemsCount)
		{
			using (var mainDb = new DatabaseEntities())
			{
				//Create a Employer list for the dropdown restricted to the employers that the user can see
				var relatedProductIds = await (from e in mainDb.ProductKeywords
											   where (keywords.Contains(e.Keyword) && e.Product.ProductUUID != productUuid)
											   select e.ProductId).Distinct().ToListAsync();

				var shuffledAndRestrictedIds = relatedProductIds.OrderBy(a => Guid.NewGuid()).ToList();

				var relatedProductsDb = await (from e in mainDb.Products
											   where
												   (shuffledAndRestrictedIds.Contains(e.ProductId) && !e.Hidden && !e.Reserved && !e.Sold)
											   select e).Take(relatedItemsCount).ToListAsync();

				return relatedProductsDb.Select(x=>x.ToDomainProduct()).ToList();
			}

		}

		public async Task<List<ProductType>> GetProductTypes()
		{
			using (var mainDb = new DatabaseEntities())
			{
				var productTypes = await mainDb.ProductTypes.ToListAsync();

				return productTypes.Select(x => x.ToDomainProductType()).ToList();
			}
		}
	}
}
