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
		Task<Product> PostCategoryProductAsync(string clientId, string categoryId, Product newProduct, string token);
		Task UpdateCategoryAsync(string clientId, Category category, string token);
		Task DeleteCategoryAsync(string clientId, string categoryId, string token);
	}

	public class CategoryHelper : ICategoryHelper
	{
		private IGlobalSettings _globalSettings;
		
		public CategoryHelper()
		{
			_globalSettings = new GlobalSettings();
		}

		public CategoryHelper(IGlobalSettings globalSettings)
		{
			_globalSettings = globalSettings;
		}

		public async Task<Category> GetCategoryAsync(string clientId, string categoryId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/clients/{0}/categories/{1}", clientId, categoryId));

				if (!response.IsSuccessStatusCode)
				{
					GeneralHelper.HandleWebApiFailedResult(response);
				}

				var category = await response.Content.ReadAsAsync<Category>();

				return category;
		
			}
		}

		

		public async Task DeleteCategoryAsync(string clientId, string categoryId, string token)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

				var response = await client.DeleteAsync(string.Format("api/v1/clients/{0}/categories/{1}", clientId, categoryId));

				if (!response.IsSuccessStatusCode)
				{
					GeneralHelper.HandleWebApiFailedResult(response);
				}
			}
		}

		public async Task<List<Product>> GetCategoryProductsAsync(string clientId, string categoryId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/clients/{0}/categories/{1}/products", clientId, categoryId));

				if (!response.IsSuccessStatusCode)
				{
					GeneralHelper.HandleWebApiFailedResult(response);
				}	

				var products = await response.Content.ReadAsAsync<List<Product>>();

				return products;

			}
		}

		public async Task<Product> PostCategoryProductAsync(string clientId, string categoryId, Product newProduct, string token)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

				var response = await client.PostAsJsonAsync(String.Format("api/v1/clients/{0}/categories/{1}/products", clientId, categoryId), newProduct);

				if (!response.IsSuccessStatusCode)
					GeneralHelper.HandleWebApiFailedResult(response);

				var responseProduct = await response.Content.ReadAsAsync<Product>();

				return responseProduct;

			}
		}

		public async Task UpdateCategoryAsync(string clientId, Category category, string token)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

				var response = await client.PutAsJsonAsync(string.Format("api/v1/clients/{0}/categories/{1}", clientId, category.CategoryId), category);

				if (!response.IsSuccessStatusCode)
					GeneralHelper.HandleWebApiFailedResult(response);

			}
		}
	}
}
