using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class RelatedProductDetails
	{
		public List<Product> RelatedProducts { get; set; }
		public List<string> RelatedProductIds { get; set; }
	}
}
