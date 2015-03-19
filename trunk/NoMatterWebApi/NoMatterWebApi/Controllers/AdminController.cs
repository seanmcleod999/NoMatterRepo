using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Security;
using NoMatterWebApi.Controllers.V1;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApiModels.Models;
using NoMatterWebApiModels.ViewModels;
using NoMatterWebApiWebHelper.OtherHelpers;
using NoMatterWebApiWebHelper.WebApiHelpers;

namespace NoMatterWebApi.Controllers.v1
{
	[Authorize]
	public class AdminController : Controller
	{

		private ClientController _clientController = new ClientController();
		private SectionController _sectionController = new SectionController();
		private CategoryController _categoryController = new CategoryController();
		private ProductController _productController = new ProductController();
		private ImageController _imageController = new ImageController();

		public async Task<ActionResult> Index()
		{
			//Get the logged in user client if
			var clientId = ((CustomPrincipal)HttpContext.User).ClientId;

			ViewBag.UserName = User.Identity.Name;

			if (string.IsNullOrEmpty(clientId))
			{
				//System user not allocated to a client
				ViewBag.ClientName = "System Administrator";
			}
			else
			{
				var getClientResult = await _clientController.GetClientAsync(clientId);
				var clientResult = getClientResult as OkNegotiatedContentResult<Client>;
		
				ViewBag.ClientName = clientResult.Content.ClientName;
				ViewBag.Logo = clientResult.Content.Logo;
			}

			return View();
		}

		public async Task<ActionResult> Clients()
		{

			var clientControlleResult = await _clientController.GetClientsAsync();
			var clientsResult = clientControlleResult as OkNegotiatedContentResult<List<Client>>;

			var viewClientsVm = new ViewClientsVm
			{
				Clients = clientsResult.Content
			};

			return View(viewClientsVm);
		}

		public ActionResult ClientAdd()
		{
			var addClientVm = new AddEditClientVm
			{
				Client = new Client()
				{
					Enabled = true
				}
			};

			return View(addClientVm);
		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> ClientAdd(AddEditClientVm addEditClientVm)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			var imageController = new ImageController();

			_clientController.User = new ClaimsPrincipal(WebApiGeneralHelper.GenerateClaimsIdentity(token));

			//if (addEditSectionVm.Picture != null)
			//{
			//	var imageBase64String = WebApiGeneralHelper.ConvertPictureFileToBase64String(addEditSectionVm.Picture);
			//	var uploadImageResult = await imageController.UploadImageAsync(addEditSectionVm.Section.ClientId, imageBase64String);
			//	var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

			//	addEditSectionVm.Section.Picture = pictureResult.Content;
			//}

			await _clientController.AddClientAsync(addEditClientVm.Client);

			return RedirectToAction("Clients");
		}

		public async Task<ActionResult> Products(string clientId)
		{

			//string productStatus = "Active";
			//string categoryId = null;

			//var productController = new ProductController();

			//var products = await productController.GetClientProductsAsync(clientId, productStatus, categoryId);

			//var viewClientProducts = new ViewClientProductsVm
			//{
			//	ClientProducts = products
			//};

			//return View(viewClientProducts);

			return View();
		}


		public async Task<ActionResult> Sections(string clientId = null)
		{
			if (string.IsNullOrEmpty(clientId)) clientId = ((CustomPrincipal)HttpContext.User).ClientId;

			var getClientResult = await _clientController.GetClientAsync(clientId);
			var clientResult = getClientResult as OkNegotiatedContentResult<Client>;

			var getClientSectionsResult = await _clientController.GetClientSections(clientId, true, true);
			var sectionsResult = getClientSectionsResult as OkNegotiatedContentResult<List<Section>>;

			var clientSectionsVm = new ClientSectionsVm
			{
				Client = clientResult.Content,
				Sections = sectionsResult.Content
			};

			return View(clientSectionsVm);

		}

		public ActionResult SectionAdd(string clientId)
		{
			var addSectionVm = new AddEditSectionVm
			{
				Section = new Section()
				{
					ClientId = clientId,
					Hidden = false
				}
			};

			return View(addSectionVm);
		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> SectionAdd(AddEditSectionVm addEditSectionVm)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			var imageController = new ImageController();

			_clientController.User = new ClaimsPrincipal(WebApiGeneralHelper.GenerateClaimsIdentity(token));
			
			if (addEditSectionVm.Picture != null)
			{
				var imageBase64String = WebApiGeneralHelper.ConvertPictureFileToBase64String(addEditSectionVm.Picture);
				var uploadImageResult = await imageController.UploadImageAsync(addEditSectionVm.Section.ClientId, imageBase64String);
				var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

				addEditSectionVm.Section.Picture = pictureResult.Content;
			}

			await _clientController.AddClientSection(addEditSectionVm.Section.ClientId, addEditSectionVm.Section);

			return RedirectToAction("Sections", new { clientId = addEditSectionVm.Section.ClientId });
		}

