using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace NoMatterWebApiWebHelper
{
	public interface IProductHelper
	{
		Task<Product> GetProductAsync(string productId);
		Task<Product> GetProductNoRelatedProductsAsync(string productId);
		Task<Product> UpdateProductAsync(Product product, string token);
	}

	public class ProductHelper : IProductHelper
	{
		private IGlobalSettings _globalSettings;

		public ProductHelper()
		{
			_globalSettings = new GlobalSettings();
		}

		public ProductHelper(IGlobalSettings globalSettings)
		{
			_globalSettings = globalSettings;
		}

		public async Task<Product> GetProductAsync(string productId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/products/{0}", productId));

				if (response.IsSuccessStatusCode)
				{
					var product = await response.Content.ReadAsAsync<Product>();

					return product;
				}

				throw new Exception("Cannot get Product. " + response.ToString());

			}
		}

		public async Task<Product> GetProductNoRelatedProductsAsync(string productId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/products/{0}?relatedProducts=false", productId));

				if (response.IsSuccessStatusCode)
				{
					var product = await response.Content.ReadAsAsync<Product>();

					return product;
				}

				throw new Exception("Cannot get Product. " + response.ToString());

			}
		}

		public async Task<Product> UpdateProductAsync(Product product, string token)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

				var response = await client.PostAsJsonAsync(String.Format("api/v1/products/{0}", product.ProductId), product);

				if (response.IsSuccessStatusCode)
				{
					var responseProduct = await response.Content.ReadAsAsync<Product>();

					return responseProduct;
				}
				else
				{
					throw new Exception("Cannot update Product. " + response.ToString());
				}
			}
		}
	}
}
