using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Core.Common.Helpers;

namespace Vostok.Hotline.Api.Retail.Managers
{
	public class CardApiManager
	{
		private readonly ICardApiService _cardApiService;
		public CardApiManager(ICardApiService cardApiService)
		{
			_cardApiService = cardApiService;
		}

		public async Task<CardBalanceApiModel> GetCardBalanceAsync(long? clientId, long cardId, CancellationToken cancellationToken)
		{
			return await _cardApiService.GetCardBalanceAsync(clientId, cardId, cancellationToken);
		}

		public async Task<CardCollectionApiModel> GetCardCollectionAsync(long clientId, CancellationToken cancellationToken)
		{
			var result = await _cardApiService.GetCardCollectionAsync(clientId, cancellationToken);
			if (result != null)
				return new CardCollectionApiModel(result.Where(x => x.SolarId == clientId));

			return result;
		}

		public async Task<CardApiModel> GetCardAsync(long? clientId, long cardId, CancellationToken cancellationToken)
		{
			var result = await _cardApiService.GetCardAsync(clientId, cardId, cancellationToken);
			if (result != null)
			{
				if (result.SolarId != clientId || result.CardId != cardId)
					return null;
			}

			return result;
		}

		public async Task<string> GetCardNumberAsync(long clientId, long cardId, CancellationToken cancellationToken)
		{
			var result = await _cardApiService.GetCardAsync(clientId, cardId, cancellationToken);
			return result?.Number;
		}
	}
}
