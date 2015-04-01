using System;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using CustomAuthLib;
using NoMatterDatabaseModel;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Extensions;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApiModels.ViewModels;
using Category = NoMatterWebApiModels.Models.Category;
using ClientDeliveryOption = NoMatterWebApiModels.Models.ClientDeliveryOption;
using Product = NoMatterWebApiModels.Models.Product;
using Section = NoMatterWebApiModels.Models.Section;

namespace NoMatterWebApi.Controllers.v1
{
	[Authorize]
	public class AdminController : Controller
	{
		private IClientRepository _clientRepository;
		private ISectionRepository _sectionRepository;
		private ICategoryRepository _categoryRepository;
		private IProductRepository _productRepository;
		private IGlobalRepository _globalRepository;
		private IGeneralHelper _generalHelper;

		public AdminController()
		{
			var databaseEntity = new DatabaseEntities();

			_clientRepository = new ClientRepository(databaseEntity);
			_sectionRepository = new SectionRepository(databaseEntity);
			_categoryRepository = new CategoryRepository(databaseEntity);
			_productRepository = new ProductRepository(databaseEntity);
			_globalRepository = new GlobalRepository(databaseEntity);

			_generalHelper = new GeneralHelper();
		}

		public AdminController(IClientRepository clientRepository, ISectionRepository sectionRepository, IProductRepository productRepository, IGlobalRepository globalRepository)
		{
			_clientRepository = clientRepository;
			_sectionRepository = sectionRepository;
			_productRepository = productRepository;
			_globalRepository = globalRepository;
		}

		public async Task<ActionResult> Index()
		{
			//Get the logged in user client if
			var claimsPrincipal = (ClaimsPrincipal)User;
			var authUserClientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

			ViewBag.UserName = User.Identity.Name;

			if (string.IsNullOrEmpty(authUserClientId))
			{
				//System user not allocated to a client
				ViewBag.ClientName = "System Administrator";
			}
			else
			{
				var clientDb = await _clientRepository.GetClientAsync(new Guid(authUserClientId));

				var client = clientDb.ToDomainClient();

				ViewBag.ClientName = client.ClientName;
				ViewBag.Logo = client.Logo;
			}

			return View();
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
			if (string.IsNullOrEmpty(clientId))
			{
				var claimsPrincipal = (ClaimsPrincipal)User;
				clientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;
			}

			var sections = await _sectionRepository.GetClientSectionsAsync(new Guid(clientId), true);

			var client = await _clientRepository.GetClientAsync(new Guid(clientId));

			var clientSectionsVm = new ClientSectionsVm
			{
				Client = client.ToDomainClient(),
				Sections = sections.Select(x=>x.ToDomainSection()).ToList()
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
			var client = await _clientRepository.GetClientAsync(new Guid(addEditSectionVm.Section.ClientId));

			if (addEditSectionVm.Picture != null)
			{
				addEditSectionVm.Section.Picture = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addEditSectionVm.Picture), client.ClientId);
			}

			var sectionId = await _sectionRepository.AddSectionAsync(addEditSectionVm.Section.ToDatabaseSection(client.ClientId));

			await SectionHelper.AddDefaultSectionCategories(sectionId, _categoryRepository);

			return RedirectToAction("Sections", new { clientId = addEditSectionVm.Section.ClientId });
		}

		public async Task<ActionResult> SectionEdit(string clientId, string sectionId)
		{
			var section = await _sectionRepository.GetSectionAsync(new Guid(sectionId));

			var addSectionVm = new AddEditSectionVm
			{
				Section = section.ToDomainSection()
			};

			return View(addSectionVm);

		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> SectionEdit(AddEditSectionVm addEditSectionVm)
		{
			var client = await _clientRepository.GetClientAsync(new Guid(addEditSectionVm.Section.ClientId));

			if (addEditSectionVm.Picture != null)
			{
				addEditSectionVm.Section.Picture = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addEditSectionVm.Picture), client.ClientId);
			}

			var section = await _sectionRepository.GetSectionAsync(new Guid(addEditSectionVm.Section.SectionId));

			await _sectionRepository.UpdateSectionAsync(section, addEditSectionVm.Section);

