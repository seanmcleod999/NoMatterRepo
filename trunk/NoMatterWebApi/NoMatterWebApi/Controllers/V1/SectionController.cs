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
using NoMatterWebApi.Logging;
using NoMatterWebApi.Models;
using Category = NoMatterWebApiModels.Models.Category;
using Section = NoMatterWebApiModels.Models.Section;

namespace NoMatterWebApi.Controllers.V1
{
	//[Authorize]
	[RoutePrefix("api/v1/sections")]
	public class SectionController : ApiController
	{

		private ISectionRepository _sectionRepository;
		private ICategoryRepository _categoryRepository;
		private IUserRepository _userRepository;


		public SectionController()
		{
			var databaseEntity = new DatabaseEntities();

			_sectionRepository = new SectionRepository(databaseEntity);
			_categoryRepository = new CategoryRepository(databaseEntity);
			_userRepository = new UserRepository(databaseEntity);
		}

		public SectionController(ISectionRepository sectionRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
		{
			_sectionRepository = sectionRepository;
			_categoryRepository = categoryRepository;
			_userRepository = userRepository;
		}

		

		// GET api/v1/sections/{sectionUuid}
		[Route("{sectionUuid}")]
		[ResponseType(typeof(List<Section>))]
		public async Task<IHttpActionResult> GetSection(string sectionUuid)
		{

			var sectionDb = await _sectionRepository.GetSectionAsync(new Guid(sectionUuid));

			var section = sectionDb.ToDomainSection();

			return Ok(section);
		}

		// POST api/v1/sections/{sectionId}/categories
		[Authorize]
		[HttpPost]
		[Route("{sectionId}/categories")]
		public async Task<IHttpActionResult> AddSectionCategory(string sectionId, Category category)
		{
			//TODO: make sure the user can post to this category

			try
			{
				var userToken = User.Identity.Name;

				var user = await _userRepository.GetClientUserByTokenAsync(userToken);

				var section = await _sectionRepository.GetSectionAsync(new Guid(sectionId));

				if (user.ClientId != section.Client.ClientId) return BadRequest("User does not have access to this section");

				NoMatterDatabaseModel.Category categoryDb = category.ToDatabaseCategory(section.SectionId);

				//Save the section
				await _categoryRepository.AddCategoryAsync(categoryDb);

				return Ok();

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// GET api/v1/sections/{sectionUuid}/categories
		[Route("{sectionId}/categories")]
		[ResponseType(typeof(List<Category>))]
		public async Task<IHttpActionResult> GetSectionCategories(string sectionId)
		{

			var categoriesDb = await _sectionRepository.GetSectionCategoriesAsync(new Guid(sectionId));

			var categories = categoriesDb.Select(x => x.ToDomainCategory()).ToList();

			return Ok(categories);
		}

		// DELETE api/v1/sections/{sectionId}
		[HttpDelete]
		[Route("{sectionId}")]
		public async Task<IHttpActionResult> DeleteSection(string sectionId)
		{
			try
			{
				await _sectionRepository.DeleteSectionAsync(new Guid(sectionId));

				return Ok();
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		//// POST api/v1/sections>
		//[Authorize]
		//[Route("{clientUuid}/sections")]
		//[ResponseType(typeof(Section))]
		//[HttpPost]
		//public async Task<IHttpActionResult> PostClientSection(NewSection model)
		//{
		//	var section = new Section() { SectionId = "1", SectionName = "Test1" };

		//	return Ok(section);
		//}

		

		//// GET api/v1/sections/{sectionid}/categories-for-display
		//[Route("{sectionid}/categories")]
		//[ResponseType(typeof(List<Section>))]
		//public async Task<IHttpActionResult> GetSectionCategories()
		//{
		//	//Need to get bearer token, and lookup the user so we know which client the user is from

		//	var sections = new List<Section>
		//		{
		//			new Section() {SectionId = "1", SectionName = "Test1"},
		//			new Section() {SectionId = "2", SectionName = "Test2"},
		//			new Section() {SectionId = "3", SectionName = "Test3"}
		//		};

		//	return Ok(sections);
		//}

		//// GET api/v1/sections/{sectionid}/categories-for-edit
		//[Route("{sectionid}/categories-for-edit")]
		//[ResponseType(typeof(List<Section>))]
		//public async Task<IHttpActionResult> GetSectionCategoriesEdit()
		//{
		//	//Need to get bearer token, and lookup the user so we know which client the user is from

		//	var sections = new List<Section>
		//		{
		//			new Section() {SectionId = "1", SectionName = "Test1"},
		//			new Section() {SectionId = "2", SectionName = "Test2"},
		//			new Section() {SectionId = "3", SectionName = "Test3"}
		//		};

		//	return Ok(sections);
		//}
	}
}