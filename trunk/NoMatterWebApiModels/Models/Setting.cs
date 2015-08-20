using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMatterWebApiModels.Models
{
	public class Setting
	{
		public short SettingId { get; set; }

		[Required(ErrorMessage = "required")]
		[StringLength(50, ErrorMessage = "Must be under 50 characters")]
		public string SettingName { get; set; }

		public string SettingDescription { get; set; }

		public string RegexValidation { get; set; }

		public byte SettingTypeId { get; set; }

		public string SettingType { get; set; }

		public short SettingCategoryId { get; set; }

		public string SettingCategory { get; set; }
	}
}
