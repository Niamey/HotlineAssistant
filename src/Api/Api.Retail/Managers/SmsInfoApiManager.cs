using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Api.Retail.Managers
{
	public class SmsInfoApiManager
	{
		private readonly ISmsInfoApiService _smsInfoApiService;
		public SmsInfoApiManager(ISmsInfoApiService smsInfoApiService)
		{
			_smsInfoApiService = smsInfoApiService;
		}

		public async Task<SmsInfoApiModel> GetStatusAsync(long? clientId, long cardId, CancellationToken cancellationToken)
		{
			return await _smsInfoApiService.GetStatusAsync(clientId, cardId, cancellationToken);
		}

		public async Task<SmsInfoChangeStatusApiModel> ChangeStatusTurnOnAsync(long? clientId, long cardId, CancellationToken cancellationToken)
		{
			return await _smsInfoApiService.ChangeStatusAsync(clientId, cardId, SmsInfoChangeStatusEnum.TurnOn, cancellationToken);
		}

		public async Task<SmsInfoChangeStatusApiModel> ChangeStatusTurnOffAsync(long? clientId, long cardId, CancellationToken cancellationToken)
		{
			return await _smsInfoApiService.ChangeStatusAsync(clientId, cardId, SmsInfoChangeStatusEnum.TurnOff, cancellationToken);
		}

		public async Task<SmsInfoHistoryStatusApiModel> GetHistoryStatusAsync(long? clientId, long cardId, CancellationToken cancellationToken)
		{
			return await _smsInfoApiService.HistoryStatusAsync(clientId, cardId, cancellationToken);
		}
	}
}

