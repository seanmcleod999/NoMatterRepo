using System;
using System.Drawing;
using System.Web.Mvc;
using SharedLibrary.Enums;
using SharedLibrary.Logging;
using SharedLibrary.Models;
using SharedLibrary.Services.PictureService;
using SharedLibrary.Services.SliderService;
using SharedLibrary.ViewModels;

namespace PrettyDamnThriftyWeb.Controllers
{
    public class SliderController : Controller
    {
		private ISliderService _sliderService;
		private IPictureService _pictureService;
		private IPictureUploadSettings _sliderPictureUploadSettings;

		public SliderController()
		{
			_sliderService = new SliderService();
			_pictureService = new PictureService();
			_sliderPictureUploadSettings = new PictureUploadSettings(PictureTypeEnum.SliderPicture);
		}


		[Authorize(Roles = "Admin")]
		public ActionResult Index()
		{
			var sliders = _sliderService.GetSliders();

			return View(sliders);
		}

		[Authorize(Roles = "Superuser")]
		public ActionResult SliderAdd()
		{
			var sliderVm = new SliderVm
			{
				Slider = new Slider() { MaxSlides=1, MinSlides=1}
			};

			return View(sliderVm);
		}

		[Authorize(Roles = "Superuser")]
		[HttpPost]
		public ActionResult SliderAdd(SliderVm sliderVm)
		{
			_sliderService.AddSlider(sliderVm.Slider);

			return RedirectToAction("Index");
		}

		[Authorize(Roles = "Admin")]
		public ActionResult SliderEdit(short id)
		{
			var sliderVm = new SliderVm
			{
				Slider = _sliderService.GetSlider(id)
			};

			return View(sliderVm);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public ActionResult SliderEdit(SliderVm sliderVm)
		{
			_sliderService.UpdateSlider(sliderVm.Slider);

			return RedirectToAction("SliderEdit", new { id = sliderVm.Slider.SliderId });
		}

		[Authorize(Roles = "Superuser")]
		public ActionResult SliderDelete(short id)
		{
			_sliderService.DeleteSlider(id);

			return RedirectToAction("GetSlidersForEdit");
		}

		[Authorize(Roles = "Admin")]
		public ActionResult SliderImageAdd(short sliderId)
		{
			var sliderImageVm = new SliderImageVm
				{
					SliderPicture = new SliderPicture {SliderId = sliderId}
				};

			return View(sliderImageVm);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public ActionResult SliderImageAdd(SliderImageVm sliderImageVm)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var pictureName = _pictureService.UploadPicture(Image.FromStream(sliderImageVm.File.InputStream), _sliderPictureUploadSettings);

					var sliderPicture = new SliderPicture
						{
							SliderId = sliderImageVm.SliderPicture.SliderId,
							PicturePath = pictureName,
							Order = sliderImageVm.SliderPicture.Order,
							Url = sliderImageVm.SliderPicture.Url,
							Notes = sliderImageVm.SliderPicture.Notes
						};

					_sliderService.UploadSliderPicture(sliderPicture);

					return RedirectToAction("SliderEdit", new { id = sliderImageVm.SliderPicture.SliderId });
				}
				catch (Exception ex)
				{
					//error msg for failed insert in XML file
					ModelState.AddModelError("", "Error creating record. " + ex.Message);
				}
			}

			return View(sliderImageVm);
		}

		[Authorize(Roles = "Admin")]
		public ActionResult SliderImageEdit(int id)
		{
			var sliderImageVm = new SliderImageVm();

			sliderImageVm.SliderPicture = _sliderService.GetSliderPicture(id);

			return View(sliderImageVm);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public ActionResult SliderImageEdit(SliderImageVm sliderImageVm)
		{
			if (ModelState.IsValid)
			{
				try
				{
					_sliderService.UpdateSliderPicture(sliderImageVm.SliderPicture);

					return RedirectToAction("SliderEdit", new { id = sliderImageVm.SliderPicture.SliderId });
				}
				catch (Exception ex)
				{
					Logger.WriteGeneralError(ex);
					ModelState.AddModelError("", "Error editing record. " + ex.Message);
				}
			}

			return View(sliderImageVm);
		}

		[Authorize(Roles = "Admin")]
		public ActionResult SliderImageDelete(short id, int slideId)
		{
			_sliderService.DeleteSliderPicture(id);
			return RedirectToAction("SliderEdit", new { id = slideId });
		}

		[Authorize(Roles = "Superuser")]
		public ActionResult GetSlidersForEdit()
		{
			var sliders = _sliderService.GetSliders();

			return View(sliders);
		}
    }
}
