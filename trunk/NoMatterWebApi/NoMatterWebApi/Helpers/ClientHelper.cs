using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NoMatterDataLibrary;
using NoMatterDataLibrary.Enums;
using NoMatterWebApi.Enums;
using NoMatterWebApi.Models;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApi.Helpers
{
	public class ClientHelper
	{
		public static async Task<BankDetails> GetClientBankDetails(IClientRepository clientRepository, Guid clientUuid)
		{
			var bankDetails = new BankDetails
				{
					AccountName = await clientRepository.GetClientStringSettingsAsync(clientUuid, SettingEnum.AccountName),
					BankName = await clientRepository.GetClientStringSettingsAsync(clientUuid, SettingEnum.BankName),
					BranchName = await clientRepository.GetClientStringSettingsAsync(clientUuid, SettingEnum.BranchName),
					BranchNumber = await clientRepository.GetClientStringSettingsAsync(clientUuid, SettingEnum.BranchNumber),
					AccountNumber = await clientRepository.GetClientStringSettingsAsync(clientUuid, SettingEnum.AccountNumber),
				};

			return bankDetails;
		}

		public static async Task<string> GetClientSalesEmailAddress(IClientRepository clientRepository, Guid clientUuid)
		{
			return await clientRepository.GetClientStringSettingsAsync(clientUuid, SettingEnum.SalesEmailAddress);
		}

		public static async Task<string> GetClientSiteFriendlyName(IClientRepository clientRepository, Guid clientUuid)
		{
			return await clientRepository.GetClientStringSettingsAsync(clientUuid, SettingEnum.SiteNameFriendly);
		}
	}
}