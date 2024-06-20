using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Vostok.HotLineAssistant.CardManager.Application.Cards.Dtos;
using Vostok.HotLineAssistant.CardManager.Application.Cards.Queries;
using Vostok.HotLineAssistant.CardManager.Domain.Models.Cards;

namespace Vostok.HotLineAssistant.CardManager.Infrastructure.QueryHandlers
{
	public class CardInfoQueryHandler : IRequestHandler<CardInfoByIdQuery, CardInfoDto>
	{
		private IClientBalanceService _balanceService;

		public CardInfoQueryHandler(IClientBalanceService balanceService)
		{
			_balanceService = balanceService;
		}

		public Task<CardInfoDto> Handle(CardInfoByIdQuery request, CancellationToken cancellationToken)
		{
			return (Task<CardInfoDto>)Task.CompletedTask;
		}
	}
}
