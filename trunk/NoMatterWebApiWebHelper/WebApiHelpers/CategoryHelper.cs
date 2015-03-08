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
		Task<Category> GetCategoryAsync(string categoryId);
		Task<List<Product>> GetCategoryProductsAsync(string categoryId);
		Task<Product> PostCategoryProductsAsync(string categoryId, NewProduct newProduct, string token);
		Task PostCategoryAsync(Category category, string token);
		Task DeleteCategoryAsync(string categoryId, string token);
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

		public async Task<Category> GetCategoryAsync(string categoryId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/categories/{0}", categoryId));

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot get Category", response);	

				var category = await response.Content.ReadAsAsync<Category>();

				return category;
		
			}
		}

		public async Task PostCategoryAsync(Category category, string token)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

				var response = await client.PostAsJsonAsync(string.Format("api/v1/sections/{0}/categories", category.SectionId), category);

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot save Category", response);

			}
		}

		public async Task DeleteCategoryAsync(string categoryId, string token)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

				var response = await client.DeleteAsync(string.Format("api/v1/categories/{0}", categoryId));

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot delete Category", response);
			}
		}

		public async Task<List<Product>> GetCategoryProductsAsync(string categoryId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/categories/{0}/products", categoryId));

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot get Categories", response);	

				var products = await response.Content.ReadAsAsync<List<Product>>();

				return products;

			}
		}

		public async Task<Product> PostCategoryProductsAsync(string categoryId, NewProduct newProduct, string token)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

				var response = await client.PostAsJsonAsync(String.Format("api/v1/categories/{0}/products", categoryId), newProduct);

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot save Product", response);	

				var responseProduct = await response.Content.ReadAsAsync<Product>();

				return responseProduct;

			}
		}
	}
}
