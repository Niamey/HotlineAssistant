using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.References.Managers;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Mapper
{
	public class CardBalanceMapper
	{
		private readonly CurrencyApiManager _currencyManager;

		public CardBalanceMapper(CurrencyApiManager currencyManager)
		{
			_currencyManager = currencyManager;
		}

		public async Task<CardBalanceViewModel> MapToCardBalanceViewModelAsync(CardBalanceApiModel response, CancellationToken cancellationToken)
		{
			var result = new CardBalanceViewModel
			{
				Available = response.Available,
				Date = response.Date
			};

			result.Currency = await _currencyManager.GetCurrencyShortNameAsync(response.CurrencyCode, cancellationToken);
			return result;
		}

		public async Task<CardExtendedBalanceViewModel> MapToCardExtendedBalanceViewModelAsync(CardBalanceApiModel response, CancellationToken cancellationToken)
		{
			var result = new CardExtendedBalanceViewModel
			{
				Available = response.Available,
				Date = response.Date,
				HasCreditLimit = response.CreditLimit > 0,
				CreditLimit = response.CreditLimit,
				UsedCreditLimit = response.Loan,
				MinimumBalance = response.MinimumBalance
			};

			result.Currency = await _currencyManager.GetCurrencyShortNameAsync(response.CurrencyCode, cancellationToken);

			return result;
		}

		public async Task<CardFullBalanceViewModel> MapToCardFullBalanceViewModelAsync(CardBalanceApiModel response, CancellationToken cancellationToken)
		{
			var result = new CardFullBalanceViewModel
			{
				Available = response.Available,
				Own = response.Own,
				Blocked = response.Blocked,
				Loan = response.Loan,
				Overlimit = response.Overlimit,
				Overdue = response.Overdue,
				CreditLimit = response.CreditLimit,
				FinBlocking = response.FinBlocking,
				Interests = response.Interests,
				Penalty = response.Penalty,
				MinimalPayment = response.MinimalPayment,
				MandatoryPayment = response.MandatoryPayment,
				UnusedCreditLimit = response.UnusedCreditLimit,
				Overdraft = response.Overdraft,
				Debt = response.Debt,
				FullOwn = response.FullOwn,
				Fee = response.Fee
			};

			result.Currency = await _currencyManager.GetCurrencyShortNameAsync(response.CurrencyCode, cancellationToken);

			return result;
		}
	}
}
