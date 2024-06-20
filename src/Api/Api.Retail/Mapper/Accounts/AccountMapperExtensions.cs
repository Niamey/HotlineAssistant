using System.Collections.Generic;
using Vostok.Hotline.Api.Retail.Models;
using Vostok.Hotline.Api.Retail.Responses.Accounts;

namespace Vostok.Hotline.Api.Retail.Mapper.Accounts
{
	internal static class AccountMapperExtensions
	{
		public static AccountApiModel ToAccountApiModel(this AccountResponseModel response)
		{
			var result = new AccountApiModel
			{
				AccountId = response.Id, 				 
				AgreementId = response.AgreementId,
				SolarId = response.ClientId,
				CurrencyCode = response.CurrencyCode,
				Number = response.Number,
				Status = response.Status.ToAccountStatusEnum(),
				Type = response.Type.ToAccountTypeEnum()
			};

			return result;
		}

		public static AccountCollectionApiModel ToAccountCollectionApiModel(this AccountCollectionResponseModel respose)
		{
			var result = new AccountCollectionApiModel();

			foreach (var row in respose)
			{
				result.Add(row.ToAccountApiModel());
			}

			return result;
		}
	}
}