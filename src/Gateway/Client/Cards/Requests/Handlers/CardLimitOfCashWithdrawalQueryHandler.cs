using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardLimitOfCashWithdrawalQueryHandler : IRequestHandler<CardLimitOfCashWithdrawalQuery, CardLimitOfCashWithdrawalViewModel>
	{
		private readonly CardService _cardService;

		public CardLimitOfCashWithdrawalQueryHandler(CardService cardService)
		{
			_cardService = cardService;
		}

		public async Task<CardLimitOfCashWithdrawalViewModel> Handle(CardLimitOfCashWithdrawalQuery searchRequest, CancellationToken cancellationToken)
		{
			return await _cardService.GetCardLimitOfCashWithdrawalAsync(searchRequest, cancellationToken);
		}
	}
}