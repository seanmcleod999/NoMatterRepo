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
using NoMatterWebApiModels.Models;
using Product = NoMatterWebApiModels.Models.Product;

namespace NoMatterWebApi.Controllers.V1
{
	[RoutePrefix("api/v1/products")]
	public class ProductController : ApiController
	{
		private IProductRepository _productRepository;

		public ProductController()
		{
			var databaseEntity = new DatabaseEntities();

			_productRepository = new ProductRepository(databaseEntity);		
		}

		// GET api/v1/products/{productId}?relatedProducts=false
		[HttpGet]
		[Route("{productId}")]
		[ResponseType(typeof(Product))]
		public async Task<IHttpActionResult> GetProduct(string productId, bool relatedProducts = true)
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

		// GET api/<controller>/5
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<controller>
		public void Post([FromBody]string value)
		{
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/5
		public void Delete(int id)
		{
		}
	}
}