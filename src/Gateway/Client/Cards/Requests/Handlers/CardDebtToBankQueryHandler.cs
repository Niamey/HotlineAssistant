using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardDebtToBankQueryHandler : IRequestHandler<CardDebtToBankQuery, CardDebtToBankViewModel>
	{
		private readonly CardService _cardService;

		public CardDebtToBankQueryHandler(CardService cardService)
		{
			_cardService = cardService;
		}
		public async Task<CardDebtToBankViewModel> Handle(CardDebtToBankQuery request, CancellationToken cancellationToken)
		{
			return await _cardService.GetDebtToBankAsync(request, cancellationToken);
		}
	}
}
