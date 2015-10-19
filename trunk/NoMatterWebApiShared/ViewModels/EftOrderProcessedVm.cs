using NoMatterWebApiModels.Models;

namespace NoMatterWebApiModels.ViewModels
{
	public class EftOrderProcessedVm
	{
		public Order Order { get; set; }

		public BankDetails BankDetails { get; set; }
	}
}
