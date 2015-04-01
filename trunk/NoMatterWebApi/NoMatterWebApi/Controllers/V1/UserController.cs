using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CustomAuthLib;
using NoMatterDatabaseModel;
using NoMatterWebApi.ActionResults;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Enums;
using NoMatterWebApi.Extensions;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApi.Models;
using NoMatterWebApiModels.Models;
using Order = NoMatterDatabaseModel.Order;
using User = NoMatterDatabaseModel.User;


namespace NoMatterWebApi.Controllers.V1
{
	[RoutePrefix("api/v1/clients")]
	public class UserController : ApiController
	{
		private IClientRepository _clientRepository;
		private IOrderRepository _orderRepository;
		private ICartRepository _cartRepository;
		private IUserRepository _userRepository;
		private IGeneralHelper generalHelper;
		private IGlobalSettings _globalSettings;
		private ICustomAuthentication _customAuthentication;

		public UserController()
		{
			var databaseEntity = new DatabaseEntities();

			_orderRepository = new OrderRepository(databaseEntity);
			_clientRepository = new ClientRepository(databaseEntity);
			_cartRepository = new CartRepository(databaseEntity);
			_userRepository = new UserRepository(databaseEntity);
			generalHelper = new GeneralHelper();
			_globalSettings = new GlobalSettings();
			_customAuthentication = CustomAuthentication.Instance;
		}

		public UserController(IClientRepository clientRepository, IOrderRepository orderRepository, ICartRepository cartRepository, IUserRepository userRepository, IGeneralHelper facebookHelper, ICustomAuthentication customAuthentication)
		{
			_clientRepository = clientRepository;
			_cartRepository = cartRepository;
			_orderRepository = orderRepository;
			_userRepository = userRepository;
			generalHelper = facebookHelper;
			_customAuthentication = customAuthentication;
		}

		//// GET api/v1/sections/{sectionUuid}/categories
		//[Route("{clientUuid}/users/{email}")]
		//[ResponseType(typeof(List<User>))]
		//public async Task<IHttpActionResult> GetUserByEmails(string clientUuid, string email)
		//{

		//	var userDb = await _userRepository.GetClientUserByEmailAsync(new Guid(clientUuid), email);

		//	var user = userDb.ToDomainUser();

		//	return Ok(user);
		//}

		// POST api/Account/Register
		[AllowAnonymous]
		[Route("{clientId}/users/register")]
		public async Task<IHttpActionResult> Register(string clientId, RegisterBindingModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
			if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

			var passwordSalt = PasswordHelper.CreateSalt();

			var user = new User { Email = model.Email, FullName = model.FullName };
			//
			//IdentityResult result = await UserManager.CreateAsync(user, model.Password);

			//Non facebook user
			var userDb = new User
			{
				Client = clientDb,
				CredentialTypeId = (byte)CredentialTypeEnum.UsernameAndPassword,
				FullName = model.FullName,
				Identifier = model.Email,
				Email = model.Email,
				PasswordSalt = passwordSalt,
				Password = PasswordHelper.StrToByteArray(PasswordHelper.CreatePasswordHash(model.Password, passwordSalt))
			};

			var userUuid = await _userRepository.SaveUserAsync(userDb);

			return Ok();
		}

		// POST api/v1/clients/{clientUuid}/users/register>
		[Route("{clientUuid}/users/register2")]
		[ResponseType(typeof(UserAuthenticatedResult))]
		[HttpPost]
		public async Task<IHttpActionResult> RegisterUser(string clientUuid, NewUser model)
		{
			try
			{
				var passwordSalt = PasswordHelper.CreateSalt();

				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientUuid));
				if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				var userDb = await _userRepository.GetClientUserByEmailAsync(new Guid(clientUuid), model.Email);
				if (userDb != null) return new CustomBadRequest(Request, ApiResultCode.UserAlreadyExists);

				if (string.IsNullOrEmpty(model.FacebookToken))
				{
					//Non facebook user
					userDb = new User
					{
						Client = clientDb,
						CredentialTypeId = (byte)CredentialTypeEnum.UsernameAndPassword,
						FullName = model.FirstName + " " +model.LastName,
						Identifier = model.Email,
						Email = model.Email,
						PasswordSalt = passwordSalt,
						Password = PasswordHelper.StrToByteArray(PasswordHelper.CreatePasswordHash(model.Password, passwordSalt))
					};
				}
				else
				{
					//Facebook User.. need to validate the token etc
					var facebookUser = await generalHelper.VerifyFacebookTokenAsync(model.FacebookToken);

					userDb = new User
					{
						Client = clientDb,
						CredentialTypeId = (byte)CredentialTypeEnum.Facebook,
						Identifier = facebookUser.Id,
						FullName = facebookUser.First_Name + " " + facebookUser.Last_Name,
						Email = facebookUser.Email,
						PasswordSalt = passwordSalt,
					};
				}

				var userUuid = await _userRepository.SaveUserAsync(userDb);

				//Return the user details and token etc
				var user = new UserAuthenticatedResult()
				{
					Id = userUuid,
					Fullname = model.FirstName + " " + model.LastName,
					Email = model.Email,
					TokenDetails = new TokenDetails()
					{
						AccessToken = passwordSalt
					}
				};

