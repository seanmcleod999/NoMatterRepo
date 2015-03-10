using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterWebApiWebHelper.Enums;
using System.Web;

namespace NoMatterWebApiWebHelper.OtherHelpers
{
	public interface IPictureUploadSettings
	{
		int ThumbnailMaxPictureSize { get; }
		string PicturePath { get; }
		int MaxPictureSize { get; }
		bool GenerateThumbnail { get; }

	}

	public class PictureUploadSettings : IPictureUploadSettings
	{
		//private GlobalSettings _globalSettings;

		private int _thumbnailMaxPictureSize;
		private string _picturePath;
		private int _maxPictureSize;
		private bool _generateThumbnail;

		public PictureUploadSettings()
		{

		}

		public PictureUploadSettings(PictureTypeEnum pictureType, IGlobalSettings globalSettings)
		{
			//_globalSettings = new GlobalSettings();

			_thumbnailMaxPictureSize = globalSettings.PictureThumbnailSize;

			switch (pictureType)
			{
				case PictureTypeEnum.ProductPicture:
					_picturePath = HttpContext.Current.Server.MapPath(globalSettings.ShopImagesPath);
					_maxPictureSize = globalSettings.ShopItemImageMaxSize;
					_generateThumbnail = true;
					break;

				case PictureTypeEnum.CategoryPicture:
					_picturePath = HttpContext.Current.Server.MapPath(globalSettings.CategoryImagesPath);
					_maxPictureSize = globalSettings.CategoryImageMaxSize;
					_generateThumbnail = true;
					break;

				case PictureTypeEnum.SliderPicture:
					_picturePath = HttpContext.Current.Server.MapPath(globalSettings.SliderImagesPath);
					_maxPictureSize = globalSettings.SliderImageMaxSize;
					_generateThumbnail = true;
					break;

				case PictureTypeEnum.SectionPicture:
					_picturePath = HttpContext.Current.Server.MapPath(globalSettings.SectionImagesPath);
					_maxPictureSize = globalSettings.SectionImageMaxSize;
					_generateThumbnail = true;
					break;

				default:
					_picturePath = null;
					break;
			}
		}


		public int ThumbnailMaxPictureSize { get { return _thumbnailMaxPictureSize; } }
		public int MaxPictureSize { get { return _maxPictureSize; } }
		public string PicturePath { get { return _picturePath; } }
		public bool GenerateThumbnail { get { return _generateThumbnail; } }

	}

	
}
