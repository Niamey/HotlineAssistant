using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Services;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardUnlockingCommandHandler : IRequestHandler<CardUnlockingCommand, StatusCommandViewModel>
	{
		private readonly CardUnlockingService _cardUnlocking;

		public CardUnlockingCommandHandler(CardUnlockingService cardUnlocking)
		{
			_cardUnlocking = cardUnlocking;
		}
		public async Task<StatusCommandViewModel> Handle(CardUnlockingCommand request, CancellationToken cancellationToken)
		{
			return await _cardUnlocking.CardUnlockingAsync(request, cancellationToken);
		}
	}
}
