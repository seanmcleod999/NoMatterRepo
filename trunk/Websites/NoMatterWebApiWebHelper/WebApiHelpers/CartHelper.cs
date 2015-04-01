using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NoMatterWebApiModels.Models;
using NoMatterWebApiWebHelper.Exceptions;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace NoMatterWebApiWebHelper.WebApiHelpers
{
	public interface ICartHelper
	{
		Task<ShoppingCartDetails> GetCartAsync(string cartId);
		Task<int> AddProductToCartAsync(string cartId, string productId, int quantity);
		Task<int> DeleteProductFromCartAsync(string cartId, string productId);
		Task EmptyCartAsync(string cartId);
	}

	public class CartHelper : ICartHelper
	{

		private IGlobalSettings _globalSettings;
		
		public CartHelper()
		{
			_globalSettings = new GlobalSettings();
		}

		public CartHelper(IGlobalSettings globalSettings)
		{
			_globalSettings = globalSettings;
		}

		public async Task<ShoppingCartDetails> GetCartAsync(string cartId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format("api/v1/cart/{0}", cartId));

				if (!response.IsSuccessStatusCode)
					GeneralHelper.HandleWebApiFailedResult(response);

				var shoppingCartDetails = await response.Content.ReadAsAsync<ShoppingCartDetails>();

				return shoppingCartDetails;
			}
		}

		public async Task<int> AddProductToCartAsync(string cartId, string productId, int quantity)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var addProductToCartModel = new AddProductToCartModel
					{
						ProductId = productId, 
						Quantity = quantity
					};

				var response = await client.PostAsJsonAsync(String.Format("api/v1/cart/{0}", cartId), addProductToCartModel);

				if (!response.IsSuccessStatusCode)
					GeneralHelper.HandleWebApiFailedResult(response);

				var cartProductCount = await response.Content.ReadAsAsync<int>();

				return cartProductCount;
			}
		}

		public async Task<int> DeleteProductFromCartAsync(string cartId, string productId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.DeleteAsync(string.Format("api/v1/cart/{0}/Product/{1}", cartId, productId));

				if (!response.IsSuccessStatusCode)
					GeneralHelper.HandleWebApiFailedResult(response);

				var cartProductCount = await response.Content.ReadAsAsync<int>();

				return cartProductCount;
			}
		}

		public async Task EmptyCartAsync(string cartId)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_globalSettings.ApiBaseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.DeleteAsync(string.Format("api/v1/cart/{0}", cartId));

				if (!response.IsSuccessStatusCode)
					GeneralHelper.HandleWebApiFailedResult(response);
			}
		}
	}
}
