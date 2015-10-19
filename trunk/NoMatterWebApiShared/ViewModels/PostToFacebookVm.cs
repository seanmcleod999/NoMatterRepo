using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApiModels.ViewModels
{
	public class PostToFacebookVm
	{
		public string ClientUuid { get; set; }

		public Product Product { get; set; }

		public string FacebookPostText { get; set; }

		public string FacebookAlbumId { get; set; }

		public string SelectedPicturePath { get; set; }

		public List<string> FacebookAlbumList { get; set; }

		//public PostToFacebookVm()
		//{
		//	using (var mainDb = new DatabaseModelEntities())
		//	{

		//		FacebookAlbumList = mainDb.Fa.OrderBy(x => x.AlbumName).ToList();

		//	}
		//}
	}
}
