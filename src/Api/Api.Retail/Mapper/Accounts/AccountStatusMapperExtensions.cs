using System;
using Vostok.Hotline.Api.Retail.Responses.Accounts.Enums;
using Vostok.Hotline.Core.Common.Enums.Accounts;

namespace Vostok.Hotline.Api.Retail.Mapper.Accounts
{
	internal static class AccountStatusMapperExtensions
	{
		public static AccountStatusEnum ToAccountStatusEnum(this AccountStatusRetailEnum response)
			=> response switch
			{
				AccountStatusRetailEnum.Active => AccountStatusEnum.Active,
				AccountStatusRetailEnum.Inactive => AccountStatusEnum.Inactive,
				_ => AccountStatusEnum.Undefined
			};
	}
}
