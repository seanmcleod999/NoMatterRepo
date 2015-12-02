using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class User
	{
		public Client Client { get; set; }

		public int UserId { get; set; }

		public string UserUuid { get; set; }

		public byte CredentialTypeId { get; set; }

		public byte[] Password { get; set; }

		public string Email { get; set; }

		public string Identifier { get; set; }

		public string Fullname { get; set; }

		public string ContactNumber { get; set; }

		public string Address { get; set; }

		public string Suburb { get; set; }
	
		public string City { get; set; }

		public string Province { get; set; }

		public string Country { get; set; }

		public string PostalCode { get; set; }

		public string UserRolesString { get; set; }

	}
}
