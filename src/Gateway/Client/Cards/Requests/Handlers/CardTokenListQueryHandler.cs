using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardTokenListQueryHandler : IRequestHandler<CardTokenListQuery, SearchRowsResponseRowModel<CardTokenViewModel>>
	{
		private readonly CardTokenService _cardTokenService;

		public CardTokenListQueryHandler(CardTokenService cardTokenService)
		{
			_cardTokenService = cardTokenService;
		}

		public async Task<SearchRowsResponseRowModel<CardTokenViewModel>> Handle(CardTokenListQuery request, CancellationToken cancellationToken)
		{
			return await _cardTokenService.GetCardTokenListAsync(request, cancellationToken);			
		}
	}
}
