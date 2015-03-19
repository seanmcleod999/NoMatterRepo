using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApi
{
	public enum ApiResultCode : int
	{
		Success = 0,
		ParametersNotSpecified = 1,
		ParametersNotValid = 2,
		InvalidDate = 3,

		AuthenticationFailed = 10,
		ClientNotFound = 11,
		UserAlreadyExists = 12,
		UserNotFound = 13,
		UserDoesNotBelongToClient = 14,
		SectionNotFound = 15,
		ProductNotFound = 16,
		OrderNotFound = 17,
		CategoryNotFound = 18,
		ClientPageNotFound = 19,
		ClientSettingNotFound = 20

	}
}