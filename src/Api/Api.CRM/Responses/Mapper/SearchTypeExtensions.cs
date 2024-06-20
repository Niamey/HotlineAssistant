using System;
using Vostok.Hotline.Api.CRM.Enums;

namespace Vostok.Hotline.Api.CRM.Responses.Enums.Mapper
{
	internal static class SearchTypeExtensions
	{
		public static CounterpartSearchCodeCrmEnum ToCounterpartSearchCodeEnum(this SearchTypeEnum searchType)
		{
			return searchType switch
			{
				SearchTypeEnum.AgreementNumber => CounterpartSearchCodeCrmEnum.AGREEMENT_NUMBER,
				SearchTypeEnum.Email => CounterpartSearchCodeCrmEnum.EMAIL,
				SearchTypeEnum.FinancePhone => CounterpartSearchCodeCrmEnum.P_FINANCIALPHONE,
				SearchTypeEnum.FullName => CounterpartSearchCodeCrmEnum.FULLNAME,
				SearchTypeEnum.IBAN => CounterpartSearchCodeCrmEnum.IBAN,
				SearchTypeEnum.INN => CounterpartSearchCodeCrmEnum.IDENTCODE,
				SearchTypeEnum.Passport => CounterpartSearchCodeCrmEnum.P_PNUMBER,
				SearchTypeEnum.PersonId => CounterpartSearchCodeCrmEnum.PERSON_ID,
				SearchTypeEnum.PlasticCard => CounterpartSearchCodeCrmEnum.CARD,
				SearchTypeEnum.SolarId => CounterpartSearchCodeCrmEnum.SOLAR_ID,
				SearchTypeEnum.SrBankId => CounterpartSearchCodeCrmEnum.SR_ID,
				_ => throw new NotSupportedException()
			};
		}
	}
}
