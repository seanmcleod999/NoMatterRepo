using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NoMatterDatabaseModel;
using NoMatterWebApi.ActionResults;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Enums;
using NoMatterWebApi.Extensions;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApi.Models;
using NoMatterWebApiModels.Enums;
using NoMatterWebApiModels.Models;
using PrettyDamnThriftyWeb.Mailers;
using Order = NoMatterWebApiModels.Models.Order;

namespace NoMatterWebApi.Controllers.V1
{
	//[RoutePrefix("api/v1/orders")]
	public class OrderController : ApiController
	{
		
		private IOrderRepository _orderRepository;
		private IClientRepository _clientRepository;
		private IGlobalSettings _globalSettings;
		

		public OrderController()
		{
			var databaseEntity = new DatabaseEntities();

			_orderRepository = new OrderRepository(databaseEntity);
			_clientRepository = new ClientRepository(databaseEntity);
			_globalSettings = new GlobalSettings();
			
			
		}

		public OrderController(IOrderRepository orderRepository, IClientRepository clientRepository, IGlobalSettings globalSettings)
		{
			_clientRepository = clientRepository;
			_orderRepository = orderRepository;
			_globalSettings = globalSettings;
		}


		[Route("api/v1/clients/{clientId}/orders/{orderId}")]
		[ResponseType(typeof(Order))]
		public async Task<IHttpActionResult> GetOrder(int orderId)
		{
			try
			{
				var orderDb = await _orderRepository.GetOrderAsync(orderId);

				if (orderDb == null) return new CustomBadRequest(Request, ApiResultCode.OrderNotFound);

				var order = orderDb.ToDomainOrder();

				return Ok(order);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}

		}


		[Route("api/v1/clients/{clientId}/orders/{orderId}/processeft")]
		[ResponseType(typeof(Order))]
		public async Task<IHttpActionResult> ProcessEftOrder(string clientId, int orderId, ProcessEftOrderModel model)
		{
			try
			{
				var orderDb = await _orderRepository.GetOrderAsync(orderId);

				if (orderDb == null) return new CustomBadRequest(Request, ApiResultCode.OrderNotFound);

				//if (orderDb.User.ClientId == null) throw new Exception("UserDoesNotBelongToAClient");

				//Update the paymentType for the order
				await _orderRepository.UpdateOrderPaymentTypeAsync(orderDb, (short)PaymentTypeEnum.EFT);

				var order = orderDb.ToDomainOrder();

		
				//Need to get the clients bank details
				var bankDetails = await ClientHelper.GetClientBankDetails(_clientRepository, orderDb.User.ClientId.Value);
				if (string.IsNullOrEmpty(bankDetails.AccountName) || 
					string.IsNullOrEmpty(bankDetails.BankName) || 
					string.IsNullOrEmpty(bankDetails.BranchName) ||
					string.IsNullOrEmpty(bankDetails.BranchNumber)) return new CustomBadRequest(Request, ApiResultCode.BankDetailsIncomplete);

				var salesEmailAddress = await ClientHelper.GetClientSalesEmailAddress(_clientRepository, orderDb.User.ClientId.Value);
				if (string.IsNullOrEmpty(salesEmailAddress)) return new CustomBadRequest(Request, ApiResultCode.SalesEmailAddressNotDefined);

				var clientSiteFriendlyName = await ClientHelper.GetClientSiteFriendlyName(_clientRepository, orderDb.User.ClientId.Value);
				if (string.IsNullOrEmpty(clientSiteFriendlyName)) return new CustomBadRequest(Request, ApiResultCode.ClientSiteFriendlyNameNotDefined);

				//Send EFT Emails
				var mailer = new ApiMailer();

				//Send an EFT Related email to the user
				mailer.ConfirmEftOrder(clientSiteFriendlyName, order, bankDetails, salesEmailAddress).Send();

				//Send an email to the administrator
				//mailer.CustomerOrder(orderResult, salesEmailAddress).Send();

				return Ok(order);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}

		}

		[Route("api/v1/clients/{clientId}/orders/{orderId}/updatepaymenttype")]
		[ResponseType(typeof (Order))]
		public async Task<IHttpActionResult> UpdateOrderPaymentType(string clientId, int orderId, UpdateOrderPaymentTypeModel model)
		{
			try
			{
				var orderDb = await _orderRepository.GetOrderAsync(orderId);

				if (orderDb == null) return new CustomBadRequest(Request, ApiResultCode.OrderNotFound);

				//Update the paymentType for the order
				await _orderRepository.UpdateOrderPaymentTypeAsync(orderDb, model.PaymentTypeId);

				var order = orderDb.ToDomainOrder();

				return Ok(order);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		[Route("api/v1/clients/{clientId}/orders/{orderId}/paid")]
		[HttpGet]
		public async Task<IHttpActionResult> ProcessPaidOrder(int orderId)
		{
			try
			{
				//await _orderRepository.UpdateOrderPaid(orderId);

				//await _orderRepository.UpdateOrderProductsAsSold(orderId);

				return Ok();
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}

		}

	}
}