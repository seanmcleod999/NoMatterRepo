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
using NoMatterWebApi.Logging;
using NoMatterWebApi.Models;
using Category = NoMatterWebApiModels.Models.Category;
using Section = NoMatterWebApiModels.Models.Section;

namespace NoMatterWebApi.Controllers.V1
{
	//[Authorize]
	//[RoutePrefix("api/v1/sections")]
	public class SectionController : ApiController
	{

		private ISectionRepository _sectionRepository;
		private ICategoryRepository _categoryRepository;
		private IUserRepository _userRepository;
		private IClientRepository _clientRepository;


		public SectionController()
		{
			var databaseEntity = new DatabaseEntities();

			_sectionRepository = new SectionRepository(databaseEntity);
			_categoryRepository = new CategoryRepository(databaseEntity);
			_userRepository = new UserRepository(databaseEntity);
			_clientRepository = new ClientRepository(databaseEntity);
		}

		public SectionController(ISectionRepository sectionRepository, ICategoryRepository categoryRepository, IUserRepository userRepository, IClientRepository clientRepository)
		{
			_sectionRepository = sectionRepository;
			_categoryRepository = categoryRepository;
			_userRepository = userRepository;
			_clientRepository = clientRepository;
		}

		

		// GET api/v1/sections/{sectionUuid}
		[Route("api/v1/clients/{clientId}/sections/{sectionId}")]
		[ResponseType(typeof(List<Section>))]
		public async Task<IHttpActionResult> GetSectionAsync(string clientId, string sectionId)
		{

			var sectionDb = await _sectionRepository.GetSectionAsync(new Guid(sectionId));

			var section = sectionDb.ToDomainSection();

			return Ok(section);
		}


		// POST api/v1/sections
		[HttpPut]
		[Authorize]
		[Route("api/v1/clients/{clientId}/sections/{sectionId}")]
		public async Task<IHttpActionResult> UpdateSectionAsync(string clientId, string sectionId, Section section)
		{
			//TODO: make sure the user can post to this category

			try
			{
				var userToken = User.Identity.Name;

				var userDb = await _userRepository.GetClientUserByTokenAsync(userToken);
				if (userDb == null) return new CustomBadRequest(Request, ApiResultCode.UserNotFound);

				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
				if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				var sectionDb = await _sectionRepository.GetSectionAsync(new Guid(sectionId));
				if (sectionDb == null) return new CustomBadRequest(Request, ApiResultCode.SectionNotFound);

				if (userDb.Client != null && userDb.ClientId != clientDb.ClientId) return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

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

		// DELETE api/v1/sections/{sectionId}
		[HttpDelete]
		[Route("api/v1/clients/{clientId}/sections/{sectionId}")]
		public async Task<IHttpActionResult> DeleteSectionAsync(string clientId, string sectionId)
		{
			try
			{
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

				await _sectionRepository.DeleteSectionAsync(new Guid(sectionId));

				return Ok();
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// GET api/v1/sections/{sectionUuid}/categories
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

		// POST api/v1/sections/{sectionId}/categories
		[Authorize]
		[HttpPost]
		[Route("api/v1/clients/{clientId}/sections/{sectionId}/categories")]
		public async Task<IHttpActionResult> AddSectionCategory(string clientId, string sectionId, Category category)
		{
			//TODO: make sure the user can post to this category

			try
			{
				var userToken = User.Identity.Name;

				var user = await _userRepository.GetClientUserByTokenAsync(userToken);

				var section = await _sectionRepository.GetSectionAsync(new Guid(sectionId));

				if (section == null) return new CustomBadRequest(Request, ApiResultCode.SectionNotFound);

				if (user.ClientId != null && user.ClientId != section.Client.ClientId) return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				NoMatterDatabaseModel.Category categoryDb = category.ToDatabaseCategory(section.SectionId);

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