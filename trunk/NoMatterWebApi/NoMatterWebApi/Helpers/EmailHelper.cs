using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NoMatterDataLibrary;
using NoMatterDataLibrary.Enums;
using NoMatterWebApi.Enums;
using NoMatterWebApi.Mailers;
using Order = NoMatterWebApiModels.Models.Order;

namespace NoMatterWebApi.Helpers
{

	public interface IEmailHelper
	{
		Task SendCustomerEftOrderEmail(Guid clientUuid, Order order, IClientRepository clientRepository, IGlobalRepository globalRespository);
		Task SendCustomerPaidOrderEmail(Guid clientUuid, Order order, IClientRepository clientRepository, IGlobalRepository globalRespository);
		Task ClientOrder(Guid clientUuid, Order order, IClientRepository clientRepository, IGlobalRepository globalRespository);

	}


	public class EmailHelper : IEmailHelper
	{
		private ApiMailer mailer = new ApiMailer();

		public async Task SendCustomerEftOrderEmail(Guid clientUuid, Order order, IClientRepository clientRepository, IGlobalRepository globalRespository)
		{
			//Need to get the clients bank details
			var bankDetails = await ClientHelper.GetClientBankDetails(clientRepository, clientUuid);

			//if (string.IsNullOrEmpty(bankDetails.AccountName) ||
			//	string.IsNullOrEmpty(bankDetails.BankName) ||
			//	string.IsNullOrEmpty(bankDetails.BranchName) ||
			//	string.IsNullOrEmpty(bankDetails.BranchNumber)) return new ApiException(Request, ApiResultCode.BankDetailsIncomplete);

			var salesEmailAddress = await clientRepository.GetClientStringSettingsAsync(clientUuid, SettingEnum.SalesEmailAddress);
			//if (string.IsNullOrEmpty(salesEmailAddress)) return new CustomBadRequest(Request, ApiResultCode.SalesEmailAddressNotDefined);

			var clientSiteFriendlyName = await clientRepository.GetClientStringSettingsAsync(clientUuid, SettingEnum.SiteNameFriendly);
			//if (string.IsNullOrEmpty(clientSiteFriendlyName)) return new CustomBadRequest(Request, ApiResultCode.ClientSiteFriendlyNameNotDefined);

			Exception exception = null;

			try
			{
				//Send an EFT Related email to the user
				await mailer.CustomerEftOrder(clientUuid.ToString(), clientSiteFriendlyName, order, bankDetails, salesEmailAddress).SendAsync();

				//Log successful email
				await LogEmail(clientUuid, (short)EmailEventEnum.CustomerEftOrder, true, null, globalRespository);
			}
			catch (Exception ex)
			{
				exception = ex;
			}

			if (exception != null)
			{
				await LogEmail(clientUuid, (short)EmailEventEnum.ClientOrder, false, exception.ToString(), globalRespository);
			}
		
		}

		public async Task SendCustomerPaidOrderEmail(Guid clientUuid, Order order, IClientRepository clientRepository, IGlobalRepository globalRespository)
		{

			var salesEmailAddress = await clientRepository.GetClientStringSettingsAsync(clientUuid, SettingEnum.SalesEmailAddress);
			//if (string.IsNullOrEmpty(salesEmailAddress)) return new CustomBadRequest(Request, ApiResultCode.SalesEmailAddressNotDefined);

			var clientSiteFriendlyName = await clientRepository.GetClientStringSettingsAsync(clientUuid, SettingEnum.SiteNameFriendly);
			//if (string.IsNullOrEmpty(clientSiteFriendlyName)) return new CustomBadRequest(Request, ApiResultCode.ClientSiteFriendlyNameNotDefined);

			Exception exception = null;

			try
			{
				//Send an EFT Related email to the user
				await mailer.CustomerPaidOrder(clientUuid.ToString(), clientSiteFriendlyName, order, salesEmailAddress).SendAsync();

				//Log successful email
				await LogEmail(clientUuid, (short)EmailEventEnum.CustomerEftOrder, true, null, globalRespository);
			}
			catch (Exception ex)
			{
				exception = ex;
			}

			if (exception != null)
			{
				await LogEmail(clientUuid, (short)EmailEventEnum.ClientOrder, false, exception.ToString(), globalRespository);
			}

		}

		public async Task ClientOrder(Guid clientUuid, Order order, IClientRepository clientRepository, IGlobalRepository globalRespository)
		{
			var salesEmailAddress = await clientRepository.GetClientStringSettingsAsync(clientUuid, SettingEnum.SalesEmailAddress);
			//if (string.IsNullOrEmpty(salesEmailAddress)) return new CustomBadRequest(Request, ApiResultCode.SalesEmailAddressNotDefined);

			var clientSiteFriendlyName = await clientRepository.GetClientStringSettingsAsync(clientUuid, SettingEnum.SiteNameFriendly);
			//if

			Exception exception = null;

			try
			{
				//Send an EFT Related email to the user
				await mailer.ClientOrder(clientUuid.ToString(), clientSiteFriendlyName, order, salesEmailAddress).SendAsync();

				//Log successful email
				await LogEmail(clientUuid, (short)EmailEventEnum.ClientOrder, true, null, globalRespository);
			}
			catch (Exception ex)
			{
				exception = ex;
			}

			if (exception != null)
			{
				await LogEmail(clientUuid, (short)EmailEventEnum.ClientOrder, false, exception.ToString(), globalRespository);
			}
		}

		private async Task LogEmail(Guid clientUuid, short emailEventId, bool success, string errorMessage, IGlobalRepository globalRespository)
		{
			//var clientEmail = new ClientEmail();

			//clientEmail.ClientID = clientId;
			//clientEmail.EmailEventId = emailEventId;
			//clientEmail.Success = success;
			//clientEmail.ErrorMessage = errorMessage;

			//await globalRespository.InsertClientEmailSendLog(clientEmail);
		}
	}
}