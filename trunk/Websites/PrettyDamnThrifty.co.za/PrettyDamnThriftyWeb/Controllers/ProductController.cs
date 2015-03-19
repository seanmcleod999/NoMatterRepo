using System;
using System.Drawing;
using System.IO;
using System.Web.Mvc;
using SharedLibrary.DatabaseModel;
using SharedLibrary.Enums;
using SharedLibrary.Helpers;
using SharedLibrary.Logging;
using SharedLibrary.Models;
using SharedLibrary.Services;
using SharedLibrary.Services.CategoryService;
using SharedLibrary.Services.DiscountService;
using SharedLibrary.Services.PictureService;
using SharedLibrary.Services.ProductService;
using SharedLibrary.Services.ProductService;
using SharedLibrary.ViewModels;

namespace PrettyDamnThriftyWeb.Controllers
{
	public class ProductController : Controller
	{

		private IProductService _productService;
		
		private ICategoryService _categoryService;
		private IPictureService _pictureService;
		private ICurrentUser _currentUser;
		private IGlobalSettings _globalSettings;
		private IPictureUploadSettings _shopItemPictureUploadSettings;

		public ProductController()
		{
			_productService = new ProductService();
			_categoryService = new CategoryService();
			_pictureService = new PictureService();
			_currentUser = new CurrentUser();
			_globalSettings = new GlobalSettings();
			_shopItemPictureUploadSettings = new PictureUploadSettings(PictureTypeEnum.ShopItemPicture);
		}

		public ProductController(ICurrentUser currentUser)
		{
			_productService = new ProductService();
			_currentUser = currentUser;
		}

		[Authorize]
		public ActionResult Index()
		{
			short categoryId = 0;
			string sortColumn = "";
			string selectedDisplayType = "AllExceptSold";

			if (Session["ShopAdminCategoryId"] != null) categoryId = (short)Session["ShopAdminCategoryId"];
			if (Session["SortColumn"] != null) sortColumn = (string)Session["SortColumn"];
			if (Session["ShopAdminSelectedDisplayType"] != null) selectedDisplayType = (string)Session["ShopAdminSelectedDisplayType"];

			var viewShopItemsVm = new ViewShopItemsAdminVm
				{
					CategoryId = categoryId,
					SortColumn = sortColumn,
					SelectedDisplayType = selectedDisplayType,
					Categories = _categoryService.GetCategoriesForDropDown()
				};

			viewShopItemsVm.Categories.Add(new CategoryLinkModel { CategoryId = 0, CategoryName = "All" });

			viewShopItemsVm.DisplayTypes = _productService.GetDisplayTypes();
			viewShopItemsVm.ShopItems = _productService.GetShopItemsForAdmin(categoryId, sortColumn, selectedDisplayType);

			return View(viewShopItemsVm);
		}

		[Authorize]
		[HttpPost]
		public ActionResult Index(short categoryId, string sortColumn, string selectedDisplayType)
		{
			//var getCategoriesResult = _categoryService.GetCategoriesForEditing();

			Session["ShopAdminCategoryId"] = categoryId;
			Session["SortColumn"] = sortColumn;
			Session["ShopAdminSelectedDisplayType"] = selectedDisplayType;

			var viewShopItemsVm = new ViewShopItemsAdminVm
			{
				CategoryId = categoryId,
				SortColumn = sortColumn,
				SelectedDisplayType = selectedDisplayType,
				Categories =  _categoryService.GetCategoriesForDropDown()
			};

			viewShopItemsVm.Categories.Add(new CategoryLinkModel { CategoryId = 0, CategoryName = "All" });

			viewShopItemsVm.DisplayTypes = _productService.GetDisplayTypes();
			viewShopItemsVm.ShopItems = _productService.GetShopItemsForAdmin(categoryId, sortColumn, selectedDisplayType);

			return View(viewShopItemsVm);
		}

		[Authorize]
		public ActionResult Create()
		{

			var shopItemVm = new ShopItemVm {ShopItem = new ShopItem()};

			shopItemVm.ShopItem.ReleaseDate = DateTime.Now;

			shopItemVm.Categories = _categoryService.GetCategoriesForDropDown();

			shopItemVm.FacebookAlbumList = _productService.GetFacebookAlbumList();

			return View(shopItemVm);
		}

