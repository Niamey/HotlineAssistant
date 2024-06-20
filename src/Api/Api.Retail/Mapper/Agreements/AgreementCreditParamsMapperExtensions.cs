using Vostok.Hotline.Api.Retail.Models.Agreements;
using Vostok.Hotline.Api.Retail.Responses.Agreements;
using Vostok.Hotline.Api.Retail.Responses.Agreements.Enums;
using Vostok.Hotline.Core.Common.Enums.Agreements;

namespace Vostok.Hotline.Api.Retail.Mapper.Agreements
{
	internal static class AgreementCreditParamsMapperExtensions
	{
		public static AgreementCreditParamsApiModel ToAgreementCreditParamsApiModel(this AgreementCreditParamsResponseModel response)
		{
			var result = new AgreementCreditParamsApiModel()
			{
				Period = response.Period.ToCreditPeriodTypeRetailEnum(),
				MandatoryPayment = response.MandatoryPayment,
				MandatoryDate = response.MandatoryDate,
				PreferentialPayment = response.PreferentialPayment,
				Debt = response.Debt,
				OverdueLoan = response.OverdueLoan,
				Interests = response.Interests,
				Overdraft = response.Overdraft,
				Penalty = response.Penalty,
				Fee = response.Fee,
				IsVacationPeriod = response.IsVacationPeriod,
				VacationPeriodEnd = response.VacationPeriodEnd,
				DebtPercent = response.DebtPercent
			};

			return result;
		}

		public static AgreementCreditPeriodTypeEnum ToCreditPeriodTypeRetailEnum(this AgreementCreditPeriodTypeRetailEnum? response)
		=> response switch
		{
			null => AgreementCreditPeriodTypeEnum.NotUsed,
			AgreementCreditPeriodTypeRetailEnum.NotUsed => AgreementCreditPeriodTypeEnum.NotUsed,
			AgreementCreditPeriodTypeRetailEnum.NotInGracePeriodWithDebt => AgreementCreditPeriodTypeEnum.NotInGracePeriodWithDebt,
			AgreementCreditPeriodTypeRetailEnum.NotInGracePeriod => AgreementCreditPeriodTypeEnum.NotInGracePeriod,
			AgreementCreditPeriodTypeRetailEnum.InGracePeriodFirstMonth => AgreementCreditPeriodTypeEnum.InGracePeriodFirstMonth,
			AgreementCreditPeriodTypeRetailEnum.InGracePeriodSecondMonth => AgreementCreditPeriodTypeEnum.InGracePeriodSecondMonth,
			_ => AgreementCreditPeriodTypeEnum.NotUsed
		};
	}
}
