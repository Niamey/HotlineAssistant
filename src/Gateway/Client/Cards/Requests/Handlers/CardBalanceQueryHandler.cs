using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardBalanceQueryHandler : IRequestHandler<CardBalanceQuery, CardBalanceViewModel>
	{
		private readonly CardService _cardService;

		public CardBalanceQueryHandler(CardService cardService)
		{
			_cardService = cardService;
		}

		public async Task<CardBalanceViewModel> Handle(CardBalanceQuery searchRequest, CancellationToken cancellationToken)
		{
			return await _cardService.GetCardBalanceAsync(searchRequest, cancellationToken);
		}
	}
}