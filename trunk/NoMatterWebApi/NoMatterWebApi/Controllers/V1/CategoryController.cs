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
using NoMatterWebApi.Logging;
using NoMatterWebApi.Models;
using NoMatterWebApiModels.Models;
using Category = NoMatterWebApi.Models.Category;
using Product = NoMatterWebApiModels.Models.Product;
using Section = NoMatterWebApiModels.Models.Section;

namespace NoMatterWebApi.Controllers.V1
{
	[RoutePrefix("api/v1/categories")]
	public class CategoryController : ApiController
	{

		private IProductRepository _productRepository;
		private ICategoryRepository _categoryRepository;

		public CategoryController()
		{
			var databaseEntity = new DatabaseEntities();

			_productRepository = new ProductRepository(databaseEntity);
			_categoryRepository = new CategoryRepository(databaseEntity);
			
		}

		public CategoryController(IProductRepository productRepository, ICategoryRepository categoryRepository)
		{
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;

		}

		// POST api/v1/category/{categoryid}/products
		[HttpPost]
		[Route("{categoryid}/products")]
		[ResponseType(typeof (Product))]
		public async Task<IHttpActionResult> AddCategoryProduct(string categoryid, NewProduct model)
		{
			//TODO: make sure the user can post to this category

			try
			{		
				var userToken = User.Identity.Name;


				var categoryDb = await _categoryRepository.GetCategoryAsync(new Guid(categoryid));

				if (categoryDb == null) return BadRequest("CategoryNotFound");

				var productDb = new NoMatterApiDAL.DatabaseModel.Product
					{
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
						ReleaseDate = Convert.ToDateTime(model.ReleaseDate)
					};

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

				var productUuid = await _productRepository.AddProductAsync(productDb);

				var product = await _productRepository.GetProductAsync(new Guid(productUuid));

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
			//TODO: make sure the user can access this category
			//Need to get bearer token, and lookup the user so we know which client the user is from

			var categoryDb = await _categoryRepository.GetCategoryAsync(new Guid(categoryid));

			var category = categoryDb.ToDomainCategory();

			return Ok(category);
		}

		// GET api/v1/category/{categoryid}/products
		[HttpGet]
		[Route("{categoryid}/products")]
		[ResponseType(typeof(List<Product>))]
		public async Task<IHttpActionResult> GetCategoryProducts(string categoryid)
		{
			//TODO: make sure the user can get products for this category
			//Need to get bearer token, and lookup the user so we know which client the user is from

			var productsDb = await _categoryRepository.GetCategoryProductsAsync(new Guid(categoryid));

			var products = productsDb.Select(x => x.ToDomainProduct()).ToList();

			return Ok(products);
		}

		// GET api/v1/category/{categoryid}/products-for-edit
		[Route("{categoryid}/products-for-edit")]
		[ResponseType(typeof(List<Section>))]
		public async Task<IHttpActionResult> GetCategoryProductsEdit()
		{
			//Need to get bearer token, and lookup the user so we know which client the user is from

			var sections = new List<Section>
				{
					new Section() {SectionId = "1", SectionName = "Test1"},
					new Section() {SectionId = "2", SectionName = "Test2"},
					new Section() {SectionId = "3", SectionName = "Test3"}
				};

			return Ok(sections);
		}
	}
}