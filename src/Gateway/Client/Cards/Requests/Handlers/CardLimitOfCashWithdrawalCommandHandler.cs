using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Services;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardLimitOfCashWithdrawalCommandHandler : IRequestHandler<CardLimitOfCashWithdrawalCommand, StatusCommandViewModel>
	{
		private readonly CardLimitService _cardLimitService;

		public CardLimitOfCashWithdrawalCommandHandler(CardLimitService cardLimitService)
		{
			_cardLimitService = cardLimitService;
		}

		public async Task<StatusCommandViewModel> Handle(CardLimitOfCashWithdrawalCommand searchRequest, CancellationToken cancellationToken)
		{
			return await _cardLimitService.ChangeCardLimitStatusAsync(searchRequest, cancellationToken);
		}
	}
}