using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApi.ActionResults
{
	public class CustomBadRequest : IHttpActionResult
	{

		private readonly HttpRequestMessage _request;
		private readonly ApiResultCode _result;

		public CustomBadRequest(HttpRequestMessage request, ApiResultCode result)
		{
			_request = request;
			_result = result;
		}

		public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			var errorModel = new WebApiErrorModel()
			{
				Code = (int)_result,
				Text = _result.ToString()
			};

			var response = _request.CreateResponse(HttpStatusCode.BadRequest, errorModel);
			return Task.FromResult(response);
		}

		public ApiResultCode Result
		{
			get
			{
				return _result;
			}

		}
	}
}