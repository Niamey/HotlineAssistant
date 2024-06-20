using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardTokenDeleteCommandHandler : IRequestHandler<CardTokenDeleteCommand, StatusCommandViewModel>
	{
		private readonly CardTokenService _cardTokenService;

		public CardTokenDeleteCommandHandler(CardTokenService cardTokenService)
		{
			_cardTokenService = cardTokenService;
		}

		public async Task<StatusCommandViewModel> Handle(CardTokenDeleteCommand request, CancellationToken cancellationToken)
		{
			return await _cardTokenService.TokenDeleteAsync(request, cancellationToken);			
		}
	}
}
