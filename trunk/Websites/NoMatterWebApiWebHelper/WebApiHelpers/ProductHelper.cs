using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.Enums;
using NoMatterWebApiWebHelper.Exceptions;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace NoMatterWebApiWebHelper.WebApiHelpers
{
	public interface IProductHelper
	{
		Task<List<Product>> GetClientProductsAsync(string clientId, string productStatus, string categoryId);
		Task<Product> GetProductAsync(string productId);
		Task<Product> GetProductNoRelatedProductsAsync(string productId);
		Task<Product> UpdateProductAsync(Product product, string token);
		Task DeleteProductAsync(string productId, string token);
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

		public async Task<List<Product>> GetClientProductsAsync(string clientId, string productStatus, string categoryId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/clients/{0}/products?status={1}&categoryId={2}", clientId, productStatus, categoryId));

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot get Products", response);

				var products = await response.Content.ReadAsAsync<List<Product>>();

				return products;

			}
		}

		public async Task<Product> GetProductAsync(string productId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/products/{0}", productId));

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot get Product", response);	

				var product = await response.Content.ReadAsAsync<Product>();

				return product;
			
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

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot get Product", response);	

				var product = await response.Content.ReadAsAsync<Product>();
				return product;
				
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

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot update Product", response);	

				var responseProduct = await response.Content.ReadAsAsync<Product>();
				return responseProduct;
							
			}
		}

		public async Task DeleteProductAsync(string productId, string token)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

				var response = await client.DeleteAsync(string.Format("api/v1/products/{0}", productId));

				if (!response.IsSuccessStatusCode)
					throw new WebApiException("Cannot delete Product", response);
			}
		}
	}
}
