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
using NoMatterWebApiWebHelper.WebApiHelpers;
using Product = NoMatterWebApiModels.Models.Product;

namespace NoMatterWebApi.Controllers.V1
{
	//[RoutePrefix("api/v1/products")]
	public class ProductController : ApiController
	{
		private IProductRepository _productRepository;
		private IWebApiGlobalSettings _webApiGlobalSettings;
		private IImageDeleteHelper _imageDeleteHelper;
		
		//private IGeneralHelper _generalHelper;

		public ProductController()
		{
			var databaseEntity = new DatabaseEntities();

			_productRepository = new ProductRepository(databaseEntity);
			_imageDeleteHelper = new ImageDeleteHelper();
			//_categoryRepository = new CategoryRepository(databaseEntity);
			//_generalHelper = new GeneralHelper();

			_webApiGlobalSettings = new WebApiGlobalSettings();
		}

		public ProductController(IProductRepository productRepository, IImageDeleteHelper imageDeleteHelper)
		{
			_productRepository = productRepository;
			_imageDeleteHelper = new ImageDeleteHelper();
			//_categoryRepository = categoryRepository;
			//_generalHelper = generalHelper;
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

		

		// POST api/v1/products/{productId}
		[HttpPost]
		[Route("api/v1/clients/{clientId}/products/{productId}")]
		[ResponseType(typeof(Product))]
		public async Task<IHttpActionResult> UpdateProductAsync(string clientId, string productId, Product model)
		{
			try
			{
				//TODO: make sure the user can update this product
				//Need to get bearer token, and lookup the user so we know which client the user is from

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

		// DELETE api/v1/products/{productId}
		[HttpDelete]
		[Route("api/v1/clients/{clientId}/products/{productId}")]
		public async Task<IHttpActionResult> DeleteProductAsync(string clientId, string productId)
		{
			try
			{
				//Need to delete all the images for this product
				var product = await _productRepository.GetProductAsync(new Guid(productId));

				if (product == null) return new CustomBadRequest(Request, ApiResultCode.ProductNotFound);

				var imagesPath = System.Web.HttpContext.Current.Server.MapPath("~\\images") + "/";

				if (!string.IsNullOrEmpty(product.Picture1)) _imageDeleteHelper.DeleteImage(imagesPath + product.Picture1);
				if (!string.IsNullOrEmpty(product.Picture2)) _imageDeleteHelper.DeleteImage(imagesPath + product.Picture2);
				if (!string.IsNullOrEmpty(product.Picture3)) _imageDeleteHelper.DeleteImage(imagesPath + product.Picture3);
				if (!string.IsNullOrEmpty(product.Picture4)) _imageDeleteHelper.DeleteImage(imagesPath + product.Picture4);
				if (!string.IsNullOrEmpty(product.Picture5)) _imageDeleteHelper.DeleteImage(imagesPath + product.Picture5);
				if (!string.IsNullOrEmpty(product.PictureOther)) _imageDeleteHelper.DeleteImage(imagesPath + product.PictureOther);

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