﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoMatterWebApiModels.Models;

namespace WebApplication7.ViewModels
{
	public class ViewProductVm
	{
		public string FromCategoryId { get; set; }

		public Product Product { get; set; }
	}
}