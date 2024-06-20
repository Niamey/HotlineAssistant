using Vostok.Hotline.Api.Retail.Models.Agreements;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Mapper
{
	public static class CardDebtToBankMapperExtensions
	{
		public static CardDebtToBankViewModel ToCardDebtToBankViewModel(this AgreementCreditParamsApiModel response)
		{
			var result = new CardDebtToBankViewModel
			{
				Period = response.Period,
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
	}
}
