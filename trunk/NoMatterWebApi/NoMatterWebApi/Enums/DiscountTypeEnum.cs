using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMatterWebApi.Enums
{
	public enum DiscountTypeEnum : byte
	{
		FreeShipping = 1,
		FixedAmount = 2,
		Percentage = 3

	}
}