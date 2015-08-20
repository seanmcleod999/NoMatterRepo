using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CustomAuthLib;
using NoMatterDataLibrary;
using NoMatterWebApi.ActionResults;
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
			_productRepository = new ProductRepository();
			_categoryRepository = new CategoryRepository();
			_generalHelper = new GeneralHelper();
			
		}

		public CategoryController(IProductRepository productRepository, ICategoryRepository categoryRepository, IGeneralHelper generalHelper)
		{
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;;
			generalHelper = generalHelper;
		}

	
		[HttpGet]
		[Route("api/v1/clients/{clientId}/categories/{categoryId}")]
		[ResponseType(typeof(Category))]
		public async Task<IHttpActionResult> GetCategoryAsync(string clientId, int categoryid)
		{
			try
			{
				//TODO: make sure the category is part of this client

				var category = await _categoryRepository.GetCategoryAsync(new Guid(clientId), categoryid);

				if (category == null) return new CustomBadRequest(Request, ApiResultCode.CategoryNotFound);

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

				//Update the section
				await _categoryRepository.UpdateCategoryAsync(category);

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
		public async Task<IHttpActionResult> DeleteCategoryAsync(string clientId, int categoryId)
		{
			try
			{
				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				await _categoryRepository.DeleteCategoryAsync(categoryId);

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
		public async Task<IHttpActionResult> GetCategoryProducts(string clientId, int categoryid)
		{
			try
			{	
				//TODO: make sure the user can get products for this category
				//Need to get bearer token, and lookup the user so we know which client the user is from

				var category = await _categoryRepository.GetCategoryAsync(new Guid(clientId), categoryid);

				if (category == null) return new CustomBadRequest(Request, ApiResultCode.CategoryNotFound);

				var products = await _categoryRepository.GetCategoryProductsByTypeAsync(category.Section.SectionId, category.CategoryId, category.ActionName);

				//If its the Sale category.. filter out sale items
				if (category.ActionName != null && category.ActionName.ToLower() == "sale")
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
		public async Task<IHttpActionResult> AddCategoryProductAsync(string clientId, int categoryId, Product product)
		{
			try
			{

				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);


				var category = await _categoryRepository.GetCategoryAsync(new Guid(clientId), categoryId);

				if (category == null) return new CustomBadRequest(Request, ApiResultCode.CategoryNotFound);

				//var productDb = ProductHelper.GenerateProductDbModel(model, categoryDb.CategoryId, _generalHelper);

				product.CategoryId = categoryId;

				//Save the product
				await _productRepository.AddProductAsync(product);

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