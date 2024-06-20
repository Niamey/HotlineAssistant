using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardImageUrlQueryHandler : IRequestHandler<CardImageUrlQuery, CardImageUrlViewModel>
	{
		private readonly CardService _cardService;

		public CardImageUrlQueryHandler(CardService cardService)
		{
			_cardService = cardService;
		}

		public async Task<CardImageUrlViewModel> Handle(CardImageUrlQuery request, CancellationToken cancellationToken)
		{
			return await _cardService.GetCardFrontImageUrlAsync(request, cancellationToken);
		}
	}
}
