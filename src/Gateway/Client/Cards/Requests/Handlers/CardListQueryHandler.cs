using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardListQueryHandler : IRequestHandler<CardListQuery, SearchRowsResponseRowModel<CardViewModel>>
	{
		private readonly CardService _cardService;

		public CardListQueryHandler(CardService cardService)
		{
			_cardService = cardService;
		}

		public async Task<SearchRowsResponseRowModel<CardViewModel>> Handle(CardListQuery searchRequest, CancellationToken cancellationToken)
		{
			return await _cardService.GetCardListAsync(searchRequest, cancellationToken);			
		}
	}
}
