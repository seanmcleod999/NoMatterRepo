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
				client.BaseAddress = new Uri(_globalSettings.BaseAddress);
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
				client.BaseAddress = new Uri(_globalSettings.BaseAddress);
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
	}
}
