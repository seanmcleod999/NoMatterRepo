using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiWebHelper.Exceptions
{

	public class WebApiException : Exception
	{
		//public string RemoteResultCode { get; private set; }
		//public bool HasExplicitMessage { get; private set; }

		public WebApiException(string errorMessage, HttpResponseMessage response) :
			base("ErrorMessage: " + errorMessage + ". StatusCode: " + response.StatusCode + ". Reason: " + response.ReasonPhrase)
		{
			//ResultCode = resultCode;
			//HasExplicitMessage = false;
		}

		//public WebApiException(PitchInResultCode resultCode, string errorText, string remoteResultCode = null) :
		//	base(string.Format("Result code: {0:g}, Message: {1}", resultCode, errorText))
		//{
		//	ResultCode = resultCode;
		//	RemoteResultCode = remoteResultCode;
		//	HasExplicitMessage = true;
		//}

	}
}
