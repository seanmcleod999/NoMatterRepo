using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using NoMatterWebApiWebHelper;
using RedOrange.Models;

namespace RedOrange.Controllers
{
	public class WebApiController : Controller
	{
		protected void HandleBadRequest(ApiException apiException)
        {
            if (apiException.StatusCode == HttpStatusCode.BadRequest)
            {
                //There are, at the moment, two types of bad requests: when signing in, JsonData might contain an error and error_description field.
                //When returning a BadRequest from the Web API, we'll get a message and ModelState error dictionary back.

                var badRequestData = JsonConvert.DeserializeObject<JsonBadRequest>(apiException.JsonData);
                
                if (badRequestData.ModelState != null)
                {
                    foreach (var modelStateItem in badRequestData.ModelState)
                    {
                        foreach (var message in modelStateItem.Value)
                        {
                            ModelState.AddModelError(modelStateItem.Key, message);
                        }
                    }
                }

                //When an error occurs while signing in, Error equals "invalid_grant" and ErrorDescription will contain more detail.
                //This error is being set in the Web API project in the YetAnotherTodo.WebApi.Providers.ApplicationOAuthProvider class
                if (string.Equals(badRequestData.Error, "invalid_grant"))
                {
                    ModelState.AddModelError("", badRequestData.ErrorDescription);
                }
            }
        }
    }
	
}