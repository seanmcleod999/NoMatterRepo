using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NoMatterDatabaseModel;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Extensions;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApi.Models;
using NoMatterWebApiModels.Models;
using Category = NoMatterWebApiModels.Models.Category;
using Product = NoMatterWebApiModels.Models.Product;
using Section = NoMatterWebApiModels.Models.Section;

namespace NoMatterWebApi.Controllers.V1
{
	[RoutePrefix("api/v1/categories")]
	public class CategoryController : ApiController
	{

		private IProductRepository _productRepository;
		private ICategoryRepository _categoryRepository;
		private IGeneralHelper _generalHelper;
		

		public CategoryController()
		{
			var databaseEntity = new DatabaseEntities();

			_productRepository = new ProductRepository(databaseEntity);
			_categoryRepository = new CategoryRepository(databaseEntity);
			_generalHelper = new GeneralHelper();
			
			
		}

		public CategoryController(IProductRepository productRepository, ICategoryRepository categoryRepository, IGeneralHelper generalHelper)
		{
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
			_generalHelper = generalHelper;
		}

		// POST api/v1/category/{categoryid}/products
		[HttpPost]
		[Route("{categoryid}/products")]
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

				var productDb = new NoMatterDatabaseModel.Product
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

					foreach (var productKeyword in keywords.Select(keyword => new NoMatterDatabaseModel.ProductKeyword
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
	
		// GET api/v1/category/{categoryid}
		[HttpGet]
		[Route("{categoryid}")]
		[ResponseType(typeof(Category))]
		public async Task<IHttpActionResult> GetCategory(string categoryid)
		{
			try
			{
				//TODO: make sure the user can access this category
				//Need to get bearer token, and lookup the user so we know which client the user is from

				var categoryDb = await _categoryRepository.GetCategoryAsync(new Guid(categoryid));

				var category = categoryDb.ToDomainCategory();

				return Ok(category);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// GET api/v1/category/{categoryid}/products
		[HttpGet]
		[Route("{categoryid}/products")]
		[ResponseType(typeof(List<Product>))]
		public async Task<IHttpActionResult> GetCategoryProducts(string categoryid)
		{
			try
			{	
				//TODO: make sure the user can get products for this category
				//Need to get bearer token, and lookup the user so we know which client the user is from

				var productsDb = await _categoryRepository.GetCategoryProductsAsync(new Guid(categoryid));

				var products = productsDb.Select(x => x.ToDomainProduct()).ToList();

				return Ok(products);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

	}
}