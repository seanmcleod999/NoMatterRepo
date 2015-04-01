using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.Exceptions;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace NoMatterWebApiWebHelper.WebApiHelpers
{
	public interface ICategoryHelper
	{
		Task<Category> GetCategoryAsync(string clientId, string categoryId);
		Task<List<Product>> GetCategoryProductsAsync(string clientId, string categoryId);
		Task PostCategoryProductAsync(string clientId, string categoryId, Product newProduct, string token);
		Task UpdateCategoryAsync(string clientId, Category category, string token);
		Task DeleteCategoryAsync(string clientId, string categoryId, string token);
	}

	public class CategoryHelper : ICategoryHelper
	{

		public async Task<Category> GetCategoryAsync(string clientId, string categoryId)
		{
			var category = await WebApiService.Instance.GetAsync<Category>(string.Format("api/v1/clients/{0}/categories/{1}", clientId, categoryId));

			return category;
		}

		public async Task DeleteCategoryAsync(string clientId, string categoryId, string token)
		{
			await WebApiService.Instance.DeleteAsync(string.Format("api/v1/clients/{0}/categories/{1}", clientId, categoryId));
		}

		public async Task<List<Product>> GetCategoryProductsAsync(string clientId, string categoryId)
		{
			var products = await WebApiService.Instance.GetAsync<List<Product>>(string.Format("api/v1/clients/{0}/categories/{1}/products", clientId, categoryId));

			return products;
		}

		public async Task PostCategoryProductAsync(string clientId, string categoryId, Product product, string token)
		{
			await WebApiService.Instance.PostAsync(string.Format("api/v1/clients/{0}/categories/{1}/products", clientId, categoryId), product, token);
		}

		public async Task UpdateCategoryAsync(string clientId, Category category, string token)
		{
			await WebApiService.Instance.PutAsync(string.Format("api/v1/clients/{0}/categories/{1}", clientId, category.CategoryId), category, token);
		}
	}
}
