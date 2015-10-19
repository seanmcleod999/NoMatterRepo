using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Helpers
{

	public interface IFacebookHelper
	{
		int PostItemToFacebook(string facebookPostText, string facebookAlbumId, string path);
	}

	public class FacebookHelper : IFacebookHelper
	{
		public int PostItemToFacebook(string facebookPostText, string facebookAlbumId, string path)
		{
			return 123;
			
		}
	}
}