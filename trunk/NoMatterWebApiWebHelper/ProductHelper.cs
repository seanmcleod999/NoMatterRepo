﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.Exceptions;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace NoMatterWebApiWebHelper
{
	public interface IProductHelper
	{
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

				throw new WebApiException("Cannot get Product", response);

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

				throw new WebApiException("Cannot get Product", response);

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

				throw new WebApiException("Cannot update Product", response);
				
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

				if (response.IsSuccessStatusCode)
				{
					
				}
				else
				{
					throw new WebApiException("Cannot delete Product", response);
				}
			}
		}
	}
}
