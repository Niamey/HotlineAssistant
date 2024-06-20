using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Services;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardLimitRiskChangeCommandHandler : IRequestHandler<CardLimitRiskChangeCommand, StatusCommandViewModel>
	{
		private readonly CardLimitService _cardLimitService;

		public CardLimitRiskChangeCommandHandler(CardLimitService cardLimitService)
		{
			_cardLimitService = cardLimitService;
		}
		public async Task<StatusCommandViewModel> Handle(CardLimitRiskChangeCommand request, CancellationToken cancellationToken)
		{
			return await _cardLimitService.ChangeRiskAsync(request, cancellationToken);
		}
	}
}