				return Ok(user);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// POST api/v1/clients/{clientId}/users>
		[Route("{clientId}/users/authenticate")]
		[ResponseType(typeof(UserAuthenticatedResult))]
		[HttpPost]
		public async Task<IHttpActionResult> AuthenticateUser(string clientId, UserAuthenticateModel model)
		{
			User userDb;

			Guid? clientUuid = null;

			if (clientId != "sys") clientUuid = new Guid(clientId);

			if (!string.IsNullOrEmpty(model.FacebookToken))
			{
				//Facebook User.. need to validate the token etc
				var facebookUser = await generalHelper.VerifyFacebookTokenAsync(model.FacebookToken);

				userDb = await _userRepository.GetClientUserByFacebookIdAsync(clientUuid, facebookUser.Id);

				if (userDb == null)
				{
					return new CustomBadRequest(Request, ApiResultCode.AuthenticationFailed);
				}
			}
			else
			{
				userDb = await _userRepository.GetClientUserByEmailAsync(clientUuid, model.Email);

				if (userDb == null)
				{
					return new CustomBadRequest(Request, ApiResultCode.AuthenticationFailed);
				}

				var dBytes = userDb.Password;
				var enc = new UTF8Encoding();
				var length = dBytes.TakeWhile(b => b != 0).Count();
				var strDbPassword = enc.GetString(dBytes, 0, length);

				if (strDbPassword != PasswordHelper.CreatePasswordHash(model.Password, userDb.PasswordSalt))
				{
					return new CustomBadRequest(Request, ApiResultCode.AuthenticationFailed);
				}
			}

			//Profile successfully authenticated, so generate the response with token details etc
			var accessToken = _customAuthentication.CreateAccessToken(userDb.UserUUID.ToString(), userDb.Client.ClientUUID.ToString());

			var userRoles = String.Join(",", userDb.UserRoles.Select(x=>x.Role.RoleName));

			//Return the user details and token etc
			var userAuthenticated = new UserAuthenticatedResult()
			{
				Id = userDb.UserUUID.ToString(),
				ClientId = userDb.Client != null ? userDb.Client.ClientUUID.ToString() : null,
				Fullname = userDb.FullName,
				Email = userDb.Email,
				UserRoles = userRoles,
				TokenDetails = new TokenDetails()
				{
					AccessToken = accessToken.Token,
					AccessTokenExpiryUtc = accessToken.Expires,
				}
			};

			return Ok(userAuthenticated);
		}

		// POST api/v1/clients/{id}/users/createorupdate>
		[Route("{clientUuid}/users")]
		[ResponseType(typeof(string))]
		[HttpPost]
		public async Task<IHttpActionResult> CreateOrUpdateUser(string clientUuid, UserModel model)
		{
			try
			{
		
				string userId;

				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientUuid));
				if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				var userDb = await _userRepository.GetClientUserByEmailAsync(new Guid(clientUuid), model.Email);

				if (userDb == null)
				{
					//Create the user

					userDb = new User
						{
							Client = clientDb,
							CredentialTypeId = 3,
							Identifier = model.Email,
							Email = model.Email,
							FullName = model.Fullname,
							ContactNumber = model.ContactNumber,
							Address = model.Address,
							Suburb = model.Suburb,
							City = model.City,
							Province = model.Province,
							Country = model.Country,
							PostalCode = model.PostalCode
						};

					userId = await _userRepository.SaveUserAsync(userDb);
				}
				else
				{
					userId = userDb.UserUUID.ToString();

					userDb.FullName = model.Fullname;
					userDb.ContactNumber = model.ContactNumber;
					userDb.Address = model.Address;

					userDb.Suburb = model.Suburb;
					userDb.City = model.City;
					userDb.Province = model.Province;
					userDb.Country = model.Country;
					userDb.PostalCode = model.PostalCode;


					//Update the user
					await _userRepository.UpdateUserAsync(userDb);
				}


				return Ok(userId);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		// POST api/v1/clients/{id}/users/{userId}/orders>
		[Route("{clientId}/users/{userId}/orders")]
		[ResponseType(typeof(string))]
		[HttpPost]
		public async Task<IHttpActionResult> CreateUserOrder(string clientId, string userId, GenerateUserOrder model)
		{
			try
			{
	
				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
				if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				var userDb = await _userRepository.GetUserByUuidAsync(new Guid(userId));
				if (userDb == null) return new CustomBadRequest(Request, ApiResultCode.UserNotFound);

				var clientDeliveryOption = await _clientRepository.GetClientDeliveryOptionAsync(Convert.ToInt16(model.ClientDeliveryOptionId));

				if (userDb.Client.ClientUUID != clientDb.ClientUUID) return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToClient);

				var cartProductsDb = await _cartRepository.GetCartProductsAsync(model.CartId);

				var cartProducts = cartProductsDb.Select(x => x.ToDomainCartProduct()).ToList();

				var order = new Order
					{
						User = userDb,
						TotalAmount = cartProducts.Sum(x => x.Product.DiscountDetails.DiscountedPrice),
						DeliveryAmount = clientDeliveryOption.DeliveryAmount,
						Message = model.Message,
						ClientDeliveryOptionId = Convert.ToInt16(model.ClientDeliveryOptionId),
						OrderStatusId = 1,
					};

				foreach (var cartProduct in cartProductsDb)
				{
					order.OrderProducts.Add(new OrderProduct() { ProductId = cartProduct.Product.ProductId });
				}

				var orderId = await _orderRepository.AddOrderAsync(order);

				return Ok(orderId);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		
	}
}