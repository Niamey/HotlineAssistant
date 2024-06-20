using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardDetailQueryHandler : IRequestHandler<CardDetailQuery, CardViewModel>
	{
		private readonly CardService _cardService;

		public CardDetailQueryHandler(CardService cardService)
		{
			_cardService = cardService;
		}

		public async Task<CardViewModel> Handle(CardDetailQuery searchRequest, CancellationToken cancellationToken)
		{
			return await _cardService.GetCardDetailAsync(searchRequest, cancellationToken);
		}
	}
}