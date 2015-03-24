using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NoMatterDatabaseModel;
using NoMatterWebApi.ActionResults;
using NoMatterWebApi.CustomAuth;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Extensions;
using NoMatterWebApi.Logging;
using NoMatterWebApi.Models;
using Category = NoMatterWebApiModels.Models.Category;
using Section = NoMatterWebApiModels.Models.Section;

namespace NoMatterWebApi.Controllers.V1
{
	//[RoutePrefix("api/v1/sections")]
	public class SectionController : ApiController
	{

		private ISectionRepository _sectionRepository;
		private ICategoryRepository _categoryRepository;

		public SectionController()
		{
			var databaseEntity = new DatabaseEntities();
			_sectionRepository = new SectionRepository(databaseEntity);
			_categoryRepository = new CategoryRepository(databaseEntity);
		}

		public SectionController(ISectionRepository sectionRepository, ICategoryRepository categoryRepository)
		{
			_sectionRepository = sectionRepository;
			_categoryRepository = categoryRepository;;
		}

	
		[Route("api/v1/clients/{clientId}/sections/{sectionId}")]
		[ResponseType(typeof(List<Section>))]
		public async Task<IHttpActionResult> GetSectionAsync(string clientId, string sectionId)
		{
			var sectionDb = await _sectionRepository.GetSectionAsync(new Guid(sectionId));

			var section = sectionDb.ToDomainSection();

			return Ok(section);
		}


		[HttpPut]
		[Authorize]
		[Route("api/v1/clients/{clientId}/sections/{sectionId}")]
		public async Task<IHttpActionResult> UpdateSectionAsync(string clientId, string sectionId, Section section)
		{
			try
			{
				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				//Get the section
				var sectionDb = await _sectionRepository.GetSectionAsync(new Guid(sectionId));
				if (sectionDb == null) return new CustomBadRequest(Request, ApiResultCode.SectionNotFound);

				//Update the section
				await _sectionRepository.UpdateSectionAsync(sectionDb, section);

				return Ok();

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		[HttpDelete]
		[Authorize]
		[Route("api/v1/clients/{clientId}/sections/{sectionId}")]
		public async Task<IHttpActionResult> DeleteSectionAsync(string clientId, string sectionId)
		{
			try
			{
				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				var sectionCategories = await _categoryRepository.GetSectionCategoriesAsync(new Guid(sectionId), true);

				//Delete the default Latest Items and Sale categories
				foreach (var category in sectionCategories)
				{
					if (category.ActionName == "Latest" || category.ActionName == "Sale")
					{
						//Delete latest item and sale categories
						await _categoryRepository.DeleteCategoryAsync(category.CategoryUUID);
					}
				}

				//Delete the section
				await _sectionRepository.DeleteSectionAsync(new Guid(sectionId));

				return Ok();
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		[Route("api/v1/clients/{clientId}/sections/{sectionId}/categories")]
		[ResponseType(typeof(List<Category>))]
		public async Task<IHttpActionResult> GetSectionCategoriesAsync(string clientId, string sectionId, bool includeEmpty = false, bool includeHidden = false)
		{

			var categoriesDb = await _categoryRepository.GetSectionCategoriesAsync(new Guid(sectionId), includeHidden);

			var categories = categoriesDb.Select(x => x.ToDomainCategory()).ToList();

			if (!includeEmpty)
			{
				categories = categories.Where(x => x.VisibleProductCount > 0 || x.Conditional).ToList();
			}

			return Ok(categories);
		}

		[Authorize]
		[HttpPost]
		[Route("api/v1/clients/{clientId}/sections/{sectionId}/categories")]
		public async Task<IHttpActionResult> AddSectionCategory(string clientId, string sectionId, Category category)
		{
			try
			{
				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				var section = await _sectionRepository.GetSectionAsync(new Guid(sectionId));
				if (section == null) return new CustomBadRequest(Request, ApiResultCode.SectionNotFound);

				var categoryDb = category.ToDatabaseCategory(section.SectionId);

				//Save the section
				await _categoryRepository.AddSectionCategoryAsync(categoryDb);

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