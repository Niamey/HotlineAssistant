using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Managers;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Mapper;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Commands;

namespace Vostok.Hotline.Gateway.Client.Cards.Services
{
	public class CardLimitService
	{
		private readonly CardLimitApiManager _cardLimitApiManager;
		public CardLimitService(CardLimitApiManager cardLimitApiManager)
		{
			_cardLimitApiManager = cardLimitApiManager;
		}
		public async Task<StatusCommandViewModel> ChangeCardLimitStatusAsync(CardLimitOfCashWithdrawalCommand request, CancellationToken cancellationToken)
		{
			var result = await _cardLimitApiManager.ChangeCardLimitStatusAsync(request.ClientId, request.CardId.Value, request.TurnOn.Value, cancellationToken);
			if (result != null)
				return result.MapToStatusCommandViewModel();
			else
				throw new NotFoundRequestException();
		}

		public async Task<StatusCommandViewModel> ChangeRiskAsync(CardLimitRiskChangeCommand request, CancellationToken cancellationToken)
		{
			var result = await _cardLimitApiManager.ChangeRiskAsync(request.ClientId.Value, request.CardId.Value, request.Limit.Value, request.IsP2PLimited.Value, request.LimitSetDate, request.Phone, cancellationToken);
			if (result != null)
				return result.MapToStatusCommandViewModel();
			else
				throw new NotFoundRequestException();
		}
	}
}
