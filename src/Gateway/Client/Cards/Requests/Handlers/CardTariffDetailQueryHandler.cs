using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Api.Retail.SearchRequests.Cards;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardTariffDetailQueryHandler : IRequestHandler<CardTariffDetailQuery, CardTariffViewModel>
	{
		private readonly CardTariffService _cardTariffService;

		public CardTariffDetailQueryHandler(CardTariffService cardTariffService)
		{
			_cardTariffService = cardTariffService;
		}

		public async Task<CardTariffViewModel> Handle(CardTariffDetailQuery request, CancellationToken cancellationToken)
		{
			var searchRequest = new CardTariffSearchRequest
			{
				ClientId = request.SolarId,
				CardId = request.CardId
			};

			return await _cardTariffService.GetCurrentTariffAsync(searchRequest, cancellationToken);
		}
	}
}
