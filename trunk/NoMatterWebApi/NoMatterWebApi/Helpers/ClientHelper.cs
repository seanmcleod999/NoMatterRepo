using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NoMatterWebApi.DAL;
using NoMatterWebApi.Enums;
using NoMatterWebApi.Models;
using NoMatterWebApiModels.Models;

namespace NoMatterWebApi.Helpers
{
	public class ClientHelper
	{
		public static async Task<BankDetails> GetClientBankDetails(IClientRepository clientRepository, int clientId)
		{
			var bankDetails = new BankDetails
				{
					AccountName =  await clientRepository.GetClientStringSettingsAsync(clientId, SettingEnum.AccountName),
					BankName = await clientRepository.GetClientStringSettingsAsync(clientId, SettingEnum.BankName),
					BranchName = await clientRepository.GetClientStringSettingsAsync(clientId, SettingEnum.BranchName),
					BranchNumber = await clientRepository.GetClientStringSettingsAsync(clientId, SettingEnum.BranchNumber),
					AccountNumber = await clientRepository.GetClientStringSettingsAsync(clientId, SettingEnum.AccountNumber),
				};

			return bankDetails;
		}

		public static async Task<string> GetClientSalesEmailAddress(IClientRepository clientRepository, int clientId)
		{
			return await clientRepository.GetClientStringSettingsAsync(clientId, SettingEnum.SalesEmailAddress);
		}

		public static async Task<string> GetClientSiteFriendlyName(IClientRepository clientRepository, int clientId)
		{
			return await clientRepository.GetClientStringSettingsAsync(clientId, SettingEnum.SiteNameFriendly);
		}
	}
}