using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Core.Common.Enums.Agreements;

namespace Vostok.Hotline.Gateway.Client.Agreements.Mapper
{
	internal static class MoneyBoxStatusMapperExtensions
	{
		public static AgreementMoneyBoxStatusEnum ToAgreementMoneyBoxStatusEnum(this MoneyBoxStatusEnum response)
			=> response switch
			{
				MoneyBoxStatusEnum.Active => AgreementMoneyBoxStatusEnum.Active,
				MoneyBoxStatusEnum.Inactive => AgreementMoneyBoxStatusEnum.Inactive,
				_ => AgreementMoneyBoxStatusEnum.Undefined
			};
	}
}
