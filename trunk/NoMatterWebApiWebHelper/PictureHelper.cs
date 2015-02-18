using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterWebApiWebHelper.OtherHelpers;

namespace NoMatterWebApiWebHelper
{
	public interface IPictureHelper
	{
		string UploadPicture(Image image, IPictureUploadSettings pictureUploadSettings);
		void DeletePicture(string pictureFileName, string picturePath);

	}

	public class PictureHelper : IPictureHelper
	{
		public string UploadPicture(Image image, IPictureUploadSettings pictureUploadSettings)
		{

			if (image == null) return null;

			var filenameGuid = Guid.NewGuid();
			var pictureFileName = filenameGuid + ".jpg";

			//Check if the picture is maybe too large
			if (image.Height > pictureUploadSettings.MaxPictureSize)
			{
				//Resize the image to a smaller size
				var mainPicture = ScaleImage(image, pictureUploadSettings.MaxPictureSize, pictureUploadSettings.MaxPictureSize);
				var mainPicturePath = Path.Combine(pictureUploadSettings.PicturePath, filenameGuid + ".jpg");
				mainPicture.Save(mainPicturePath, System.Drawing.Imaging.ImageFormat.Jpeg);
			}
			else
			{
				//Save image to the images path
				var fullPicturePath = Path.Combine(pictureUploadSettings.PicturePath, pictureFileName);
				image.Save(fullPicturePath);
			}
				
			if (pictureUploadSettings.GenerateThumbnail)
			{
				//Generate a thumbnail image
				var pictureThumbnailSize = pictureUploadSettings.ThumbnailMaxPictureSize;
				var thumb = ScaleImage(image, pictureThumbnailSize, pictureThumbnailSize);
				var thumbPicturePath = Path.Combine(pictureUploadSettings.PicturePath, "thumb-" + pictureFileName);
				thumb.Save(thumbPicturePath, System.Drawing.Imaging.ImageFormat.Jpeg);
			}

			//Return the full picture path
			return pictureFileName;

			
		}

		public void DeletePicture(string pictureFileName, string picturePath)
		{
			string fullPicturePath = picturePath + "\\" + pictureFileName;
			string thumbFullPicturePath = picturePath + "\\" + "thumb-" + pictureFileName;


			if (!string.IsNullOrEmpty(fullPicturePath))
			{
				//Delete the main pic
				if (File.Exists(fullPicturePath))
				{
					File.Delete(fullPicturePath);
				}
			}

			if (!string.IsNullOrEmpty(thumbFullPicturePath))
			{
				//Delete the thumbnail
				if (File.Exists(thumbFullPicturePath))
				{
					File.Delete(thumbFullPicturePath);
				}
			}

		}

		private Image ScaleImage(Image image, int maxWidth, int maxHeight)
		{
			var ratioX = (double)maxWidth / image.Width;
			var ratioY = (double)maxHeight / image.Height;
			var ratio = Math.Min(ratioX, ratioY);

			var newWidth = (int)(image.Width * ratio);
			var newHeight = (int)(image.Height * ratio);

			var newImage = new Bitmap(newWidth, newHeight);
			Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
			return newImage;
		}
	}
}
