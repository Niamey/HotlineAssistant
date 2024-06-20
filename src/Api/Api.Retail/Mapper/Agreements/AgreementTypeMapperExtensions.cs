using System;
using Vostok.Hotline.Api.Retail.Responses.Agreements.Enums;
using Vostok.Hotline.Core.Common.Enums.Agreements;

namespace Vostok.Hotline.Api.Retail.Mapper.Agreements
{
	internal static class AgreementTypeMapperExtensions
	{
		public static AgreementTypeEnum ToAgreementTypeEnum(this AgreementTypeRetailEnum response)
		=> response switch
		{
			AgreementTypeRetailEnum.CardAccount => AgreementTypeEnum.CardAccount,
			_ => AgreementTypeEnum.Undefined
		};
	}
}
