using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NoMatterWebApi.Logging;

namespace NoMatterWebApi.Helpers
{
	public interface IImageDeleteHelper
	{
		void DeleteImage(string imagePath);

	}

	public class ImageDeleteHelper : IImageDeleteHelper
	{
		public void DeleteImage(string imagePath)
		{
			if (File.Exists(imagePath))
			{
				try
				{
					File.Delete(imagePath);
				}
				catch (Exception ex)
				{
					Logger.WriteGeneralError(ex);
					//throw;
				}
				
			}
		}
		
	}
}