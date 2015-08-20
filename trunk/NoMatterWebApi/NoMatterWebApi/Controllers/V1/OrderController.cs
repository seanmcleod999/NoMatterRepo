using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NoMatterDataLibrary;
using NoMatterWebApi.ActionResults;
using NoMatterWebApi.Enums;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApi.Models;
using NoMatterWebApiModels.Enums;
using NoMatterWebApiModels.Models;
using Order = NoMatterWebApiModels.Models.Order;

namespace NoMatterWebApi.Controllers.V1
{
	//[RoutePrefix("api/v1/orders")]
	public class OrderController : ApiController
	{
		
		private IOrderRepository _orderRepository;
		private IClientRepository _clientRepository;
		private IGlobalRepository _globalRepository;
		private IEmailHelper _emailHelper;
		
		public OrderController()
		{

			_orderRepository = new OrderRepository();
			_clientRepository = new ClientRepository();
			_globalRepository = new GlobalRepository();
			_emailHelper = new EmailHelper();		
		}

		public OrderController(IOrderRepository orderRepository, IClientRepository clientRepository, IEmailHelper emailHelper, IGlobalRepository globalRepository)
		{
			_clientRepository = clientRepository;
			_orderRepository = orderRepository;
			_globalRepository = globalRepository;
			_emailHelper = emailHelper;
		}


		[Route("api/v1/clients/{clientId}/orders/{orderId}")]
		[ResponseType(typeof(Order))]
		public async Task<IHttpActionResult> GetOrder(int orderId)
		{
			try
			{
				var order = await _orderRepository.GetOrderAsync(orderId);

				if (order == null) return new CustomBadRequest(Request, ApiResultCode.OrderNotFound);

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
				var clientDb = await _clientRepository.GetClientAsync(new Guid(clientId));
				if (clientDb == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				var order = await _orderRepository.GetOrderAsync(orderId);
				if (order == null) return new CustomBadRequest(Request, ApiResultCode.OrderNotFound);

				if (order.User.Client.ClientUuid == null) return new CustomBadRequest(Request, ApiResultCode.UserDoesNotBelongToAClient);

				//Update the paymentType for the order
				await _orderRepository.UpdateOrderPaymentTypeAsync(order, (short)PaymentTypeEnum.EFT);


				//Send the emails
				await _emailHelper.SendCustomerEftOrderEmail(new Guid(clientDb.ClientUuid), order, _clientRepository, _globalRepository);

				await _emailHelper.ClientOrder(new Guid(clientDb.ClientUuid), order, _clientRepository, _globalRepository);

				return Ok(order);

			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}

		}

		[HttpPost]
		[Route("api/v1/clients/{clientId}/orders/{orderId}/updatepaymenttype")]
		[ResponseType(typeof (Order))]
		public async Task<IHttpActionResult> UpdateOrderPaymentType(string clientId, int orderId, UpdateOrderPaymentTypeModel model)
		{
			try
			{
				var order = await _orderRepository.GetOrderAsync(orderId);

				if (order == null) return new CustomBadRequest(Request, ApiResultCode.OrderNotFound);

				//Update the paymentType for the order
				await _orderRepository.UpdateOrderPaymentTypeAsync(order, model.PaymentTypeId);

				//var order = orderDb;

				return Ok(order);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		[Route("api/v1/clients/{clientId}/orders/{orderId}/paid")]
		[HttpPost]
		public async Task<IHttpActionResult> ProcessPaidOrder(string clientId, int orderId, ProcessPaidOrderModel model)
		{
			try
			{
				var client = await _clientRepository.GetClientAsync(new Guid(clientId));
				if (client == null) return new CustomBadRequest(Request, ApiResultCode.ClientNotFound);

				var order = await _orderRepository.GetOrderAsync(orderId);

				await _orderRepository.UpdateOrderPaid(order, true);

				await _orderRepository.UpdateOrderProductsAsSold(order);

				await _emailHelper.SendCustomerPaidOrderEmail(new Guid(client.ClientUuid), order, _clientRepository, _globalRepository);

				await _emailHelper.ClientOrder(new Guid(client.ClientUuid), order, _clientRepository, _globalRepository);

				return Ok();
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		[Route("api/v1/clients/{clientId}/orders/{orderId}/failed")]
		[HttpPost]
		public async Task<IHttpActionResult> ProcessFailedOrder(string clientId, int orderId, ProcessFailedOrderModel model)
		{
			try
			{
				var orderDb = await _orderRepository.GetOrderAsync(orderId);

				await _orderRepository.UpdateOrderFailed(orderDb);

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