		public async Task<ActionResult> SectionEdit(string clientId, string sectionId)
		{
			var getSectionResult = await _sectionController.GetSectionAsync(clientId, sectionId);
			var sectionResult = getSectionResult as OkNegotiatedContentResult<Section>;

			var addSectionVm = new AddEditSectionVm
			{
				Section = sectionResult.Content
			};

			return View(addSectionVm);

		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> SectionEdit(AddEditSectionVm addEditSectionVm)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			_sectionController.User = new ClaimsPrincipal(WebApiGeneralHelper.GenerateClaimsIdentity(token));
			

			if (addEditSectionVm.Picture != null)
			{
				var imageBase64String = WebApiGeneralHelper.ConvertPictureFileToBase64String(addEditSectionVm.Picture);
				var uploadImageResult = await _imageController.UploadImageAsync(addEditSectionVm.Section.ClientId, imageBase64String);
				var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

				addEditSectionVm.Section.Picture = pictureResult.Content;
			}

			await _sectionController.UpdateSectionAsync(addEditSectionVm.Section.ClientId, addEditSectionVm.Section.SectionId, addEditSectionVm.Section);

			return RedirectToAction("Sections", new { clientId = addEditSectionVm.Section.ClientId});

		}

		public async Task<ActionResult> SectionDelete(string clientId, string sectionId)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			_sectionController.User = new ClaimsPrincipal(WebApiGeneralHelper.GenerateClaimsIdentity(token));

			await _sectionController.DeleteSectionAsync(clientId, sectionId);

			return RedirectToAction("Sections", new {clientId = clientId});
		}

		public async Task<ActionResult> SectionCategories(string clientId, string sectionId)
		{
			var getSectionResult = await _sectionController.GetSectionAsync(clientId, sectionId);
			var sectionResult = getSectionResult as OkNegotiatedContentResult<Section>;

			var getClientResult = await _clientController.GetClientAsync(clientId);
			var clientResult = getClientResult as OkNegotiatedContentResult<Client>;

			var getSectionCategoriesResult = await _sectionController.GetSectionCategoriesAsync(clientId, sectionId, true, true);
			var categoriesResult = getSectionCategoriesResult as OkNegotiatedContentResult<List<Category>>;

			var sectionCategoriesVm = new SectionCategoriesVm
			{
				Client = clientResult.Content,
				Section = sectionResult.Content,
				Categories = categoriesResult.Content
			};

			return View("Categories", sectionCategoriesVm);

		}

		public async Task<ActionResult> CategoryAdd(string clientId, string sectionId)
		{				
			var getSectionResult = await _sectionController.GetSectionAsync(clientId, sectionId);
			var sectionResult = getSectionResult as OkNegotiatedContentResult<Section>;

			var addCategoryVm = new AddEditCategoryVm
			{
				Section = sectionResult.Content,
				Category = new Category()
				{
					SectionId = sectionId
				}
			};

			return View("CategoryAdd", addCategoryVm);
		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> CategoryAdd(AddEditCategoryVm addCategoryVm)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			_sectionController.User = new ClaimsPrincipal(WebApiGeneralHelper.GenerateClaimsIdentity(token));

			if (addCategoryVm.Picture != null)
			{
				var imageBase64String = WebApiGeneralHelper.ConvertPictureFileToBase64String(addCategoryVm.Picture);
				var uploadImageResult = await _imageController.UploadImageAsync(addCategoryVm.Section.ClientId, imageBase64String);
				var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

				addCategoryVm.Category.Picture = pictureResult.Content;
			}

			await _sectionController.AddSectionCategory(addCategoryVm.Section.ClientId, addCategoryVm.Category.SectionId, addCategoryVm.Category);

			return RedirectToAction("SectionCategories", new { clientId = addCategoryVm.Section.ClientId, sectionId = addCategoryVm.Category.SectionId });

		}

