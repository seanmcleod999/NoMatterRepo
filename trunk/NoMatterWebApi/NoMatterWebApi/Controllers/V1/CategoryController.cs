using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NoMatterDatabaseModel;
using NoMatterWebApi.ActionResults;
using NoMatterWebApi.CustomAuth;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Extensions;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using Category = NoMatterWebApiModels.Models.Category;
using Product = NoMatterWebApiModels.Models.Product;


namespace NoMatterWebApi.Controllers.V1
{
	//[RoutePrefix("api/v1/categories")]
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
			_generalHelper = new WebApiGeneralHelper();
			
		}

		public CategoryController(IProductRepository productRepository, ICategoryRepository categoryRepository, IGeneralHelper generalHelper)
		{
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;;
			_generalHelper = generalHelper;
		}

	
		[HttpGet]
		[Route("api/v1/clients/{clientId}/categories/{categoryId}")]
		[ResponseType(typeof(Category))]
		public async Task<IHttpActionResult> GetCategoryAsync(string clientId, string categoryid)
		{
			try
			{
				//TODO: make sure the category is part of this client

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

		[HttpPut]
		[Authorize]
		[Route("api/v1/clients/{clientId}/categories/{categoryId}")]
		public async Task<IHttpActionResult> UpdateCategoryAsync(string clientId, string categoryId, Category category)
		{
			try
			{
				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);


				//var userToken = User.Identity.Name;

				//var userDb = await _userRepository.GetClientUserByTokenAsync(userToken);
				//if (userDb == null) return new CustomBadRequest(Request, ApiResultCode.UserNotFound);

				//var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
				//if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				var categoryDb = await _categoryRepository.GetCategoryAsync(new Guid(clientId), new Guid(categoryId));
				if (categoryDb == null) return new CustomBadRequest(Request, ApiResultCode.CategoryNotFound);

				//if (userDb.Client != null && userDb.ClientId != clientDb.ClientId) return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

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


		[Authorize]
		[HttpDelete]
		[Route("api/v1/clients/{clientId}/categories/{categoryId}")]
		public async Task<IHttpActionResult> DeleteCategoryAsync(string clientId, string categoryId)
		{
			try
			{
				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				await _categoryRepository.DeleteCategoryAsync(new Guid(categoryId));

				return Ok();
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

	
		[HttpGet]
		[Route("api/v1/clients/{clientId}/categories/{categoryid}/products")]
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

				//If its the Sale category.. filter out sale items
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

		[Authorize]
		[HttpPost]
		[Route("api/v1/clients/{clientId}/categories/{categoryId}/products")]
		[ResponseType(typeof(Product))]
		public async Task<IHttpActionResult> AddCategoryProductAsync(string clientId, string categoryId, Product model)
		{
			try
			{

				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				var productUuid = Guid.NewGuid();

				var categoryDb = await _categoryRepository.GetCategoryAsync(new Guid(clientId), new Guid(categoryId));

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