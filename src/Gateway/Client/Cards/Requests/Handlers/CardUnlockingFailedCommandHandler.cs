using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Services;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardUnlockingFailedCommandHandler : IRequestHandler<CardUnlockingFailedCommand, StatusCommandViewModel>
	{
		private readonly CardUnlockingService _cardUnlocking;

		public CardUnlockingFailedCommandHandler(CardUnlockingService cardUnlocking)
		{
			_cardUnlocking = cardUnlocking;
		}
		public async Task<StatusCommandViewModel> Handle(CardUnlockingFailedCommand request, CancellationToken cancellationToken)
		{
			return await _cardUnlocking.CardUnlockingFailedAsync(request, cancellationToken);
		}
	}
}
