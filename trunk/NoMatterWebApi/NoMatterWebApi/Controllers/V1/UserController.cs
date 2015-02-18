using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NoMatterApiDAL.DatabaseModel;
using NoMatterApiDAL.Repositories;
using NoMatterWebApi.Enums;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApi.Models;
using NoMatterWebApiModels.Models;
using User = NoMatterApiDAL.DatabaseModel.User;

namespace NoMatterWebApi.Controllers.V1
{
	[RoutePrefix("api/v1/clients")]
	public class UserController : ApiController
	{
		private IClientRepository _clientRepository;
		private IUserRepository _userRepository;
		private IGeneralHelper _generalHelper;

		public UserController()
		{
			var databaseEntity = new DatabaseEntities();

			_clientRepository = new ClientRepository(databaseEntity);
			_userRepository = new UserRepository(databaseEntity);
			_generalHelper = new GeneralHelper();
		}

		public UserController(IClientRepository clientRepository, IUserRepository userRepository, IGeneralHelper facebookHelper)
		{
			_clientRepository = clientRepository;
			_userRepository = userRepository;
			_generalHelper = facebookHelper;
		}

		// POST api/v1/clients/{clientUuid}/users>
		[Route("{clientUuid}/users")]
		[ResponseType(typeof(UserAuthenticatedResult))]
		[HttpPost]
		public async Task<IHttpActionResult> RegisterUser(string clientUuid, NewUser model)
		{
			try
			{
				var passwordSalt = PasswordHelper.CreateSalt();

				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientUuid));
				if (clientDb == null) return BadRequest("Client Not Found");

				var userDb = await _userRepository.GetClientUserByEmailAsync(new Guid(clientUuid), model.Email);
				if (userDb != null) return BadRequest("User Already Exists");

				if (string.IsNullOrEmpty(model.FacebookToken))
				{
					//Non facebook user
					userDb = new User
					{
						Client = clientDb,
						CredentialTypeId = (byte)CredentialTypeEnum.UsernameAndPassword,
						FirstName = model.FirstName,
						LastName = model.LastName,
						Identifier = model.Email,
						Email = model.Email,
						PasswordSalt = passwordSalt,
						Password = PasswordHelper.StrToByteArray(PasswordHelper.CreatePasswordHash(model.Password, passwordSalt))
					};
				}
				else
				{
					//Facebook User.. need to validate the token etc
					var facebookUser = await _generalHelper.VerifyFacebookTokenAsync(model.FacebookToken);

					userDb = new User
					{
						Client = clientDb,
						CredentialTypeId = (byte)CredentialTypeEnum.Facebook,
						Identifier = facebookUser.Id,
						FirstName = facebookUser.First_Name,
						LastName = facebookUser.Last_Name,
						Email = facebookUser.Email,
						PasswordSalt = passwordSalt,
					};
				}

				var userUuid = await _userRepository.SaveUserAsync(userDb);

				//Return the user details and token etc
				var user = new UserAuthenticatedResult()
				{
					Id = userUuid,
					FirstName = model.FirstName,
					LastName = model.LastName,
					Email = model.Email,
					TokenDetails = new TokenDetails()
					{
						Token = passwordSalt
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

		// POST api/v1/clients/{id}/users>
		[Route("{clientUuid}/users/authenticate")]
		[ResponseType(typeof(UserAuthenticatedResult))]
		[HttpPost]
		public async Task<IHttpActionResult> AuthenticateUser(string clientUuid, UserAuthenticateModel model)
		{
			User userDb;

			if (!string.IsNullOrEmpty(model.FacebookToken))
			{
				//Facebook User.. need to validate the token etc
				var facebookUser = await _generalHelper.VerifyFacebookTokenAsync(model.FacebookToken);

				userDb = await _userRepository.GetClientUserByFacebookIdAsync(new Guid(clientUuid), facebookUser.Id);
			}
			else
			{
				userDb = await _userRepository.GetClientUserByEmailAsync(new Guid(clientUuid), model.Email);
			}		

			if (userDb == null)
			{
				return BadRequest("Authentication Failed");
			}

			var dBytes = userDb.Password;
			var enc = new UTF8Encoding();
			var length = dBytes.TakeWhile(b => b != 0).Count();
			var strDbPassword = enc.GetString(dBytes, 0, length);

			if (strDbPassword != PasswordHelper.CreatePasswordHash(model.Password, userDb.PasswordSalt))
			{
				return BadRequest("Authentication Failed");
			}

			//Return the user details and token etc
			var userAuthenticated = new UserAuthenticatedResult()
			{
				Id = userDb.UserUUID.ToString(),
				FirstName = userDb.FirstName,
				LastName = userDb.LastName,
				Email = userDb.Email,
				TokenDetails = new TokenDetails()
				{
					Token = userDb.PasswordSalt
				}
			};

			return Ok(userAuthenticated);
		}
	}
}