			return RedirectToAction("Sections", new { clientId = addEditSectionVm.Section.ClientId});

		}

		public async Task<ActionResult> SectionDelete(string clientId, string sectionId)
		{
			await SectionHelper.DeleteDefaultSectionCategories(_categoryRepository, sectionId);

			await _sectionRepository.DeleteSectionAsync(new Guid(sectionId));

			return RedirectToAction("Sections", new {clientId = clientId});
		}

		public async Task<ActionResult> SectionCategories(string clientId, string sectionId)
		{
			var section = await _sectionRepository.GetSectionAsync(new Guid(sectionId));

			var client = await _clientRepository.GetClientAsync(new Guid(clientId));

			var categories = await _categoryRepository.GetSectionCategoriesAsync(new Guid(sectionId), true);

			var sectionCategoriesVm = new SectionCategoriesVm
			{
				Client = client.ToDomainClient(),
				Section = section.ToDomainSection(),
				Categories = categories.Select(x=>x.ToDomainCategory()).ToList()
			};

			return View("Categories", sectionCategoriesVm);

		}

		public async Task<ActionResult> CategoryAdd(string clientId, string sectionId)
		{
			var section = await _sectionRepository.GetSectionAsync(new Guid(sectionId));

			var addCategoryVm = new AddEditCategoryVm
			{
				Section = section.ToDomainSection(),
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
			var client = await _clientRepository.GetClientAsync(new Guid(addCategoryVm.Section.ClientId));

			if (addCategoryVm.Picture != null)
			{
				addCategoryVm.Category.Picture = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addCategoryVm.Picture), client.ClientId);
			}

			var section = await _sectionRepository.GetSectionAsync(new Guid(addCategoryVm.Category.SectionId));

			await _categoryRepository.AddSectionCategoryAsync(addCategoryVm.Category.ToDatabaseCategory(section.SectionId));

			return RedirectToAction("SectionCategories", new { clientId = addCategoryVm.Section.ClientId, sectionId = addCategoryVm.Category.SectionId });

		}

		public async Task<ActionResult> CategoryEdit(string clientId, string categoryId)
		{
			var category = await _categoryRepository.GetCategoryAsync(new Guid(clientId), new Guid(categoryId));

			var section = await _sectionRepository.GetSectionAsync(category.Section.SectionUUID);

			var addCategoryVm = new AddEditCategoryVm
			{
				Section = section.ToDomainSection(),
				Category = category.ToDomainCategory()
			};

			return View("CategoryEdit", addCategoryVm);

		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> CategoryEdit(AddEditCategoryVm addCategoryVm)
		{
			var client = await _clientRepository.GetClientAsync(new Guid(addCategoryVm.Section.ClientId));

			if (addCategoryVm.Picture != null)
			{
				addCategoryVm.Category.Picture = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addCategoryVm.Picture), client.ClientId);
			}

			var category = await _categoryRepository.GetCategoryAsync(new Guid(addCategoryVm.Section.ClientId), new Guid(addCategoryVm.Category.CategoryId));

			await _categoryRepository.UpdateCategoryAsync(category, addCategoryVm.Category);

			return RedirectToAction("SectionCategories", new { clientId = addCategoryVm.Section.ClientId, sectionId = addCategoryVm.Category.SectionId });

		}

		public async Task<ActionResult> CategoryDelete(string clientId, string categoryId, string sectionId)
		{
			await _categoryRepository.DeleteCategoryAsync(new Guid(categoryId));

			return RedirectToAction("SectionCategories", new { clientId = clientId, sectionId = sectionId });
		}

		public async Task<ActionResult> CategoryProducts(string clientId, string categoryId)
		{

			var category = await _categoryRepository.GetCategoryAsync(new Guid(clientId), new Guid(categoryId));

			var section = await _sectionRepository.GetSectionAsync(category.Section.SectionUUID);

			var client = await _clientRepository.GetClientAsync(new Guid(clientId));

			var products = await _categoryRepository.GetCategoryProductsAsync(new Guid(categoryId));

			var categoryProductsVm = new CategoryProductsVm
			{
				Client = client.ToDomainClient(),
				Section = section.ToDomainSection(),
				Category = category.ToDomainCategory(),
				CategoryProducts = products.Select(x=>x.ToDomainProduct()).ToList()
			};

			return View(categoryProductsVm);

		}

		public async Task<ActionResult> ProductAdd(string clientId, string categoryId)
		{
			var category = await _categoryRepository.GetCategoryAsync(new Guid(clientId), new Guid(categoryId));

			var section = await _sectionRepository.GetSectionAsync(category.Section.SectionUUID);

			var addEditProductVm = new AddProductVm
			{
				Section = section.ToDomainSection(),
				Category = category.ToDomainCategory(),
				Product = new Product()
				{
					ReleaseDate = DateTime.Now
				}
			};

			return View(addEditProductVm);

		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> ProductAdd(AddProductVm addProductVm)
		{
			try
			{
				var client = await _clientRepository.GetClientAsync(new Guid(addProductVm.Section.ClientId));

				if (addProductVm.Picture1 != null)			
					addProductVm.Product.Picture1 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addProductVm.Picture1), client.ClientId);			

				if (addProductVm.Picture2 != null)			
					addProductVm.Product.Picture2 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addProductVm.Picture2), client.ClientId);
				
				if (addProductVm.Picture3 != null)			
					addProductVm.Product.Picture3 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addProductVm.Picture3), client.ClientId);			

				if (addProductVm.Picture4 != null)		
					addProductVm.Product.Picture4 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addProductVm.Picture4), client.ClientId);
				
				if (addProductVm.Picture5 != null)				
					addProductVm.Product.Picture5 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addProductVm.Picture5), client.ClientId);				

				if (addProductVm.PictureOther != null)			
					addProductVm.Product.PictureOther = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addProductVm.PictureOther), client.ClientId);
				

				var categoryDb = await _categoryRepository.GetCategoryAsync(new Guid(addProductVm.Section.ClientId), new Guid(addProductVm.Category.CategoryId));

				var productDb = ProductHelper.GenerateProductDbModel(addProductVm.Product, categoryDb.CategoryId, _generalHelper);

				//Save the product
				await _productRepository.AddProductAsync(productDb);

				return RedirectToAction("CategoryProducts", new
				{
					clientId = addProductVm.Section.ClientId,
					categoryId = addProductVm.Category.CategoryId
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

				var product = await _productRepository.GetProductAsync(new Guid(productId));

				var category = await _categoryRepository.GetCategoryAsync(new Guid(clientId), product.Category.CategoryUUID);

				var section = await _sectionRepository.GetSectionAsync(category.Section.SectionUUID);

				var editProductVm = new EditProductVm
				{
					Section = section.ToDomainSection(),
					Category = category.ToDomainCategory(),
					Product = product.ToDomainProduct(),
				};

				return View(editProductVm);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}

		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> ProductEdit(EditProductVm editProductVm)
		{
			try
			{
				var client = await _clientRepository.GetClientAsync(new Guid(editProductVm.Section.ClientId));

				if (editProductVm.Picture1 != null)
					editProductVm.Product.Picture1 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(editProductVm.Picture1), client.ClientId);

				if (editProductVm.Picture2 != null)
					editProductVm.Product.Picture2 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(editProductVm.Picture2), client.ClientId);

				if (editProductVm.Picture3 != null)
					editProductVm.Product.Picture3 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(editProductVm.Picture3), client.ClientId);

				if (editProductVm.Picture4 != null)
					editProductVm.Product.Picture4 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(editProductVm.Picture4), client.ClientId);

				if (editProductVm.Picture5 != null)
					editProductVm.Product.Picture5 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(editProductVm.Picture5), client.ClientId);

				if (editProductVm.PictureOther != null)
					editProductVm.Product.PictureOther = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(editProductVm.PictureOther), client.ClientId);


				var productDb = await _productRepository.GetProductAsync(new Guid(editProductVm.Product.ProductId));

				productDb = ProductHelper.UpdateProductDbModel(productDb, editProductVm.Product);

				await _productRepository.UpdateProductAsync(productDb);

				return RedirectToAction("CategoryProducts", "Admin", new
					{
						clientId = editProductVm.Section.ClientId,
						categoryId = editProductVm.Product.CategoryId
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
				var product = await _productRepository.GetProductAsync(new Guid(productId));

				ProductHelper.DeleteProductPictures(product, _generalHelper);

				await _productRepository.DeleteProductAsync(product);

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
				var product = await _productRepository.GetProductAsync(new Guid(productId));

				var viewProductVm = new ViewProductVm
					{
						ClientId = clientId,
						Product = product.ToDomainProduct()
					};

				return View(viewProductVm);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}

		}

		public async Task<ActionResult> ClientPages(string clientId = null)
		{
			try
			{
				if (string.IsNullOrEmpty(clientId))
				{
					var claimsPrincipal = (ClaimsPrincipal)User;
					clientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;
				}

				var client = await _clientRepository.GetClientAsync(new Guid(clientId));

				var pages = await _clientRepository.GetClientPagesAsync(new Guid(clientId));

				var clientPagesVm = new ClientPagesVm
					{
						Client = client.ToDomainClient(),
						ClientPages = pages.Select(x=>x.ToDomainClientPage()).ToList()
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
				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));

				var clientPageVm = new ClientPageVm
				{
					Client = clientDb.ToDomainClient()
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
			var client = await _clientRepository.GetClientAsync(new Guid(clientPageVm.Client.ClientId));

			await _clientRepository.AddClientPageAsync(clientPageVm.ClientPage.ToDatabaseClientPage(client.ClientId));

			return RedirectToAction("ClientPages", "Admin", new { clientId = clientPageVm.Client.ClientId });
		}

		public async Task<ActionResult> ClientPageEdit(string clientId, string pageName)
		{
			try
			{
				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));

				var page = await _clientRepository.GetClientPageAsync(new Guid(clientId), pageName);

				var clientPageVm = new ClientPageVm
				{
					Client = clientDb.ToDomainClient(),
					ClientPage = page.ToDomainClientPage()
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
			var client = await _clientRepository.GetClientAsync(new Guid(clientPageVm.Client.ClientId));

			await _clientRepository.AddClientPageAsync(clientPageVm.ClientPage.ToDatabaseClientPage(client.ClientId));

			return RedirectToAction("ClientPages", "Admin", new { clientId = clientPageVm .Client.ClientId});
		}

		public async Task<ActionResult> ClientPageDelete(string clientId, string pageName)
		{
			await _clientRepository.DeleteClientPageAsync(new Guid(clientId), pageName);

			return RedirectToAction("ClientPages", new { clientId = clientId});
		}

		public async Task<ActionResult> ClientSettings(string clientId = null)
		{
			try
			{
				if (string.IsNullOrEmpty(clientId))
				{
					var claimsPrincipal = (ClaimsPrincipal)User;
					clientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;
				}

				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));

				var settings = await _clientRepository.GetClientSettingsAsync(new Guid(clientId));

				var clientSettingsVm = new ClientSettingsVm
				{
					Client = clientDb.ToDomainClient(),
					ClientSettings = settings.Select(x=>x.ToDomainClientSetting()).ToList()
				};

				return View(clientSettingsVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		public async Task<ActionResult> ClientSettingAddMissing(string clientId)
		{
			try
			{
				var client = await _clientRepository.GetClientAsync(new Guid(clientId));

				//Get the current settings
				var clientSettings = await _clientRepository.GetClientSettingsAsync(new Guid(clientId));

				//Get the ids
				var settingsIds = clientSettings.Select(x => x.SettingId).ToList();

				await _clientRepository.AllocateMissingClientSettingsAsync(client, settingsIds);

				return RedirectToAction("ClientSettings", "Admin", new { clientId = clientId });
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
				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));

				var clientSettingVm = new ClientSettingVm
				{
					Client = clientDb.ToDomainClient()
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

			var client = await _clientRepository.GetClientAsync(new Guid(clientSettingVm.Client.ClientId));

			await _clientRepository.AddClientSettingAsync(clientSettingVm.ClientSetting.ToDatabaseClientSetting(client.ClientId));

			return RedirectToAction("ClientSettings", "Admin", new { clientId = clientSettingVm.Client.ClientId });
		}

		public async Task<ActionResult> ClientSettingEdit(string clientId, short clientSettingId)
		{
			try
			{
				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));;

				var setting = await _clientRepository.GetClientSettingAsync(new Guid(clientId), clientSettingId);

				var clientPageVm = new ClientSettingVm
				{
					Client = clientDb.ToDomainClient(),
					ClientSetting = setting.ToDomainClientSetting()
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
			var setting = await _globalRepository.GetSettingAsync(clientSettingVm.ClientSetting.Setting.SettingId);

			//Regex validation
			if (!string.IsNullOrEmpty(setting.RegexValidation) && !string.IsNullOrEmpty(clientSettingVm.ClientSetting.StringValue))
			{
				//Do the validation
				var regex = new Regex(setting.RegexValidation);
				var match = regex.Match(clientSettingVm.ClientSetting.StringValue);

				if (!match.Success)
				{
					ModelState.AddModelError("", "Value Validation Failed");

					var clientDb = await _clientRepository.GetClientAsync(new Guid(clientSettingVm.Client.ClientId));

					//var getClientSettingResult = await _clientController.GetClientSettingAsync(clientSettingVm.Client.ClientId, clientSettingVm.ClientSetting.ClientSettingId);
					//var clientSettingResult = getClientSettingResult as OkNegotiatedContentResult<ClientSetting>;

					//var clientSetting = await _clientRepository.GetClientSettingAsync(new Guid(clientSettingVm.Client.ClientId), clientSettingVm.ClientSetting.ClientSettingId);

					return View(clientSettingVm);
				}

			}

			var client = await _clientRepository.GetClientAsync(new Guid(clientSettingVm.Client.ClientId));

			var clientSetting = await _clientRepository.GetClientSettingAsync(new Guid(clientSettingVm.Client.ClientId), clientSettingVm.ClientSetting.ClientSettingId);

			await _clientRepository.UpdateClientSettingAsync(clientSettingVm.ClientSetting.ToDatabaseClientSetting(client.ClientId), clientSetting.ToDomainClientSetting());

			return RedirectToAction("ClientSettings", "Admin", new { clientId = clientSettingVm.Client.ClientId });
		}

		public async Task<ActionResult> ClientSettingDelete(string clientId, short settingId)
		{
			await _clientRepository.DeleteClientSettingAsync(new Guid(clientId), settingId);

			return RedirectToAction("ClientSettings", new { clientId = clientId });
		}

		public async Task<ActionResult> ClientPostage(string clientId = null)
		{
			try
			{
				if (string.IsNullOrEmpty(clientId))
				{
					var claimsPrincipal = (ClaimsPrincipal)User;
					clientId = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;
				}

				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));

				var deliveryOptions = await _clientRepository.GetClientDeliveryOptionsAsync(new Guid(clientId));

				var clientDeliveryOptionsVm = new ClientDeliveryOptionsVm
				{
					Client = clientDb.ToDomainClient(),
					ClientDeliveryOptions = deliveryOptions.Select(x=>x.ToDomainClientDeliveryOption()).ToList()
				};

				return View(clientDeliveryOptionsVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		public async Task<ActionResult> ClientPostageAdd(string clientId)
		{
			try
			{
				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));

				var clientDeliveryOptionVm = new ClientDeliveryOptionVm
				{
					Client = clientDb.ToDomainClient(),
					ClientDeliveryOption = new ClientDeliveryOption{Enabled = true}
				};

				return View(clientDeliveryOptionVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> ClientPostageAdd(ClientDeliveryOptionVm clientDeliveryOptionVm)
		{

			var client = await _clientRepository.GetClientAsync(new Guid(clientDeliveryOptionVm.Client.ClientId));

			await _clientRepository.AddClientDeliveryOptionAsync(clientDeliveryOptionVm.ClientDeliveryOption.ToDatabaseClientDeliveryOption(client.ClientId));

			return RedirectToAction("ClientPostage", "Admin", new { clientId = clientDeliveryOptionVm.Client.ClientId });
		}

		public async Task<ActionResult> ClientPostageEdit(string clientId, short clientDeliveryOptionId)
		{
			try
			{
				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));

				var deliveryOption = await _clientRepository.GetClientDeliveryOptionAsync(clientDeliveryOptionId);

				var clientDeliveryOptionVm = new ClientDeliveryOptionVm
				{
					Client = clientDb.ToDomainClient(),
					ClientDeliveryOption = deliveryOption.ToDomainClientDeliveryOption()
				};

				return View(clientDeliveryOptionVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> ClientPostageEdit(ClientDeliveryOptionVm clientDeliveryOptionVm)
		{
			var deliveryOption = await _clientRepository.GetClientDeliveryOptionAsync(clientDeliveryOptionVm.ClientDeliveryOption.ClientDeliveryOptionId);

			await _clientRepository.UpdateClientDeliveryOptionAsync(deliveryOption, clientDeliveryOptionVm.ClientDeliveryOption);

			return RedirectToAction("ClientPostage", "Admin", new { clientId = clientDeliveryOptionVm.Client.ClientId });
		}

		public async Task<ActionResult> ClientPostageDelete(string clientId, short clientDeliveryOptionId)
		{
			await _clientRepository.DeleteClientDeliveryOptionAsync(new Guid(clientId), clientDeliveryOptionId);

			return RedirectToAction("ClientPostage", new { clientId = clientId });
		}

	}
}