using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardHistoryStatusQueryHandler : IRequestHandler<CardHistoryStatusQuery, CardHistoryStatusViewModel>
	{
		private readonly CardHistoryService _cardHistoryService;

		public CardHistoryStatusQueryHandler(CardHistoryService cardHistoryService)
		{
			_cardHistoryService = cardHistoryService;
		}

		public async Task<CardHistoryStatusViewModel> Handle(CardHistoryStatusQuery searchRequest, CancellationToken cancellationToken)
		{
			return await _cardHistoryService.GetCardHistoryStatusAsync(searchRequest, cancellationToken);			
		}
	}
}