		[Authorize]
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Create(ShopItemVm shopItemVm)
		{
			if (ModelState.IsValid)
			{
				try
				{
					//if (shopItemVm.File != null) shopItemVm.ShopItem.Picture1Upload = new UploadedPictureFileBase(shopItemVm.File.InputStream, shopItemVm.File.FileName);
					//if (shopItemVm.File2 != null) shopItemVm.ShopItem.Picture2Upload = new UploadedPictureFileBase(shopItemVm.File2.InputStream, shopItemVm.File2.FileName);
					//if (shopItemVm.File3 != null) shopItemVm.ShopItem.Picture3Upload = new UploadedPictureFileBase(shopItemVm.File3.InputStream, shopItemVm.File3.FileName);
					//if (shopItemVm.File4 != null) shopItemVm.ShopItem.Picture4Upload = new UploadedPictureFileBase(shopItemVm.File4.InputStream, shopItemVm.File4.FileName);
					//if (shopItemVm.File5 != null) shopItemVm.ShopItem.Picture5Upload = new UploadedPictureFileBase(shopItemVm.File5.InputStream, shopItemVm.File5.FileName);
					//if (shopItemVm.FileSocialMedia != null) shopItemVm.ShopItem.PictureSocialMediaUpload = new UploadedPictureFileBase(shopItemVm.FileSocialMedia.InputStream, shopItemVm.FileSocialMedia.FileName);

					if (shopItemVm.File != null) shopItemVm.ShopItem.Picture1 = _pictureService.UploadPicture(Image.FromStream(shopItemVm.File.InputStream), _shopItemPictureUploadSettings);
					if (shopItemVm.File2 != null) shopItemVm.ShopItem.Picture2 = _pictureService.UploadPicture(Image.FromStream(shopItemVm.File2.InputStream), _shopItemPictureUploadSettings);
					if (shopItemVm.File3 != null) shopItemVm.ShopItem.Picture3 = _pictureService.UploadPicture(Image.FromStream(shopItemVm.File3.InputStream), _shopItemPictureUploadSettings);
					if (shopItemVm.File4 != null) shopItemVm.ShopItem.Picture4 = _pictureService.UploadPicture(Image.FromStream(shopItemVm.File4.InputStream), _shopItemPictureUploadSettings);
					if (shopItemVm.File5 != null) shopItemVm.ShopItem.Picture5 = _pictureService.UploadPicture(Image.FromStream(shopItemVm.File5.InputStream), _shopItemPictureUploadSettings);
					if (shopItemVm.FileSocialMedia != null) shopItemVm.ShopItem.PictureSocialMedia = _pictureService.UploadPicture(Image.FromStream(shopItemVm.FileSocialMedia.InputStream), _shopItemPictureUploadSettings);

					_productService.CreateProduct(shopItemVm.ShopItem);
					
					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					//error msg for failed insert in XML file
					ModelState.AddModelError("", "Error creating record. " + ex.Message);
				}
			}

			shopItemVm.Categories = _categoryService.GetCategoriesForDropDown();
			shopItemVm.FacebookAlbumList = _productService.GetFacebookAlbumList();

			return View(shopItemVm);
		}


		[Authorize]
		public ActionResult Edit(int id)
		{

			var shopItemVm = new ShopItemVm
				{
					ShopItem = _productService.GetShopItemForAdmin(id)
				};

			shopItemVm.Categories = _categoryService.GetCategoriesForDropDown();
			shopItemVm.FacebookAlbumList = _productService.GetFacebookAlbumList();

			return View(shopItemVm);
		}

		[Authorize]
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Edit(ShopItemVm shopItemVm)
		{
			if (ModelState.IsValid)
			{
				try
				{
					////Handle deletions - Should not do this.. because of caching
					//if (!string.IsNullOrEmpty(shopItemVm.Picture1Deleted)) _pictureService.DeletePicture(shopItemVm.Picture1Deleted, _shopItemPictureUploadSettings.PicturePath);
					//if (!string.IsNullOrEmpty(shopItemVm.Picture2Deleted)) _pictureService.DeletePicture(shopItemVm.Picture2Deleted, _shopItemPictureUploadSettings.PicturePath);
					//if (!string.IsNullOrEmpty(shopItemVm.Picture3Deleted)) _pictureService.DeletePicture(shopItemVm.Picture3Deleted, _shopItemPictureUploadSettings.PicturePath);
					//if (!string.IsNullOrEmpty(shopItemVm.Picture4Deleted)) _pictureService.DeletePicture(shopItemVm.Picture4Deleted, _shopItemPictureUploadSettings.PicturePath);
					//if (!string.IsNullOrEmpty(shopItemVm.Picture5Deleted)) _pictureService.DeletePicture(shopItemVm.Picture5Deleted, _shopItemPictureUploadSettings.PicturePath);
					//if (!string.IsNullOrEmpty(shopItemVm.Picture1Deleted)) _pictureService.DeletePicture(shopItemVm.PictureSocialMediaDeleted, _shopItemPictureUploadSettings.PicturePath);

					//Handle new uploads
					if (shopItemVm.File != null) shopItemVm.ShopItem.Picture1 = _pictureService.UploadPicture(Image.FromStream(shopItemVm.File.InputStream), _shopItemPictureUploadSettings);
					if (shopItemVm.File2 != null) shopItemVm.ShopItem.Picture2 = _pictureService.UploadPicture(Image.FromStream(shopItemVm.File2.InputStream), _shopItemPictureUploadSettings);
					if (shopItemVm.File3 != null) shopItemVm.ShopItem.Picture3 = _pictureService.UploadPicture(Image.FromStream(shopItemVm.File3.InputStream), _shopItemPictureUploadSettings);
					if (shopItemVm.File4 != null) shopItemVm.ShopItem.Picture4 = _pictureService.UploadPicture(Image.FromStream(shopItemVm.File4.InputStream), _shopItemPictureUploadSettings);
					if (shopItemVm.File5 != null) shopItemVm.ShopItem.Picture5 = _pictureService.UploadPicture(Image.FromStream(shopItemVm.File5.InputStream), _shopItemPictureUploadSettings);
					if (shopItemVm.FileSocialMedia != null) shopItemVm.ShopItem.PictureSocialMedia = _pictureService.UploadPicture(Image.FromStream(shopItemVm.FileSocialMedia.InputStream), _shopItemPictureUploadSettings);

					_productService.UpdateProduct(shopItemVm.ShopItem);

					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					Logger.WriteGeneralError(ex);
					ModelState.AddModelError("", "Error editing record. " + ex.Message);
				}
			}

			//Get the values for the dropdows again so that the screen can render again
			shopItemVm.Categories = _categoryService.GetCategoriesForDropDown();
			shopItemVm.FacebookAlbumList = _productService.GetFacebookAlbumList();

			return View(shopItemVm);
		}

