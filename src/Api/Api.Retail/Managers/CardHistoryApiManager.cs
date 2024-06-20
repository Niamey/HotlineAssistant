using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Api.Retail.Models.Cards;

namespace Vostok.Hotline.Api.Retail.Managers
{
	public class CardHistoryApiManager
	{
		private readonly ICardHistoryApiService _cardHistoryApiService;
		public CardHistoryApiManager(ICardHistoryApiService cardApiService)
		{
			_cardHistoryApiService = cardApiService;
		}


		public async Task<CardHistoryStatusApiModel> GetCurrentStatusAsync(long cardId, CancellationToken cancellationToken)
		{
			var result = await GetStatusesAsync(cardId, cancellationToken);
			return result?.FirstOrDefault();
		}

		public async Task<CardCollectionHistoryStatusApiModel> GetStatusesAsync(long cardId, CancellationToken cancellationToken)
		{
			return await _cardHistoryApiService.GetCardHistoryStatusAsync(cardId, cancellationToken);
		}
	}
}
