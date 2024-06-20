using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Retail.Abstractions;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Api.Retail.Mapper.Cards;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Api.Retail.Models.Transactions;
using Vostok.Hotline.Core.Common.Exeptions;

namespace Vostok.Hotline.Api.Retail.Managers
{
	public class TravelApiManager
	{
		private readonly ICardApiService _cardApiService;
		private readonly CardHistoryApiManager _cardHistoryApiManager;

		public TravelApiManager(ICardApiService cardApiService, CardHistoryApiManager cardHistoryApiManager)
		{
			_cardApiService = cardApiService;
			_cardHistoryApiManager = cardHistoryApiManager;
		}

		public async Task<CardChangeStatusResultApiModel> AddCommentToCardAsync(long cardId, string comment, CancellationToken cancellationToken)
		{
			var currentStatus = await _cardHistoryApiManager.GetCurrentStatusAsync(cardId, cancellationToken);
			if (currentStatus != null) {
				return await _cardApiService.ChangeStatusAsync(cardId, currentStatus.Status.ToCardStatusRetailEnum(), comment, cancellationToken);
			} else
			{
				return null;
			}			
		}
	}
}
