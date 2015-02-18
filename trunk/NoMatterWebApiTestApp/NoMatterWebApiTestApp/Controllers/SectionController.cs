using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NoMatterWebApiWebHelper;

namespace WebApplication7.Controllers
{
    public class SectionController : Controller
    {
		private ISectionHelper _sectionHelper;
		
		public SectionController()
		{
			_sectionHelper = new SectionHelper();
		}

		public SectionController(ISectionHelper sectionHelper)
		{
			_sectionHelper = sectionHelper;
		}

        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetSectionCategories(string sectionId)
        {

			var section = await _sectionHelper.GetSectionAsync(sectionId);

			var sectionCategories = await _sectionHelper.GetSectionCategoriesAsync(sectionId);

			return View(sectionCategories);
        }

        public async Task<ActionResult> AddSectionCategory(string sectionId)
        {
            return View();
        }


    }
}