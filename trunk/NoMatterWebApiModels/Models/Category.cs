using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
    public class Category
    {
		public string SectionId { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        public int CategoryOrder { get; set; }

		public string ActionName { get; set; }

        public bool Hidden { get; set; }

        public string Picture { get; set; }

		public int FullProductCount { get; set; }

		public int VisibleProductCount { get; set; }

		public bool Conditional { get; set; }
    }
}
