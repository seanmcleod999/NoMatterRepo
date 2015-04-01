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
using NoMatterWebApi.Extensions;
using NoMatterWebApi.Helpers;
using NoMatterWebApi.Logging;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApi.Controllers.V1
{
	[RoutePrefix("api/v1/orders")]
	public class OrderController : ApiController
	{
		
		private IOrderRepository _orderRepository;
		private IGlobalSettings _globalSettings;
		

		public OrderController()
		{
			var databaseEntity = new DatabaseEntities();

			_orderRepository = new OrderRepository(databaseEntity);
			_globalSettings = new GlobalSettings();
			
			
		}

		public OrderController(IOrderRepository orderRepository, IGlobalSettings globalSettings)
		{
			_orderRepository = orderRepository;
			_globalSettings = globalSettings;
		}

		// GET api/v1/orders/{orderId}
		[Route("{orderId}")]
		[ResponseType(typeof(ShoppingCartDetails))]
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

		// GET api/v1/orders/{orderId}/paid
		[Route("{orderId}/paid")]
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