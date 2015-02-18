using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
//using NoMatterApiDAL.DatabaseModel;
using NoMatterApiDAL.DatabaseModel;
using NoMatterApiDAL.Repositories;
using NoMatterWebApi.Extensions;
using NoMatterWebApi.Models;
using Category = NoMatterWebApi.Models.Category;

namespace NoMatterWebApi.Controllers.V1
{
	//[Authorize]
	[RoutePrefix("api/v1/sections")]
	public class SectionController : ApiController
	{

		private ISectionRespository _sectionRepository;


		public SectionController()
		{
			var databaseEntity = new DatabaseEntities();

			_sectionRepository = new SectionRespository(databaseEntity);
		}

		public SectionController(ISectionRespository sectionRepository)
		{
			_sectionRepository = sectionRepository;
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

		// GET api/v1/sections/{sectionUuid}/categories
		[Route("{sectionUuid}/categories")]
		[ResponseType(typeof(List<Category>))]
		public async Task<IHttpActionResult> GetSectionCategories(string sectionUuid)
		{

			var categoriesDb = await _sectionRepository.GetSectionCategoriesAsync(new Guid(sectionUuid));

			var categories = categoriesDb.Select(x => x.ToDomainCategory()).ToList();

			return Ok(categories);
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