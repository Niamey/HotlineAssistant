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
	public class CardTokenActivateCommandHandler : IRequestHandler<CardTokenActivateCommand, StatusCommandViewModel>
	{
		private readonly CardTokenService _cardTokenService;

		public CardTokenActivateCommandHandler(CardTokenService cardTokenService)
		{
			_cardTokenService = cardTokenService;
		}

		public async Task<StatusCommandViewModel> Handle(CardTokenActivateCommand request, CancellationToken cancellationToken)
		{
			return await _cardTokenService.TokenActivateAsync(request, cancellationToken);			
		}
	}
}
