using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Api.Retail.SearchRequests.Cards;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardTariffListQueryHandler : IRequestHandler<CardTariffListQuery, SearchRowsResponseRowModel<CardTariffViewModel>>
	{
		private readonly CardTariffService _cardTariffService;

		public CardTariffListQueryHandler(CardTariffService cardTariffService)
		{
			_cardTariffService = cardTariffService;
		}

		public async Task<SearchRowsResponseRowModel<CardTariffViewModel>> Handle(CardTariffListQuery request, CancellationToken cancellationToken)
		{
			var searchRequest = new CardTariffCollectionSearchRequest
			{
				ClientId = request.SolarId,
				CardId = request.CardId
			};

			return await _cardTariffService.GetAllTariffAsync(searchRequest, cancellationToken);
		}
	}
}
