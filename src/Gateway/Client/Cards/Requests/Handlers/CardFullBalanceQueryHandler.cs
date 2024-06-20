using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardFullBalanceQueryHandler : IRequestHandler<CardFullBalanceQuery, CardFullBalanceViewModel>
	{
		private readonly CardService _cardService;

		public CardFullBalanceQueryHandler(CardService cardService)
		{
			_cardService = cardService;
		}

		public async Task<CardFullBalanceViewModel> Handle(CardFullBalanceQuery searchRequest, CancellationToken cancellationToken)
		{
			return await _cardService.GetCardFullBalanceAsync(searchRequest, cancellationToken);
		}
	}
}