		[Authorize]
		public ActionResult Delete(int id)
		{

			var shopItemVm = new ShopItemVm
			{
				ShopItem = _productService.GetShopItemForAdmin(id)
			};

			return View(shopItemVm);
		}

		[Authorize]
		[HttpPost]
		public ActionResult Delete(ShopItemVm shopItemVm)
		{
			try
			{
				_productService.DeleteProduct(shopItemVm.ShopItem.ShopItemId);

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				//error msg for failed delete in XML file
				ViewBag.ErrorMsg = "Error deleting record. " + ex.Message;
				return View(shopItemVm);
			}
		}

		[Authorize]
		public ActionResult PostToFacebook(int id)
		{

			try
			{
				var shopItem = _productService.GetShopItemForAdmin(id);

				string facebookMessageTemplate = _globalSettings.FacebookItemPostMessage;

				var facebookPostText = String.Format(facebookMessageTemplate,
					shopItem.Description,
					shopItem.Price,
					String.IsNullOrEmpty(shopItem.Size) ? "" : "Size: " + shopItem.Size,
					shopItem.ItemShortUrl);

				var postToFacebookVm = new PostToFacebookVm
				{
					FacebookPostText = facebookPostText,
					ShopItem = shopItem
				};

				return View(postToFacebookVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}

		}
		

		[Authorize]
		[HttpPost]
		public ActionResult PostToFacebook(int shopItemId, string facebookPostText, string facebookAlbumId, string picturePath)
		{
			try
			{
				//TODO: move this path to the service?
				var path = Path.Combine(Server.MapPath(_globalSettings.ShopImagesPath), picturePath);

				//Try post to facebook
				_productService.PostItemToFacebook(shopItemId, facebookPostText, facebookAlbumId, path);

				return RedirectToAction("Edit", new { id = shopItemId });
						
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				ViewBag.ShopItemId = shopItemId;

				return View("FacebookPostFailed");
			}

		}

		[Authorize]
		public ActionResult PostToTwitter(int id)
		{

			try
			{
				var shopItem = _productService.GetShopItemForAdmin(id);

				string twitterMessageTemplate = _globalSettings.TwitterItemPostMessage;

				var tweetText = String.Format(twitterMessageTemplate, shopItem.Description, shopItem.Price, shopItem.ItemShortUrl);

				var postToTwitterVm = new PostToTwitterVm
				{
					TweetText = tweetText,
					PicturePath = shopItem.PictureSocialMedia ?? shopItem.Picture1,
					ShopItem = shopItem
				};

				return View(postToTwitterVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}

		}
		

		[Authorize]
		[HttpPost]
		public ActionResult PostToTwitter(int shopItemId, string tweetText, string picturePath)
		{
			try
			{
			
				var fullPicturePath = Path.Combine(HttpContext.Server.MapPath(_globalSettings.ShopImagesPath), picturePath);

				//Try post to twitter
				_productService.PostItemToTwitter(shopItemId, tweetText, fullPicturePath);

				return RedirectToAction("Edit", new { id = shopItemId });			
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				ViewBag.ShopItemId = shopItemId;
				return View("TwitterPostFailed");
			}
		}


		[Authorize]
		public String GenerateShortUrl(int id)
		{
			var shortUrl = SiteHelper.MakeGoogleShortUrl(_globalSettings.ShopItemPath + id);

			return shortUrl;
		}

	}
}
