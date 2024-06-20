using Vostok.Hotline.Api.Retail.Models.Agreements;
using Vostok.Hotline.Api.Retail.Responses.Agreements;

namespace Vostok.Hotline.Api.Retail.Mapper.Agreements
{
	internal static class AgreementBalanceMapper
	{
		public static AgreementBalanceApiModel MapToAgreementBalanceApiModel(this AgreementBalanceResponseModel response)
		{
			var result = new AgreementBalanceApiModel
			{
				Available = response.Available,
				Date = response.Date,
				CurrencyCode = response.CurrencyCode,
				AgreementId = response.AgreementId,
				Blocked = response.Blocked,
				CreditLimit = response.CreditLimit,
				FinBlocking = response.FinBlocking,
				Interests = response.Interests,
				Loan = response.Loan,
				Overdue = response.Overdue,
				Overlimit = response.Overlimit,
				Own = response.Own,
				Penalty = response.Penalty,
				Debt = response.Debt,
				VacationPeriodEnd = response.VacationPeriodEnd,
				UnusedCreditLimit = response.UnusedCreditLimit,
				PreferentialPayment = response.PreferentialPayment,
				Period = response.Period,
				MaxCreditLimit = response.MaxCreditLimit,
				MandatoryPayment = response.MandatoryPayment,
				MandatoryDate = response.MandatoryDate,
				IsVacationPeriod = response.IsVacationPeriod,
				FullOwn = response.FullOwn,
				Fee = response.Fee,
				DebtPercent = response.DebtPercent,
				MinCreditLimit = response.MinCreditLimit,
				MinimalPayment = response.MinimalPayment,
				MinimumBalance = response.MinimumBalance,
				Overdraft = response.Overdraft
			};
			return result;
		}
	}
}
