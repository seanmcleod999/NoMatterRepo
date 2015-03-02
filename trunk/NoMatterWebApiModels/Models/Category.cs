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

        public string CategoryOrder { get; set; }

        public bool Hidden { get; set; }

        public string Picture { get; set; }
    }
}
