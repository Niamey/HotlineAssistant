using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Managers;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Gateway.Client.Cards.Mapper;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Services
{
	public class CardHistoryService
	{
		private readonly CardHistoryApiManager _cardHistoryManager;
		public CardHistoryService(CardHistoryApiManager cardHistoryManager)
		{
			_cardHistoryManager = cardHistoryManager;
		}

		public async Task<CardHistoryStatusViewModel> GetCardHistoryStatusAsync(CardHistoryStatusQuery request, CancellationToken cancellationToken)
		{
			var result =  await _cardHistoryManager.GetStatusesAsync(request.CardId.Value, cancellationToken);
			if (result != null)
				return result.ToCardHistoryStatusViewModel();
			else
				throw new NotFoundRequestException();
		}

		public async Task<CardCurrentStatusViewModel> GetCurrentStatusAsync(CardCurrentStatusQuery request, CancellationToken cancellationToken)
		{
			var result = await _cardHistoryManager.GetCurrentStatusAsync(request.CardId.Value, cancellationToken);
			if (result != null)
				return result.ToCardCurrentStatusViewModel();
			else
				throw new NotFoundRequestException();
		}
	}
}