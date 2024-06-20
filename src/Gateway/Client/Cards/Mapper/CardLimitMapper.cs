using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.References.Managers;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Core.Common.Enums.Cards;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Mapper
{
	public class CardLimitMapper
	{
		private readonly CurrencyApiManager _currencyApiManager;

		public CardLimitMapper(CurrencyApiManager currencyApiManager)
		{
			_currencyApiManager = currencyApiManager;
		}


		public async Task<CardLimitViewModel> MapToCardLimitViewModelAsync(CardLimitApiModel response, CancellationToken cancellationToken)
		{
			var currencyName = await _currencyApiManager.GetCurrencyShortNameAsync(response?.Values?.Threshold?.Amount?.Currency, cancellationToken);

			return new CardLimitViewModel
			{
				Limits = new List<AmountCardLimitViewModel>()
				{
					new AmountCardLimitViewModel()
					{
						CurrencyCode = response.Values?.Threshold?.Amount?.Currency,
						Limit = response.Values?.Threshold?.Amount?.Amount,
						TypeLimit = CardTypeLimitEnum.Money,
						CurrencyName = currencyName ?? response.Values?.Threshold?.Amount?.Currency,
						PeriodTypeLimit = response.Values?.Threshold?.Period?.PeriodType,
						Used = response.Values?.Current
					}
				}
			};
		}
	}
}
