using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NoMatterDatabaseModel;
using NoMatterWebApi.ActionResults;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Extensions;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApi.Controllers.V1
{
	[RoutePrefix("api/v1/cart")]
	public class CartController : ApiController
	{
		
		private ICartRepository _cartRepository;
		private IProductRepository _productRepository;
		private IGlobalSettings _globalSettings;
		

		public CartController()
		{
			var databaseEntity = new DatabaseEntities();

			_cartRepository = new CartRepository(databaseEntity);
			_productRepository = new ProductRepository(databaseEntity);
			_globalSettings = new GlobalSettings();
			
			
		}

		public CartController(ICartRepository cartRepository, IProductRepository productRepository, IGlobalSettings globalSettings)
		{
			_cartRepository = cartRepository;
			_productRepository = productRepository;
			_globalSettings = globalSettings;
		}

		// GET api/v1/cart/{cartId}
		[Route("{cartId}")]
		[ResponseType(typeof(ShoppingCartDetails))]
		public async Task<IHttpActionResult> GetShoppingCart(string cartId)
		{
			try
			{
				var cartProductsDb = await _cartRepository.GetCartProductsAsync(cartId);

				var cartProducts = cartProductsDb.Select(x => x.ToDomainCartProduct()).ToList();

				var shoppingCartDetails = new ShoppingCartDetails();

				shoppingCartDetails.Products = cartProducts.Select(x=>x.Product).ToList();
				shoppingCartDetails.TotalAmount = cartProducts.Select(x => x.Product).Sum(x => x.DiscountDetails.DiscountedPrice); //Todo: sal prices ??

				return Ok(shoppingCartDetails);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}

		}

		// POST api/v1/cart/{cartId}
		[Route("{cartId}")]
		[HttpPost]
		public async Task<IHttpActionResult> AddProductToCart(string cartId, AddProductToCartModel model)
		{

			//TODO: if the product allows more than one order and the product already exists in the cart.. update the quantity
			try
			{
				var cartProduct = await _cartRepository.GetCartProductAsync(cartId, new Guid(model.ProductId));

				if (cartProduct == null)
				{
					var product = await _productRepository.GetProductAsync(new Guid(model.ProductId));

					if (product == null) return new CustomBadRequest(Request, ApiResultCode.ProductNotFound);

					cartProduct = new CartProduct
					{
						CartId = cartId,
						Product = product,
						Quantity = Convert.ToInt16(model.Quantity)
					};

					await _cartRepository.AddCartProductAsync(cartProduct);

				}

				var cartProductCount = await _cartRepository.GetCartProductCountAsync(cartId);
				
				return Ok(cartProductCount);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// DELETE api/v1/cart/{cartId}/product/{productId}
		[Route("{cartId}/product/{productId}")]
		[HttpDelete]
		public async Task<IHttpActionResult> DeleteProductFromCart(string cartId, string productId)
		{
			//TODO: IF deleting and the quanity is more than 1... remove 1 from quanity

			try
			{
				var cartProduct = await _cartRepository.GetCartProductAsync(cartId, new Guid(productId));

				if (cartProduct != null)
				{
					await _cartRepository.DeleteCartProductAsync(cartProduct);
				}

				var cartProductCount = await _cartRepository.GetCartProductCountAsync(cartId);

				return Ok(cartProductCount);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// DELETE api/v1/cart/{cartId}
		[Route("{cartId}")]
		[HttpDelete]
		public async Task<IHttpActionResult> DeleteCart(string cartId)
		{
			try
			{
				await _cartRepository.DeleteCartProductsAsync(cartId);

				return Ok();
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

	}
}