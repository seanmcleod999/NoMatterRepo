using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
		
		Task UpdateProductAsync(string clientId, Product product, string token);
		Task DeleteProductAsync(string productId, string token);
	}

	public class ProductHelper : IProductHelper
	{

		public async Task<List<Product>> GetClientProductsAsync(string clientId, string productStatus, string categoryId)
		{
			var products = await WebApiService.Instance.GetAsync<List<Product>>(
					string.Format("api/v1/clients/{0}/products?status={1}&categoryId={2}", clientId, productStatus, categoryId));
			
			return products;
		}

		public async Task<Product> GetProductAsync(string productId)
		{
			var product = await WebApiService.Instance.GetAsync<Product>(string.Format("api/v1/clients/{0}/products/{1}", StaticGlobalSettings.SiteClientId, productId));
			return product;
		}

		public async Task<Product> GetProductNoRelatedProductsAsync(string productId)
		{
			var product = await WebApiService.Instance.GetAsync<Product>(string.Format("api/v1/clients/{0}/products/{1}?relatedProducts=false", StaticGlobalSettings.SiteClientId, productId));
			return product;
		}

		public async Task UpdateProductAsync(string clientId, Product product, string token)
		{
			await WebApiService.Instance.PutAsync(string.Format("api/v1/clients/{0}/products/{1}", clientId, product.ProductId), product, token);
		}

		public async Task DeleteProductAsync(string productId, string token)
		{
			await WebApiService.Instance.DeleteAsync(string.Format("api/v1/clients/{0}/products/{1}", StaticGlobalSettings.SiteClientId, productId));
		}
	}
}
