using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace NoMatterWebApiWebHelper
{
	public interface ICategoryHelper
	{
		Task<Category> GetCategoryAsync(string categoryId);
		Task<List<Product>> GetCategoryProductsAsync(string categoryId);
		Task<Product> PostCategoryProductsAsync(string categoryId, NewProduct newProduct, string token);
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

				if (response.IsSuccessStatusCode)
				{
					var category = await response.Content.ReadAsAsync<Category>();

					return category;
				}

				throw new Exception("Cannot get Category. " + response.ToString());

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

				if (response.IsSuccessStatusCode)
				{
					var products = await response.Content.ReadAsAsync<List<Product>>();

					return products;
				}

				throw new Exception("Cannot get Categories. " + response.ToString());

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

				if (response.IsSuccessStatusCode)
				{
					var responseProduct = await response.Content.ReadAsAsync<Product>();

					return responseProduct;
				}
				else
				{
					throw new Exception("Cannot save Product. " + response.ToString());
				}
			}
		}
	}
}
