using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using CustomAuthLib;
using NoMatterDataLibrary;
using NoMatterDataLibrary.Enums;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApiModels.Models;
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
		private IFacebookHelper _facebookHelper;

		public AdminController()
		{
			_clientRepository = new ClientRepository();
			_sectionRepository = new SectionRepository();
			_categoryRepository = new CategoryRepository();
			_productRepository = new ProductRepository();
			_globalRepository = new GlobalRepository();

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
			var authUserclientUuid = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;

			ViewBag.UserName = User.Identity.Name;

			if (string.IsNullOrEmpty(authUserclientUuid))
			{
				//System user not allocated to a client
				ViewBag.ClientName = "System Administrator";
			}
			else
			{
				var client = await _clientRepository.GetClientAsync(new Guid(authUserclientUuid));

				ViewBag.ClientName = client.ClientName;
				ViewBag.Logo = client.Logo;
			}

			return View();
		}

		

		public async Task<ActionResult> Products(string clientUuid)
		{

			//string productStatus = "Active";
			//string categoryId = null;

			//var productController = new ProductController();

			//var products = await productController.GetClientProductsAsync(clientUuid, productStatus, categoryId);

			//var viewClientProducts = new ViewClientProductsVm
			//{
			//	ClientProducts = products
			//};

			//return View(viewClientProducts);

			return View();
		}


		public async Task<ActionResult> Sections(string clientUuid = null)
		{
			if (string.IsNullOrEmpty(clientUuid))
			{
				var claimsPrincipal = (ClaimsPrincipal)User;
				clientUuid = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;
			}

			var sections = await _sectionRepository.GetClientSectionsAsync(new Guid(clientUuid), true);

			var client = await _clientRepository.GetClientAsync(new Guid(clientUuid));

			var clientSectionsVm = new ClientSectionsVm
			{
				Client = client,
				Sections = sections
			};

			return View(clientSectionsVm);

		}

		public ActionResult SectionAdd(string clientUuid)
		{
			var addSectionVm = new AddEditSectionVm
			{
				Section = new Section()
				{
					Client = new Client() { ClientUuid = clientUuid },
					Hidden = false
				}
			};

			return View(addSectionVm);
		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> SectionAdd(AddEditSectionVm addEditSectionVm)
		{
			try
			{
				if (addEditSectionVm.Picture != null)
				{
					addEditSectionVm.Section.Picture = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addEditSectionVm.Picture), addEditSectionVm.Section.Client.ClientUuid);
				}

				var sectionId = await _sectionRepository.AddSectionAsync(addEditSectionVm.Section);

				await SectionHelper.AddDefaultSectionCategories(sectionId, _categoryRepository);

				return RedirectToAction("Sections", new { clientUuid = addEditSectionVm.Section.Client.ClientUuid });
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
			
		}

		public async Task<ActionResult> SectionEdit(string clientUuid, int sectionId)
		{
			var section = await _sectionRepository.GetSectionAsync(sectionId);

			var addSectionVm = new AddEditSectionVm
			{
				Section = section
			};

			return View(addSectionVm);

		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> SectionEdit(AddEditSectionVm addEditSectionVm)
		{
			if (addEditSectionVm.Picture != null)
			{
				addEditSectionVm.Section.Picture = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addEditSectionVm.Picture), addEditSectionVm.Section.Client.ClientUuid);
			}

			await _sectionRepository.UpdateSectionAsync(addEditSectionVm.Section);

			return RedirectToAction("Sections", new { clientUuid = addEditSectionVm.Section.Client.ClientUuid});

		}

		public async Task<ActionResult> SectionDelete(string clientUuid, int sectionId)
		{
			await SectionHelper.DeleteDefaultSectionCategories(_categoryRepository, sectionId);

			await _sectionRepository.DeleteSectionAsync(sectionId);

			return RedirectToAction("Sections", new {clientUuid = clientUuid});
		}

		public async Task<ActionResult> SectionCategories(string clientUuid, int sectionId)
		{
			var section = await _sectionRepository.GetSectionAsync(sectionId);

			//var client = await _clientRepository.GetClientAsync(new Guid(clientUuid));

			var categories = await _categoryRepository.GetSectionCategoriesAsync(sectionId, true);

			var sectionCategoriesVm = new SectionCategoriesVm
			{
				//Client = client,
				Section = section,
				Categories = categories
			};

			return View("Categories", sectionCategoriesVm);

		}

		public async Task<ActionResult> CategoryAdd(string clientUuid, int sectionId)
		{
			var section = await _sectionRepository.GetSectionAsync(sectionId);

			var addCategoryVm = new AddEditCategoryVm
			{
				Section = section,
				Category = new Category()
				{
					Section = new Section
						{
							SectionId = sectionId
						}
				}
			};

			return View("CategoryAdd", addCategoryVm);
		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> CategoryAdd(AddEditCategoryVm addCategoryVm)
		{

			if (addCategoryVm.Picture != null)
			{
				addCategoryVm.Category.Picture = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addCategoryVm.Picture), addCategoryVm.Section.Client.ClientUuid);
			}

			await _categoryRepository.AddSectionCategoryAsync(addCategoryVm.Category);

			return RedirectToAction("SectionCategories", new { clientUuid = addCategoryVm.Section.Client.ClientUuid, sectionId = addCategoryVm.Category.Section.SectionId });

		}

		public async Task<ActionResult> CategoryEdit(string clientUuid, int categoryId)
		{
			var category = await _categoryRepository.GetCategoryAsync(new Guid(clientUuid), categoryId);

			var section = await _sectionRepository.GetSectionAsync(category.Section.SectionId);

			var addCategoryVm = new AddEditCategoryVm
			{
				Section = section,
				Category = category
			};

			return View("CategoryEdit", addCategoryVm);

		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> CategoryEdit(AddEditCategoryVm addCategoryVm)
		{
			if (addCategoryVm.Picture != null)
			{
				addCategoryVm.Category.Picture = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addCategoryVm.Picture), addCategoryVm.Section.Client.ClientUuid);
			}

			await _categoryRepository.UpdateCategoryAsync(addCategoryVm.Category);

			return RedirectToAction("SectionCategories", new { clientUuid = addCategoryVm.Section.Client.ClientUuid, sectionId = addCategoryVm.Category.Section.SectionId });

		}

		public async Task<ActionResult> CategoryDelete(string clientUuid, int categoryId, int sectionId)
		{
			await _categoryRepository.DeleteCategoryAsync(categoryId);

			return RedirectToAction("SectionCategories", new { clientUuid = clientUuid, sectionId = sectionId });
		}

		public async Task<ActionResult> CategoryProducts(string clientUuid, int categoryId)
		{

			var category = await _categoryRepository.GetCategoryAsync(new Guid(clientUuid), categoryId);

			//var section = await _sectionRepository.GetSectionAsync(category.SectionId);

			//var client = await _clientRepository.GetClientAsync(new Guid(clientUuid));

			var products = await _categoryRepository.GetCategoryProductsAsync(categoryId);

			var categoryProductsVm = new CategoryProductsVm
			{
				//Client = client,
				//Section = section,
				Category = category,
				CategoryProducts = products
			};

			return View(categoryProductsVm);

		}

		public async Task<ActionResult> ProductAdd(string clientUuid, int categoryId)
		{
			var category = await _categoryRepository.GetCategoryAsync(new Guid(clientUuid), categoryId);

			var suppliers = await _clientRepository.GetClientSuppliersAsync(new Guid(clientUuid));

			var productTypes = await _productRepository.GetProductTypes();

			var addEditProductVm = new AddProductVm
			{
				Category = category,
				Suppliers = suppliers,
				ProductTypes = productTypes,
				Product = new Product()
				{
					ClientUuid = clientUuid,
					CategoryId = category.CategoryId,
					ReleaseDate = DateTime.Now
				},
				
			};

			return View(addEditProductVm);

		}

		[HttpPost]
		[ValidateInput(false)]
		public async Task<ActionResult> ProductAdd(AddProductVm addProductVm)
		{
			try
			{
				
				var clientUuid = addProductVm.Product.ClientUuid;

				if (addProductVm.Picture1 != null)
					addProductVm.Product.Picture1 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addProductVm.Picture1), clientUuid);			

				if (addProductVm.Picture2 != null)
					addProductVm.Product.Picture2 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addProductVm.Picture2), clientUuid);
				
				if (addProductVm.Picture3 != null)
					addProductVm.Product.Picture3 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addProductVm.Picture3), clientUuid);			

				if (addProductVm.Picture4 != null)
					addProductVm.Product.Picture4 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addProductVm.Picture4), clientUuid);
				
				if (addProductVm.Picture5 != null)
					addProductVm.Product.Picture5 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addProductVm.Picture5), clientUuid);				

				if (addProductVm.PictureOther != null)
					addProductVm.Product.PictureOther = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(addProductVm.PictureOther), clientUuid);


				var client = await _clientRepository.GetClientAsync(new Guid(clientUuid));
				
				//Save the product
				 var productUuid = await _productRepository.AddProductAsync(addProductVm.Product);
				
				 var shortUrl = _generalHelper.MakeGoogleShortUrl(client.DomainName + "/shop/product/" + productUuid);

				if (!string.IsNullOrEmpty(shortUrl))
				{
					await _productRepository.UpdateClientShortUrlAsync(new Guid(productUuid), shortUrl);
				}
				 
				return RedirectToAction("CategoryProducts", new
				{
					clientUuid = addProductVm.Product.ClientUuid,
					categoryId = addProductVm.Product.CategoryId
				});
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		public async Task<ActionResult> ProductEdit(string clientUuid, int productId)
		{
			try
			{

				var product = await _productRepository.GetProductAsync(productId);

				var editProductVm = new EditProductVm
				{
					Product = product,
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
				var clientUuid = editProductVm.Product.Category.Section.Client.ClientUuid;

				if (editProductVm.Picture1 != null)
					editProductVm.Product.Picture1 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(editProductVm.Picture1), clientUuid);

				if (editProductVm.Picture2 != null)
					editProductVm.Product.Picture2 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(editProductVm.Picture2), clientUuid);

				if (editProductVm.Picture3 != null)
					editProductVm.Product.Picture3 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(editProductVm.Picture3), clientUuid);

				if (editProductVm.Picture4 != null)
					editProductVm.Product.Picture4 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(editProductVm.Picture4), clientUuid);

				if (editProductVm.Picture5 != null)
					editProductVm.Product.Picture5 = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(editProductVm.Picture5), clientUuid);

				if (editProductVm.PictureOther != null)
					editProductVm.Product.PictureOther = _generalHelper.SaveImage(GeneralHelper.ConvertPicToBase64String(editProductVm.PictureOther), clientUuid);

				await _productRepository.UpdateProductAsync(editProductVm.Product);

				return RedirectToAction("CategoryProducts", "Admin", new
					{
						clientUuid = clientUuid,
						categoryId = editProductVm.Product.CategoryId
					});

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		public async Task<ActionResult> DeleteProduct(string clientUuid, int productId, string categoryId)
		{
			try
			{
				var product = await _productRepository.GetProductAsync(productId);

				ProductHelper.DeleteProductPictures(product, _generalHelper);

				await _productRepository.DeleteProductAsync(productId);

				return RedirectToAction("CategoryProducts", "Admin", new { clientUuid = clientUuid, categoryId = categoryId });
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}

		}

		public async Task<ActionResult> ViewProduct(string clientUuid, int productId)
		{
			try
			{
				var product = await _productRepository.GetProductAsync(productId);

				var viewProductVm = new ViewProductVm
					{
						ClientUuid = clientUuid,
						Product = product
					};

				return View(viewProductVm);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}

		}

		public async Task<ActionResult> ProductFacebookPost(string clientUuid, int productId)
		{
			try
			{
				var product = await _productRepository.GetProductAsync(productId);

				string facebookMessageTemplate = await ClientHelper.GetClientSetting(_clientRepository, new Guid(clientUuid), SettingEnum.FacebookPostTemplate);

				if (string.IsNullOrEmpty(facebookMessageTemplate))
				{
					throw new Exception("Facebook template for client not specified");
				}

				//var facebookPostText = String.Format(facebookMessageTemplate,
				//	product.Description,
				//	product.Price,
				//	String.IsNullOrEmpty(product.Size) ? "" : "Size: " + product.Size,
				//	product.ItemShortUrl);

				var facebookPostText = facebookMessageTemplate;


				var postToFacebookVm = new PostToFacebookVm
				{
					ClientUuid = clientUuid,
					Product = product,
					SelectedPicturePath = product.Picture1,
					FacebookPostText = facebookPostText
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
		public ActionResult ProductFacebookPost(PostToFacebookVm postToFacebookVm)
		{
			//var path = Path.Combine(Server.MapPath(.ShopImagesPath), picturePath);

			var path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Images/" + postToFacebookVm.ClientUuid + "/"), postToFacebookVm.SelectedPicturePath);

			//Try post to facebook
			var facebookPostId = _facebookHelper.PostItemToFacebook(postToFacebookVm.FacebookPostText, postToFacebookVm.FacebookAlbumId, path);

			//if (!string.IsNullOrEmpty(facebookPostId))
			//{

			//	//ShopAdminHelper.UpdateFacebookPostId(shopItemId, facebookPostId);

			//	return RedirectToAction("Edit", new { id = shopItemId });
			//}

			//ViewBag.ShopItemId = shopItemId;

			return View("FacebookPostFailed");

		}

		public async Task<ActionResult> ClientPages(string clientUuid = null)
		{
			try
			{
				if (string.IsNullOrEmpty(clientUuid))
				{
					var claimsPrincipal = (ClaimsPrincipal)User;
					clientUuid = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;
				}

				var client = await _clientRepository.GetClientAsync(new Guid(clientUuid));

				var pages = await _clientRepository.GetClientPagesAsync(new Guid(clientUuid));

				var clientPagesVm = new ClientPagesVm
					{
						Client = client,
						ClientPages = pages
					};

				return View(clientPagesVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		public async Task<ActionResult> ClientPageAdd(string clientUuid)
		{
			try
			{
				var client = await _clientRepository.GetClientAsync(new Guid(clientUuid));

				var clientPageVm = new ClientPageVm
				{
					Client = client
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
			//var client = await _clientRepository.GetClientAsync(new Guid(clientPageVm.Client.ClientUuid));

			await _clientRepository.AddClientPageAsync(clientPageVm.ClientPage);

			return RedirectToAction("ClientPages", "Admin", new { clientUuid = clientPageVm.Client.ClientUuid });
		}

		public async Task<ActionResult> ClientPageEdit(string clientUuid, string pageName)
		{
			try
			{
				var client = await _clientRepository.GetClientAsync(new Guid(clientUuid));

				var page = await _clientRepository.GetClientPageAsync(new Guid(clientUuid), pageName);

				var clientPageVm = new ClientPageVm
				{
					Client = client,
					ClientPage = page
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
			//var client = await _clientRepository.GetClientAsync(new Guid(clientPageVm.Client.ClientUuid));

			await _clientRepository.AddClientPageAsync(clientPageVm.ClientPage);

			return RedirectToAction("ClientPages", "Admin", new { clientUuid = clientPageVm .Client.ClientUuid});
		}

		public async Task<ActionResult> ClientPageDelete(string clientUuid, string pageName)
		{
			await _clientRepository.DeleteClientPageAsync(new Guid(clientUuid), pageName);

			return RedirectToAction("ClientPages", new { clientUuid = clientUuid});
		}

		public async Task<ActionResult> ClientSettings(string clientUuid = null, bool fromCreateClient = false)
		{
			try
			{
				if (string.IsNullOrEmpty(clientUuid))
				{
					var claimsPrincipal = (ClaimsPrincipal)User;
					clientUuid = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;
				}

				var client = await _clientRepository.GetClientAsync(new Guid(clientUuid));

				var settings = await _clientRepository.GetClientSettingsAsync(new Guid(clientUuid));

				var clientSettingsVm = new ClientSettingsVm
				{
					Client = client,
					ClientSettings = settings,
					FromCreateClient = fromCreateClient
				};

				return View(clientSettingsVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		public async Task<ActionResult> ClientSettingAddMissing(string clientUuid)
		{
			try
			{
				await ClientHelper.ClientSettingsAddMissing(_clientRepository, new Guid(clientUuid));

				return RedirectToAction("ClientSettings", "Admin", new { clientUuid = clientUuid });
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		public async Task<ActionResult> ClientSettingAdd(string clientUuid)
		{
			try
			{
				var client = await _clientRepository.GetClientAsync(new Guid(clientUuid));

				var clientSettingVm = new ClientSettingVm
				{
					Client = client
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

			var client = await _clientRepository.GetClientAsync(new Guid(clientSettingVm.Client.ClientUuid));

			await _clientRepository.AddClientSettingAsync(clientSettingVm.ClientSetting);

			return RedirectToAction("ClientSettings", "Admin", new { clientUuid = clientSettingVm.Client.ClientUuid });
		}

		public async Task<ActionResult> ClientSettingEdit(string clientUuid, short clientSettingId)
		{
			try
			{
				var client = await _clientRepository.GetClientAsync(new Guid(clientUuid));;

				var setting = await _clientRepository.GetClientSettingAsync(new Guid(clientUuid), clientSettingId);

				var clientPageVm = new ClientSettingVm
				{
					Client = client,
					ClientSetting = setting
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

					var clientDb = await _clientRepository.GetClientAsync(new Guid(clientSettingVm.Client.ClientUuid));

					//var getClientSettingResult = await _clientController.GetClientSettingAsync(clientSettingVm.Client.ClientUuid, clientSettingVm.ClientSetting.ClientSettingId);
					//var clientSettingResult = getClientSettingResult as OkNegotiatedContentResult<ClientSetting>;

					//var clientSetting = await _clientRepository.GetClientSettingAsync(new Guid(clientSettingVm.Client.ClientUuid), clientSettingVm.ClientSetting.ClientSettingId);

					return View(clientSettingVm);
				}

			}

			var client = await _clientRepository.GetClientAsync(new Guid(clientSettingVm.Client.ClientUuid));

			//var clientSetting = await _clientRepository.GetClientSettingAsync(new Guid(clientSettingVm.Client.ClientUuid), clientSettingVm.ClientSetting.ClientSettingId);

			await _clientRepository.UpdateClientSettingAsync(clientSettingVm.ClientSetting);

			return RedirectToAction("ClientSettings", "Admin", new { clientUuid = clientSettingVm.Client.ClientUuid });
		}

		public async Task<ActionResult> ClientSettingDelete(string clientUuid, short settingId)
		{
			await _clientRepository.DeleteClientSettingAsync(new Guid(clientUuid), settingId);

			return RedirectToAction("ClientSettings", new { clientUuid = clientUuid });
		}

		public async Task<ActionResult> ClientPostage(string clientUuid = null)
		{
			try
			{
				if (string.IsNullOrEmpty(clientUuid))
				{
					var claimsPrincipal = (ClaimsPrincipal)User;
					clientUuid = claimsPrincipal.FindFirst(x => x.Type == CustomAuthentication.ClientId).Value;
				}

				var client = await _clientRepository.GetClientAsync(new Guid(clientUuid));

				var deliveryOptions = await _clientRepository.GetClientDeliveryOptionsAsync(new Guid(clientUuid));

				var clientDeliveryOptionsVm = new ClientDeliveryOptionsVm
				{
					Client = client,
					ClientDeliveryOptions = deliveryOptions
				};

				return View(clientDeliveryOptionsVm);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				throw;
			}
		}

		public async Task<ActionResult> ClientPostageAdd(string clientUuid)
		{
			try
			{
				var client = await _clientRepository.GetClientAsync(new Guid(clientUuid));

				var clientDeliveryOptionVm = new ClientDeliveryOptionVm
				{
					Client = client,
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

			//var client = await _clientRepository.GetClientAsync(new Guid(clientDeliveryOptionVm.Client.ClientUuid));

			await _clientRepository.AddClientDeliveryOptionAsync(clientDeliveryOptionVm.ClientDeliveryOption);

			return RedirectToAction("ClientPostage", "Admin", new { clientUuid = clientDeliveryOptionVm.Client.ClientUuid });
		}

		public async Task<ActionResult> ClientPostageEdit(string clientUuid, short clientDeliveryOptionId)
		{
			try
			{
				var client = await _clientRepository.GetClientAsync(new Guid(clientUuid));

				var deliveryOption = await _clientRepository.GetClientDeliveryOptionAsync(clientDeliveryOptionId);

				var clientDeliveryOptionVm = new ClientDeliveryOptionVm
				{
					Client = client,
					ClientDeliveryOption = deliveryOption
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
			//var deliveryOption = await _clientRepository.GetClientDeliveryOptionAsync(clientDeliveryOptionVm.ClientDeliveryOption.ClientDeliveryOptionId);

			await _clientRepository.UpdateClientDeliveryOptionAsync(clientDeliveryOptionVm.ClientDeliveryOption);

			return RedirectToAction("ClientPostage", "Admin", new { clientUuid = clientDeliveryOptionVm.Client.ClientUuid });
		}

		public async Task<ActionResult> ClientPostageDelete(string clientUuid, short clientDeliveryOptionId)
		{
			await _clientRepository.DeleteClientDeliveryOptionAsync(new Guid(clientUuid), clientDeliveryOptionId);

			return RedirectToAction("ClientPostage", new { clientUuid = clientUuid });
		}

	}
}