		public async Task<ActionResult> CategoryEdit(string clientId, string categoryId)
		{
			var getCategoryResult = await _categoryController.GetCategoryAsync(clientId, categoryId);
			var categoryResult = getCategoryResult as OkNegotiatedContentResult<Category>;

			var getSectionResult = await _sectionController.GetSectionAsync(clientId, categoryResult.Content.SectionId);
			var sectionResult = getSectionResult as OkNegotiatedContentResult<Section>;

			var addCategoryVm = new AddEditCategoryVm
			{
				Section = sectionResult.Content,
				Category = categoryResult.Content
			};

			return View("CategoryEdit", addCategoryVm);

		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> CategoryEdit(AddEditCategoryVm addCategoryVm)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			_categoryController.User = new ClaimsPrincipal(WebApiGeneralHelper.GenerateClaimsIdentity(token));

			if (addCategoryVm.Picture != null)
			{
				var imageBase64String = WebApiGeneralHelper.ConvertPictureFileToBase64String(addCategoryVm.Picture);
				var uploadImageResult = await _imageController.UploadImageAsync(addCategoryVm.Section.ClientId, imageBase64String);
				var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

				addCategoryVm.Category.Picture = pictureResult.Content;
			}

			await _categoryController.UpdateCategoryAsync(addCategoryVm.Section.ClientId, addCategoryVm.Category.CategoryId, addCategoryVm.Category);

			return RedirectToAction("SectionCategories", new { clientId = addCategoryVm.Section.ClientId, sectionId = addCategoryVm.Category.SectionId });

		}

		public async Task<ActionResult> CategoryDelete(string clientId, string categoryId, string sectionId)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			_categoryController.User = new ClaimsPrincipal(WebApiGeneralHelper.GenerateClaimsIdentity(token));

			await _categoryController.DeleteCategoryAsync(categoryId);

