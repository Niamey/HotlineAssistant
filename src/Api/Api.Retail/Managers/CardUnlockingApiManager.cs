using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Api.Retail.Mapper.Cards;
using Vostok.Hotline.Api.Retail.Responses.Cards.Enums;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Api.Retail.Managers
{
	public class CardUnlockingApiManager
	{
		private readonly ICardApiService _cardApiService;

		public CardUnlockingApiManager(ICardApiService cardApiService)
		{
			_cardApiService = cardApiService;
		}

		public async Task<StatusCommandApiViewModel> UnlockingAsync(long cardId, string comment, CancellationToken cancellationToken)
		{
			var result = await _cardApiService.ChangeStatusAsync(cardId, CardStatusRetailEnum.Active, comment, cancellationToken);
			return result.Status;
		}

		public async Task<StatusCommandApiViewModel> LockingAsync(long cardId, CardStatusEnum status, string comment, CancellationToken cancellationToken)
		{
			var result = await _cardApiService.ChangeStatusAsync(cardId, status.ToCardStatusRetailEnum(), comment, cancellationToken);
			return result.Status;
		}
	}
}
