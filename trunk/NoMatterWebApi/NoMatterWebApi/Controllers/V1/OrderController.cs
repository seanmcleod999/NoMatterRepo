using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NoMatterDatabaseModel;
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
		private IGeneralHelper _generalHelper;
		

		public OrderController()
		{
			var databaseEntity = new DatabaseEntities();

			_orderRepository = new OrderRepository(databaseEntity);
			_generalHelper = new GeneralHelper();
			
			
		}

		public OrderController(IOrderRepository orderRepository, IGeneralHelper generalHelper)
		{
			_orderRepository = orderRepository;
			_generalHelper = generalHelper;
		}

		// GET api/v1/orders/{orderId}
		[Route("{orderId}")]
		[ResponseType(typeof(ShoppingCartDetails))]
		public async Task<IHttpActionResult> GetOrder(string orderId)
		{
			try
			{
				var orderDb = await _orderRepository.GetOrderAsync(new Guid(orderId));

				var order = orderDb.ToDomainOrder();

				return Ok(order);
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}

		}

	}
}