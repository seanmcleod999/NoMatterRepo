using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CustomAuthLib;
using NoMatterDataLibrary;
using NoMatterWebApi.ActionResults;
using NoMatterWebApi.Helpers;
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
			_sectionRepository = new SectionRepository();
			_categoryRepository = new CategoryRepository();
		}

		public SectionController(ISectionRepository sectionRepository, ICategoryRepository categoryRepository)
		{
			_sectionRepository = sectionRepository;
			_categoryRepository = categoryRepository;;
		}

	
		[Route("api/v1/clients/{clientId}/sections/{sectionId}")]
		[ResponseType(typeof(List<Section>))]
		public async Task<IHttpActionResult> GetSectionAsync(string clientId, int sectionId)
		{
			var section = await _sectionRepository.GetSectionAsync(sectionId);

			return Ok(section);
		}


		[HttpPut]
		[Authorize]
		[Route("api/v1/clients/{clientId}/sections/{sectionId}")]
		public async Task<IHttpActionResult> UpdateSectionAsync(string clientId, int sectionId, Section section)
		{
			try
			{
				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				////Get the section
				//var sectionDb = await _sectionRepository.GetSectionAsync(sectionId);
				//if (sectionDb == null) return new CustomBadRequest(Request, ApiResultCode.SectionNotFound);

				//Update the section
				await _sectionRepository.UpdateSectionAsync(section);

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
		public async Task<IHttpActionResult> DeleteSectionAsync(string clientId, int sectionId)
		{
			try
			{
				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				await SectionHelper.DeleteDefaultSectionCategories(_categoryRepository, sectionId);

				//Delete the section
				await _sectionRepository.DeleteSectionAsync(sectionId);

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
		public async Task<IHttpActionResult> GetSectionCategoriesAsync(string clientId, int sectionId, bool includeEmpty = false, bool includeHidden = false)
		{

			var categories = await _categoryRepository.GetSectionCategoriesAsync(sectionId, includeHidden);

			if (!includeEmpty)
			{
				categories = categories.Where(x => x.VisibleProductCount > 0 || x.Conditional).ToList();
			}

			return Ok(categories);
		}

		[Authorize]
		[HttpPost]
		[Route("api/v1/clients/{clientId}/sections/{sectionId}/categories")]
		public async Task<IHttpActionResult> AddSectionCategory(string clientId, int sectionId, Category category)
		{
			try
			{
				//Make sure the user has access to this client
				var claimsPrincipal = (ClaimsPrincipal)User;
				var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

				if (!string.IsNullOrEmpty(authUserClientId) && clientId != authUserClientId)
					return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				var section = await _sectionRepository.GetSectionAsync(sectionId);
				if (section == null) return new CustomBadRequest(Request, ApiResultCode.SectionNotFound);

				//var categoryDb = category.ToDatabaseCategory(section.SectionId);

				//Save the section
				await _categoryRepository.AddSectionCategoryAsync(category);

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