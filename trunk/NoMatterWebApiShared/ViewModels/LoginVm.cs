using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApiModels.ViewModels
{

	public class LoginVm
	{
		public bool FromCart { get; set; }
		public LoginModel LoginModel { get; set; }
		public RegisterUserModel RegisterUserModel { get; set; }
		public string ReturnUrl { get; set; }
	}
}
