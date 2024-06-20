using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Api.Retail.Responses.Cards;

namespace Vostok.Hotline.Api.Retail.Mapper.Cards
{
	internal static class CardBalanceMapper
	{
		public static CardBalanceApiModel MapToCardBalanceApiModel(this CardBalanceResponseModel response)
		{
			var result = new CardBalanceApiModel
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
				MinimumBalance = response.MinimumBalance,
				Period = response.Period,
				PreferentialPayment = response.PreferentialPayment,
				UnusedCreditLimit = response.UnusedCreditLimit,
				VacationPeriodEnd = response.VacationPeriodEnd,
				Debt = response.Debt,
				Overdraft = response.Overdraft,
				MinimalPayment = response.MinimalPayment,
				MinCreditLimit = response.MinCreditLimit,
				DebtPercent = response.DebtPercent,
				Fee = response.Fee,
				FullOwn = response.FullOwn,
				IsVacationPeriod = response.IsVacationPeriod,
				MandatoryDate = response.MandatoryDate,
				MandatoryPayment = response.MandatoryPayment,
				MaxCreditLimit = response.MaxCreditLimit
			};

			return result;
		}
	}
}