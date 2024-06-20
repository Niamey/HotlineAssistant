using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardCurrentStatusQueryHandler : IRequestHandler<CardCurrentStatusQuery, CardCurrentStatusViewModel>
	{
		private readonly CardHistoryService _cardHistoryService;

		public CardCurrentStatusQueryHandler(CardHistoryService cardHistoryService)
		{
			_cardHistoryService = cardHistoryService;
		}

		public async Task<CardCurrentStatusViewModel> Handle(CardCurrentStatusQuery request, CancellationToken cancellationToken)
		{
			return await _cardHistoryService.GetCurrentStatusAsync(request, cancellationToken);			
		}
	}
}