			return RedirectToAction("SectionCategories", new { clientId = clientId, sectionId = sectionId });
		}

		public async Task<ActionResult> CategoryProducts(string clientId, string categoryId)
		{

			var getCategoryResult = await _categoryController.GetCategoryAsync(clientId, categoryId);
			var categoryResult = getCategoryResult as OkNegotiatedContentResult<Category>;

			var getSectionResult = await _sectionController.GetSectionAsync(clientId, categoryResult.Content.SectionId);
			var sectionResult = getSectionResult as OkNegotiatedContentResult<Section>;

			var getClientResult = await _clientController.GetClientAsync(clientId);
			var clientResult = getClientResult as OkNegotiatedContentResult<Client>;

			var getCategoryProductsResult = await _categoryController.GetCategoryProducts(clientId, categoryId);
			var categoryProductsResult = getCategoryProductsResult as OkNegotiatedContentResult<List<Product>>;

			var categoryProductsVm = new CategoryProductsVm
			{
				Client = clientResult.Content,
				Section = sectionResult.Content,
				Category = categoryResult.Content,
				CategoryProducts = categoryProductsResult.Content
			};

			return View(categoryProductsVm);

		}

		public async Task<ActionResult> ProductAdd(string clientId, string categoryId)
		{
			var getCategoryResult = await _categoryController.GetCategoryAsync(clientId, categoryId);
			var categoryResult = getCategoryResult as OkNegotiatedContentResult<Category>;

			var getSectionResult = await _sectionController.GetSectionAsync(clientId, categoryResult.Content.SectionId);
			var sectionResult = getSectionResult as OkNegotiatedContentResult<Section>;

			var addEditProductVm = new AddEditProductVm
			{
				Section = sectionResult.Content,
				Category = categoryResult.Content,
				Product = new Product()
				{
					ReleaseDate = DateTime.Now
				}
			};

			return View(addEditProductVm);

		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> ProductAdd(AddEditProductVm addEditProductVm)
		{
			try
			{
				var token = ((CustomPrincipal)HttpContext.User).Token;

				_categoryController.User = new ClaimsPrincipal(WebApiGeneralHelper.GenerateClaimsIdentity(token));

				if (addEditProductVm.Picture1 != null)
				{
					var imageBase64String = WebApiGeneralHelper.ConvertPictureFileToBase64String(addEditProductVm.Picture1);
					var uploadImageResult = await _imageController.UploadImageAsync(addEditProductVm.Section.ClientId, imageBase64String);
					var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

					addEditProductVm.Product.Picture1 = pictureResult.Content;
				}

				if (addEditProductVm.Picture2 != null)
				{
					var imageBase64String = WebApiGeneralHelper.ConvertPictureFileToBase64String(addEditProductVm.Picture2);
					var uploadImageResult = await _imageController.UploadImageAsync(addEditProductVm.Section.ClientId, imageBase64String);
					var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

					addEditProductVm.Product.Picture2 = pictureResult.Content;
				}

				if (addEditProductVm.Picture3 != null)
				{
					var imageBase64String = WebApiGeneralHelper.ConvertPictureFileToBase64String(addEditProductVm.Picture3);
					var uploadImageResult = await _imageController.UploadImageAsync(addEditProductVm.Section.ClientId, imageBase64String);
					var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

					addEditProductVm.Product.Picture3 = pictureResult.Content;
				}

				if (addEditProductVm.Picture4 != null)
				{
					var imageBase64String = WebApiGeneralHelper.ConvertPictureFileToBase64String(addEditProductVm.Picture4);
					var uploadImageResult = await _imageController.UploadImageAsync(addEditProductVm.Section.ClientId, imageBase64String);
					var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

					addEditProductVm.Product.Picture4 = pictureResult.Content;
				}

				if (addEditProductVm.Picture5 != null)
				{
					var imageBase64String = WebApiGeneralHelper.ConvertPictureFileToBase64String(addEditProductVm.Picture5);
					var uploadImageResult = await _imageController.UploadImageAsync(addEditProductVm.Section.ClientId, imageBase64String);
					var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

					addEditProductVm.Product.Picture5 = pictureResult.Content;
				}

				if (addEditProductVm.PictureOther != null)
				{
					var imageBase64String = WebApiGeneralHelper.ConvertPictureFileToBase64String(addEditProductVm.PictureOther);
					var uploadImageResult = await _imageController.UploadImageAsync(addEditProductVm.Section.ClientId, imageBase64String);
					var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

					addEditProductVm.Product.PictureOther = pictureResult.Content;
				}

				var addCategoryProductResult = await _categoryController.AddCategoryProductAsync(addEditProductVm.Section.ClientId, addEditProductVm.Category.CategoryId, addEditProductVm.Product);
				var productResult = addCategoryProductResult as OkNegotiatedContentResult<Product>;

				return RedirectToAction("CategoryProducts", new
				{
					clientId = addEditProductVm.Section.ClientId,
					categoryId = addEditProductVm.Category.CategoryId
				});
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		public async Task<ActionResult> ProductEdit(string clientId, string productId)
		{
			try
			{
				var getProductResult = await _productController.GetProductAsync(clientId, productId);
				var productResult = getProductResult as OkNegotiatedContentResult<Product>;

				var getCategoryResult = await _categoryController.GetCategoryAsync(clientId, productResult.Content.CategoryId);
				var categoryResult = getCategoryResult as OkNegotiatedContentResult<Category>;

				var getSectionResult = await _sectionController.GetSectionAsync(clientId, categoryResult.Content.SectionId);
				var sectionResult = getSectionResult as OkNegotiatedContentResult<Section>;

				var addEditProductVm = new AddEditProductVm
				{
					Section = sectionResult.Content,
					Category = categoryResult.Content,
					Product = productResult.Content,
				};

				return View(addEditProductVm);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}

		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> ProductEdit(AddEditProductVm addEditProductVm)
		{
			try
			{
				var token = ((CustomPrincipal)HttpContext.User).Token;

				_productController.User = new ClaimsPrincipal(WebApiGeneralHelper.GenerateClaimsIdentity(token));
				
				if (addEditProductVm.Picture1 != null)
				{
					var imageBase64String = WebApiGeneralHelper.ConvertPictureFileToBase64String(addEditProductVm.Picture1);
					var uploadImageResult = await _imageController.UploadImageAsync(addEditProductVm.Section.ClientId, imageBase64String);
					var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

					addEditProductVm.Product.Picture1 = pictureResult.Content;
				}

				if (addEditProductVm.Picture2 != null)
				{
					var imageBase64String = WebApiGeneralHelper.ConvertPictureFileToBase64String(addEditProductVm.Picture2);
					var uploadImageResult = await _imageController.UploadImageAsync(addEditProductVm.Section.ClientId, imageBase64String);
					var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

					addEditProductVm.Product.Picture2 = pictureResult.Content;
				}

				if (addEditProductVm.Picture3 != null)
				{
					var imageBase64String = WebApiGeneralHelper.ConvertPictureFileToBase64String(addEditProductVm.Picture3);
					var uploadImageResult = await _imageController.UploadImageAsync(addEditProductVm.Section.ClientId, imageBase64String);
					var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

					addEditProductVm.Product.Picture3 = pictureResult.Content;
				}

				if (addEditProductVm.Picture4 != null)
				{
					var imageBase64String = WebApiGeneralHelper.ConvertPictureFileToBase64String(addEditProductVm.Picture4);
					var uploadImageResult = await _imageController.UploadImageAsync(addEditProductVm.Section.ClientId, imageBase64String);
					var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

					addEditProductVm.Product.Picture4 = pictureResult.Content;
				}

				if (addEditProductVm.Picture5 != null)
				{
					var imageBase64String = WebApiGeneralHelper.ConvertPictureFileToBase64String(addEditProductVm.Picture5);
					var uploadImageResult = await _imageController.UploadImageAsync(addEditProductVm.Section.ClientId, imageBase64String);
					var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

					addEditProductVm.Product.Picture5 = pictureResult.Content;
				}

				if (addEditProductVm.PictureOther != null)
				{
					var imageBase64String = WebApiGeneralHelper.ConvertPictureFileToBase64String(addEditProductVm.PictureOther);
					var uploadImageResult = await _imageController.UploadImageAsync(addEditProductVm.Section.ClientId, imageBase64String);
					var pictureResult = uploadImageResult as CreatedNegotiatedContentResult<string>;

					addEditProductVm.Product.PictureOther = pictureResult.Content;
				}

				var updateCategoryProductResult = await _productController.UpdateProductAsync(addEditProductVm.Section.ClientId, addEditProductVm.Product.ProductId, addEditProductVm.Product);
				var productResult = updateCategoryProductResult as OkNegotiatedContentResult<Product>;

				return RedirectToAction("CategoryProducts", "Admin", new
					{
						clientId = addEditProductVm.Section.ClientId,
						categoryId = addEditProductVm.Product.CategoryId
					});

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		public async Task<ActionResult> DeleteProduct(string clientId, string productId, string categoryId)
		{
			try
			{
				var token = ((CustomPrincipal)HttpContext.User).Token;

				_productController.User = new ClaimsPrincipal(WebApiGeneralHelper.GenerateClaimsIdentity(token));
				
				await _productController.DeleteProductAsync(clientId, productId);

				return RedirectToAction("CategoryProducts", "Admin", new { clientId = clientId, categoryId = categoryId });
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}

		}

		public async Task<ActionResult> ViewProduct(string clientId, string productId)
		{
			try
			{
				var getProductResult = await _productController.GetProductAsync(clientId, productId);
				var productResult = getProductResult as OkNegotiatedContentResult<Product>;

				var viewProductVm = new ViewProductVm
					{
						ClientId = clientId,
						Product = productResult.Content
					};

				return View(viewProductVm);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}

		}

		public async Task<ActionResult> ClientPages(string clientId)
		{
			try
			{
				var getClientResult = await _clientController.GetClientAsync(clientId);
				var clientResult = getClientResult as OkNegotiatedContentResult<Client>;

				var clientControllerResult = await _clientController.GetClientPagesAsync(clientId);
				var clientPagesResult = clientControllerResult as OkNegotiatedContentResult<List<ClientPage>>;

				var clientPagesVm = new ClientPagesVm
					{
						Client = clientResult.Content,
						ClientPages = clientPagesResult.Content
					};

				return View(clientPagesVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		public async Task<ActionResult> ClientPageAdd(string clientId)
		{
			try
			{
				var getClientResult = await _clientController.GetClientAsync(clientId);
				var clientResult = getClientResult as OkNegotiatedContentResult<Client>;

				var clientPageVm = new ClientPageVm
				{
					Client = clientResult.Content,
				};

				return View(clientPageVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> ClientPageAdd(ClientPageVm clientPageVm)
		{

			var token = ((CustomPrincipal)HttpContext.User).Token;

			_clientController.User = new ClaimsPrincipal(WebApiGeneralHelper.GenerateClaimsIdentity(token));

			var result = await _clientController.AddClientPageAsync(clientPageVm.Client.ClientId, clientPageVm.ClientPage);

			return RedirectToAction("ClientPages", "Admin", new { clientId = clientPageVm.Client.ClientId });
		}

		public async Task<ActionResult> ClientPageEdit(string clientId, string pageName)
		{
			try
			{
				var getClientResult = await _clientController.GetClientAsync(clientId);
				var clientResult = getClientResult as OkNegotiatedContentResult<Client>;

				var clientControllerResult = await _clientController.GetClientPageAsync(clientId, pageName);
				var clientPageResult = clientControllerResult as OkNegotiatedContentResult<ClientPage>;

				var clientPageVm = new ClientPageVm
				{
					Client = clientResult.Content,
					ClientPage = clientPageResult.Content
				};

				return View(clientPageVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> ClientPageEdit(ClientPageVm clientPageVm)
		{

			var token = ((CustomPrincipal)HttpContext.User).Token;

			_clientController.User = new ClaimsPrincipal(WebApiGeneralHelper.GenerateClaimsIdentity(token));

			await _clientController.UpdateClientPageAsync(clientPageVm.Client.ClientId, clientPageVm.ClientPage.PageName, clientPageVm.ClientPage);

			return RedirectToAction("ClientPages", "Admin", new { clientId = clientPageVm .Client.ClientId});
		}

		public async Task<ActionResult> ClientPageDelete(string clientId, string pageName)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			_categoryController.User = new ClaimsPrincipal(WebApiGeneralHelper.GenerateClaimsIdentity(token));

			await _clientController.DeleteClientPageAsync(clientId, pageName);

			return RedirectToAction("ClientPages", new { clientId = clientId});
		}

		public async Task<ActionResult> ClientSettings(string clientId)
		{
			try
			{
				var getClientResult = await _clientController.GetClientAsync(clientId);
				var clientResult = getClientResult as OkNegotiatedContentResult<Client>;

				var getClientSettingsResult = await _clientController.GetClientSettingsAsync(clientId);
				var clientSettingsResult = getClientSettingsResult as OkNegotiatedContentResult<List<ClientSetting>>;

				var clientSettingsVm = new ClientSettingsVm
				{
					Client = clientResult.Content,
					ClientSettings = clientSettingsResult.Content
				};

				return View(clientSettingsVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		public async Task<ActionResult> ClientSettingAdd(string clientId)
		{
			try
			{
				var getClientResult = await _clientController.GetClientAsync(clientId);
				var clientResult = getClientResult as OkNegotiatedContentResult<Client>;

				var clientSettingVm = new ClientSettingVm
				{
					Client = clientResult.Content,
				};

				return View(clientSettingVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> ClientSettingAdd(ClientSettingVm clientSettingVm)
		{

			var token = ((CustomPrincipal)HttpContext.User).Token;

			_clientController.User = new ClaimsPrincipal(WebApiGeneralHelper.GenerateClaimsIdentity(token));

			var result = await _clientController.AddClientSettingAsync(clientSettingVm.Client.ClientId, clientSettingVm.ClientSetting);

			return RedirectToAction("ClientSettings", "Admin", new { clientId = clientSettingVm.Client.ClientId });
		}

		public async Task<ActionResult> ClientSettingEdit(string clientId, short settingId)
		{
			try
			{
				var getClientResult = await _clientController.GetClientAsync(clientId);
				var clientResult = getClientResult as OkNegotiatedContentResult<Client>;

				var getClientSettingResult = await _clientController.GetClientSettingAsync(clientId, settingId);
				var clientSettingResult = getClientSettingResult as OkNegotiatedContentResult<ClientSetting>;

				var clientPageVm = new ClientSettingVm
				{
					Client = clientResult.Content,
					ClientSetting = clientSettingResult.Content
				};

				return View(clientPageVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> ClientSettingEdit(ClientSettingVm clientSettingVm)
		{

			var token = ((CustomPrincipal)HttpContext.User).Token;

			_clientController.User = new ClaimsPrincipal(WebApiGeneralHelper.GenerateClaimsIdentity(token));

			await _clientController.UpdateClientSettingAsync(clientSettingVm.Client.ClientId, clientSettingVm.ClientSetting.SettingId, clientSettingVm.ClientSetting);

			return RedirectToAction("ClientSettings", "Admin", new { clientId = clientSettingVm.Client.ClientId });
		}

		public async Task<ActionResult> ClientSettingDelete(string clientId, short settingId)
		{
			var token = ((CustomPrincipal)HttpContext.User).Token;

			_categoryController.User = new ClaimsPrincipal(WebApiGeneralHelper.GenerateClaimsIdentity(token));

			await _clientController.DeleteClientSettingAsync(clientId, settingId);

			return RedirectToAction("ClientSettings", new { clientId = clientId });
		}

	}
}