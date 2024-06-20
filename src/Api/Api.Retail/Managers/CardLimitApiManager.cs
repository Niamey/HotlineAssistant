using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Retail.Abstractions;
using Vostok.Hotline.Api.Retail.Models.Cards;

namespace Vostok.Hotline.Api.Retail.Managers
{
	public class CardLimitApiManager
	{
		private readonly ICardLimitApiService _cardLimitApiService;
		public CardLimitApiManager(ICardLimitApiService cardLimitApiService)
		{
			_cardLimitApiService = cardLimitApiService;
		}

		public async Task<StatusCommandApiViewModel> ChangeCardLimitStatusAsync(long? clientId, long cardId, bool turnOn, CancellationToken cancellationToken)
		{
			return await _cardLimitApiService.ChangeCardLimitStatusAsync(clientId, cardId, turnOn, cancellationToken);
		}

		public async Task<StatusCommandApiViewModel> ChangeRiskAsync(long clientId, long cardId, decimal limit, bool isP2PLimited, DateTime? limitSetDate, string phone, CancellationToken cancellationToken)
		{
			return await _cardLimitApiService.ChangeRiskAsync(clientId, cardId, limit, isP2PLimited, limitSetDate, phone, cancellationToken);
		}

		public async Task<CardLimitIdsApiModel> GetLimitIdsAsync(long cardId, CancellationToken cancellationToken)
		{
			var result = new CardLimitIdsApiModel();
			var limits = await _cardLimitApiService.GetLimitsAsync(cardId, null, cancellationToken);
			foreach (var limit in limits)
			{
				result.LimitsIds.Add(limit.Id);
			}
			return result;
		}

		public async Task<CardCollectionLimitApiModel> GetLimitsAsync(long cardId, string limiterCode, CancellationToken cancellationToken)
		{
			return await _cardLimitApiService.GetLimitsAsync(cardId, limiterCode, cancellationToken);
		}

		public async Task<bool> IsActiveLimitAsync(long cardId, string limiterCode, CancellationToken cancellationToken)
		{
			var limit = (await _cardLimitApiService.GetLimitsAsync(cardId, limiterCode, cancellationToken)).FirstOrDefault();
			return limit?.Values?.Remaining > 0;
		}
	}
}
