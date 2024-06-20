using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardExtendedBalanceQueryHandler : IRequestHandler<CardExtendedBalanceQuery, CardExtendedBalanceViewModel>
	{
		private readonly CardService _cardService;

		public CardExtendedBalanceQueryHandler(CardService cardService)
		{
			_cardService = cardService;
		}

		public async Task<CardExtendedBalanceViewModel> Handle(CardExtendedBalanceQuery searchRequest, CancellationToken cancellationToken)
		{
			return await _cardService.GetCardExtendedBalanceAsync(searchRequest, cancellationToken);
		}
	}
}