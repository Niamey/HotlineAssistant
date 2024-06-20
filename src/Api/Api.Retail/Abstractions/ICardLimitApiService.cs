using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Api.Retail.Abstractions
{
	public interface ICardLimitApiService
	{
		Task<StatusCommandApiViewModel> ChangeCardLimitStatusAsync(long? clientId, long cardId, bool turnOn, CancellationToken cancellationToken);
		Task<StatusCommandApiViewModel> ChangeRiskAsync(long clientId, long cardId, decimal limit, bool isP2PLimited, DateTime? limitSetDate, string phone, CancellationToken cancellationToken);
		Task<CardSetLimitApiModel> SetLimitAsync(long cardId, int limitId, bool enabled, CancellationToken cancellationToken);
		Task<CardCollectionLimitApiModel> GetLimitsAsync(long cardId, string limiterCode, CancellationToken cancellationToken);
	}
}
