using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Services;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardTariffSendEmailCommandHandler : IRequestHandler<CardTariffSendEmailCommand, StatusCommandViewModel>
	{
		private readonly CardTariffService _cardTariffService;

		public CardTariffSendEmailCommandHandler(CardTariffService cardTariffService)
		{
			_cardTariffService = cardTariffService;
		}

		public async Task<StatusCommandViewModel> Handle(CardTariffSendEmailCommand command, CancellationToken cancellationToken)
		{
			return await _cardTariffService.SendEmailAsync(command, cancellationToken);
		}
	}
}
