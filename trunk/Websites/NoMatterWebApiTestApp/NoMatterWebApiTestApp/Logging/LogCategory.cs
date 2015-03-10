using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Logging
{
	[Flags]
	public enum LogCategory
	{
		General = 1,
		Debug = 2,
		Trace = 4,
		Security = 8,
	}
}