using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CustomAuthLib;
using NoMatterDatabaseModel;
using NoMatterWebApi.ActionResults;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Extensions;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApiModels.Models;
using Product = NoMatterWebApiModels.Models.Product;

namespace NoMatterWebApi.Controllers.V1
{
	//[RoutePrefix("api/v1/products")]
	public class ProductController : ApiController
	{
		private IProductRepository _productRepository;
		private IGeneralHelper _generalHelper;
		
		//private IGeneralHelper _generalHelper;

		public ProductController()
		{
			var databaseEntity = new DatabaseEntities();

			_productRepository = new ProductRepository(databaseEntity);
			_generalHelper = new GeneralHelper();
		}

		public ProductController(IProductRepository productRepository, IGeneralHelper generalHelper)
		{
			_productRepository = productRepository;
			_generalHelper = generalHelper;

		}

		// GET api/v1/products/{productId}?relatedProducts=false
		[HttpGet]
		[Route("api/v1/clients/{clientId}/products/{productId}")]
		[ResponseType(typeof(Product))]
		public async Task<IHttpActionResult> GetProductAsync(string clientId, string productId, bool relatedProducts = true)
		{
			try
			{
	
				//TODO: make sure the user can access this product
				//Need to get bearer token, and lookup the user so we know which client the user is from

				var productDb = await _productRepository.GetProductAsync(new Guid(productId));

				var product = productDb.ToDomainProduct();

				if (relatedProducts)
				{

					//Get the products keywords so we can lookup similar products
					var productKeywordsList = productDb.ProductKeywords.Select(x => x.Keyword).ToList();

					//Get some related products
					var relatedProductDb =
						await _productRepository.GetRelatedProductsByKeywordsAsync(new Guid(productId), productKeywordsList, 5);

					product.RelatedProductDetails = new RelatedProductDetails()
						{
							RelatedProducts = relatedProductDb.Select(x => x.ToDomainProduct()).ToList(),
							RelatedProductIds = relatedProductDb.Select(x => x.ProductUUID.ToString()).ToList()
						};
				}

				return Ok(product);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}



		[Authorize]
		[HttpPost]
		[Route("api/v1/clients/{clientId}/products/{productId}")]
		[ResponseType(typeof(Product))]
		public async Task<IHttpActionResult> UpdateProductAsync(string clientId, string productId, Product model)
		{
			try
			{
				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				var productDb = await _productRepository.GetProductAsync(new Guid(productId));

				//productDb.CategoryId = model.CategoryId,
				productDb.Title = model.Title;
				productDb.Description = model.Description;
				productDb.Size = model.Size;
				productDb.Price = model.Price;
				productDb.Reserved = model.Reserved;
				productDb.Hidden = model.Hidden;
				productDb.AdminNotes = model.AdminNotes;
				productDb.Picture1 = model.Picture1;
				productDb.Picture2 = model.Picture2;
				productDb.Picture3 = model.Picture3;
				productDb.Picture4 = model.Picture4;
				productDb.Picture5 = model.Picture5;
				productDb.PictureOther = model.PictureOther;
				productDb.ReleaseDate = Convert.ToDateTime(model.ReleaseDate);

				productDb.Sold = model.DateSold != null;
				productDb.DateSold = model.DateSold;

				//Remove all previous keyword
				//foreach (var productKeyword in productDb.ProductKeywords)
				//{
				//	productDb.ProductKeywords.Remove(productKeyword);
				//}

				////Add all the keywords if there are any
				//if (!string.IsNullOrEmpty(model.Keywords))
				//{
				//	var keywords = model.Keywords.Split(',');

				//	foreach (var keyword in keywords)
				//	{
				//		var shopitemkeyword = productDb.ProductKeywords.Create();

				//		shopitemkeyword.ShopItemId = shopItem.ShopItemId;
				//		shopitemkeyword.keyword = keyword.Trim().ToLower();

				//		mainDb.shopitemkeywords.Add(shopitemkeyword);
				//	}
				//}

				await _productRepository.UpdateProductAsync(productDb);

				var product = productDb.ToDomainProduct();

				return Ok(product);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
			
		}

		[Authorize]
		[HttpDelete]
		[Route("api/v1/clients/{clientId}/products/{productId}")]
		public async Task<IHttpActionResult> DeleteProductAsync(string clientId, string productId)
		{
			try
			{
				//Need to delete all the images for this product
				var product = await _productRepository.GetProductAsync(new Guid(productId));
				if (product == null) return new CustomBadRequest(Request, ApiResultCode.ProductNotFound);

				ProductHelper.DeleteProductPictures(product, _generalHelper);

				await _productRepository.DeleteProductAsync(product);

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