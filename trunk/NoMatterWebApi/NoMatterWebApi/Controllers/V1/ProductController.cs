using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NoMatterApiDAL.DatabaseModel;
using NoMatterApiDAL.Repositories;
using NoMatterWebApi.Extensions;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApiModels.Models;
using Product = NoMatterWebApiModels.Models.Product;

namespace NoMatterWebApi.Controllers.V1
{
	[RoutePrefix("api/v1/products")]
	public class ProductController : ApiController
	{
		private IProductRepository _productRepository;
		private ICategoryRepository _categoryRepository;
		private IGeneralHelper _generalHelper;

		public ProductController()
		{
			var databaseEntity = new DatabaseEntities();

			_productRepository = new ProductRepository(databaseEntity);
			_categoryRepository = new CategoryRepository(databaseEntity);
			_generalHelper = new GeneralHelper();
		}

		public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IGeneralHelper generalHelper)
		{
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
			_generalHelper = generalHelper;
		}

		// GET api/v1/products/{productId}?relatedProducts=false
		[HttpGet]
		[Route("{productId}")]
		[ResponseType(typeof(Product))]
		public async Task<IHttpActionResult> GetProduct(string productId, bool relatedProducts = true)
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

		// POST api/v1/category/{categoryid}/products
		[HttpPost]
		[Route("~/api/v1/categories/{categoryid}/products")]
		[ResponseType(typeof(Product))]
		public async Task<IHttpActionResult> AddProduct(string categoryid, NewProduct model)
		{
			//TODO: make sure the user can post to this category

			try
			{
				var userToken = User.Identity.Name;

				var productUuid = Guid.NewGuid();

				var categoryDb = await _categoryRepository.GetCategoryAsync(new Guid(categoryid));

				if (categoryDb == null) return BadRequest("CategoryNotFound");

				var productDb = new NoMatterApiDAL.DatabaseModel.Product
				{
					ProductUUID = productUuid,
					Category = categoryDb,
					Title = model.Title,
					Description = model.Description,
					Size = model.Size,
					Price = model.Price,
					Reserved = model.Reserved,
					Hidden = model.Hidden,
					AdminNotes = model.AdminNotes,
					Picture1 = model.Picture1,
					Picture2 = model.Picture2,
					Picture3 = model.Picture3,
					Picture4 = model.Picture4,
					Picture5 = model.Picture5,
					PictureOther = model.PictureOther,
					ReleaseDate = Convert.ToDateTime(model.ReleaseDate),

				};

				//Handle the keywords
				if (model.Keywords != null)
				{
					var keywords = model.Keywords.Split(',');

					foreach (var productKeyword in keywords.Select(keyword => new NoMatterApiDAL.DatabaseModel.ProductKeyword
					{
						Product = productDb,
						Keyword = keyword.Trim().ToLower()
					}))
					{
						productDb.ProductKeywords.Add(productKeyword);
					}
				}

				//Generate the short url
				productDb.ProductShortUrl = _generalHelper.MakeGoogleShortUrl(model.ViewProductUrl + productUuid);

				//Save the product
				var productId = await _productRepository.AddProductAsync(productDb);

				//Get the product to return the details
				var product = await _productRepository.GetProductAsync(productId);

				//TODO: change to created response
				return Ok(product.ToDomainProduct());

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// POST api/v1/products/{productId}
		[HttpPost]
		[Route("{productId}")]
		[ResponseType(typeof(Product))]
		public async Task<IHttpActionResult> UpdateProduct(string productId, Product model)
		{
			try
			{
				//TODO: make sure the user can update this product
				//Need to get bearer token, and lookup the user so we know which client the user is from

				var productDb = await _productRepository.GetProductAsync(new Guid(productId));

				var product = productDb.ToDomainProduct();



				return Ok(product);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
			
		}	

		
	}
}