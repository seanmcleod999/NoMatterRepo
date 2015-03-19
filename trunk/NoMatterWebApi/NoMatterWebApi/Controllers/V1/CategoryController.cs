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
using NoMatterWebApi.Models;
using NoMatterWebApiModels.Models;
using Category = NoMatterWebApiModels.Models.Category;
using Product = NoMatterWebApiModels.Models.Product;
using Section = NoMatterWebApiModels.Models.Section;

namespace NoMatterWebApi.Controllers.V1
{
	//[RoutePrefix("api/v1/categories")]
	public class CategoryController : ApiController
	{
		private IUserRepository _userRepository;
		private IProductRepository _productRepository;
		private ISectionRepository _sectionRepository;
		private ICategoryRepository _categoryRepository;
		private IClientRepository _clientRepository;
		private IGeneralHelper _generalHelper;
		private IWebApiGlobalSettings _webApiGlobalSettings;
		

		public CategoryController()
		{
			var databaseEntity = new DatabaseEntities();

			_productRepository = new ProductRepository(databaseEntity);
			_categoryRepository = new CategoryRepository(databaseEntity);
			_sectionRepository = new SectionRepository(databaseEntity);
			_userRepository = new UserRepository(databaseEntity);
			_clientRepository = new ClientRepository(databaseEntity);
			_generalHelper = new WebApiGeneralHelper();
			_webApiGlobalSettings = new WebApiGlobalSettings();
			
		}

		public CategoryController(IProductRepository productRepository, ICategoryRepository categoryRepository, ISectionRepository sectionRepository, IClientRepository clientRepository, IGeneralHelper generalHelper)
		{
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
			_sectionRepository = sectionRepository;
			_clientRepository = clientRepository;
			_generalHelper = generalHelper;
		}

		
	
		// GET api/v1/category/{categoryid}
		[System.Web.Http.HttpGet]
		[System.Web.Http.Route("api/v1/clients/{clientId}/categories/{categoryId}")]
		[ResponseType(typeof(Category))]
		public async Task<IHttpActionResult> GetCategoryAsync(string clientId, string categoryid)
		{
			try
			{
				//TODO: make sure the user can access this category
				//Need to get bearer token, and lookup the user so we know which client the user is from

				var categoryDb = await _categoryRepository.GetCategoryAsync(new Guid(clientId), new Guid(categoryid));

				if (categoryDb == null) return new CustomBadRequest(Request, ApiResultCode.CategoryNotFound);

				var category = categoryDb.ToDomainCategory();

				return Ok(category);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// POST api/v1/sections
		[System.Web.Http.HttpPut]
		[System.Web.Http.Authorize]
		[System.Web.Http.Route("api/v1/clients/{clientId}/categories/{categoryId}")]
		public async Task<IHttpActionResult> UpdateCategoryAsync(string clientId, string categoryId, Category category)
		{
			//TODO: make sure the user can post to this category

			try
			{
				var userToken = User.Identity.Name;

				var userDb = await _userRepository.GetClientUserByTokenAsync(userToken);
				if (userDb == null) return new CustomBadRequest(Request, ApiResultCode.UserNotFound);

				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
				if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				var categoryDb = await _categoryRepository.GetCategoryAsync(new Guid(clientId), new Guid(categoryId));
				if (categoryDb == null) return new CustomBadRequest(Request, ApiResultCode.CategoryNotFound);

				if (userDb.Client != null && userDb.ClientId != clientDb.ClientId) return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				//Update the section
				await _categoryRepository.UpdateCategoryAsync(categoryDb, category);

				return Ok();

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// DELETE api/v1/categories/{categoryid}
		[System.Web.Http.HttpDelete]
		[System.Web.Http.Route("api/v1/clients/{clientId}/categories/{categoryId}")]
		public async Task<IHttpActionResult> DeleteCategoryAsync(string categoryId)
		{
			//TODO: make sure the user can delete this category

			try
			{
				await _categoryRepository.DeleteCategoryAsync(new Guid(categoryId));

				return Ok();
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		



		// GET api/v1/category/{categoryid}/products
		[System.Web.Http.HttpGet]
		[System.Web.Http.Route("api/v1/clients/{clientId}/categories/{categoryid}/products")]
		[ResponseType(typeof(List<Product>))]
		public async Task<IHttpActionResult> GetCategoryProducts(string clientId, string categoryid)
		{
			try
			{	
				//TODO: make sure the user can get products for this category
				//Need to get bearer token, and lookup the user so we know which client the user is from

				var categoryDb = await _categoryRepository.GetCategoryAsync(new Guid(clientId), new Guid(categoryid));

				if (categoryDb == null) return new CustomBadRequest(Request, ApiResultCode.CategoryNotFound);

				var productsDb = await _categoryRepository.GetCategoryProductsByTypeAsync(categoryDb.SectionId, categoryDb.CategoryId, categoryDb.ActionName);

				var products = productsDb.Select(x => x.ToDomainProduct()).ToList();

				//If its he Sale category.. filter out sale items
				if (categoryDb.ActionName != null && categoryDb.ActionName.ToLower() == "sale")
				{
					products = products.Where(x => x.DiscountDetails.Discounted).ToList();
				}

				return Ok(products);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// POST api/v1/category/{categoryid}/products
		[System.Web.Http.HttpPost]
		[System.Web.Http.Route("api/v1/clients/{clientId}/categories/{categoryid}/products")]
		[ResponseType(typeof(Product))]
		public async Task<IHttpActionResult> AddCategoryProductAsync(string clientId, string categoryid, Product model)
		{
			//TODO: make sure the user can post to this category

			try
			{
				var userToken = User.Identity.Name;

				var productUuid = Guid.NewGuid();

				var categoryDb = await _categoryRepository.GetCategoryAsync(new Guid(clientId), new Guid(categoryid));

				if (categoryDb == null) return new CustomBadRequest(Request, ApiResultCode.CategoryNotFound);

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

	}
}