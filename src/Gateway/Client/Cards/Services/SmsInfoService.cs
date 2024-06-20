using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Managers;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Gateway.Client.Cards.Mapper;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Services
{
	public class SmsInfoService
	{
		private readonly SmsInfoApiManager _smsInfoApiManager;

		public SmsInfoService(SmsInfoApiManager smsInfoApiManager)
		{
			_smsInfoApiManager = smsInfoApiManager;
		}

		public async Task<SmsInfoViewModel> GetStatusAsync(SmsInfoQuery request, CancellationToken cancellationToken)
		{
			var result = await _smsInfoApiManager.GetStatusAsync(request.SolarId, request.CardId.Value, cancellationToken);
			if (result != null)
				return result.ToSmsInfoViewModel();
			else
				throw new NotFoundRequestException();
		}

		public async Task<SmsInfoChangeStatusViewModel> ChangeStatusTurnOnAsync(SmsInfoChangeStatusTurnOnCommand request, CancellationToken cancellationToken)
		{
			var result = await _smsInfoApiManager.ChangeStatusTurnOnAsync(request.SolarId, request.CardId.Value, cancellationToken);
			if (result != null)
				return result.ToSmsInfoChangeStatusViewModel();
			else
				throw new NotFoundRequestException();
		}

		public async Task<SmsInfoChangeStatusViewModel> ChangeStatusTurnOffAsync(SmsInfoChangeStatusTurnOffCommand request, CancellationToken cancellationToken)
		{
			var result = await _smsInfoApiManager.ChangeStatusTurnOffAsync(request.SolarId, request.CardId.Value, cancellationToken);
			if (result != null)
				return result.ToSmsInfoChangeStatusViewModel();
			else
				throw new NotFoundRequestException();
		}

		public async Task<SmsInfoHistoryStatusViewModel> GetHistoryStatusAsync(SmsInfoHistoryStatusQuery request, CancellationToken cancellationToken)
		{
			var result = await _smsInfoApiManager.GetHistoryStatusAsync(request.SolarId, request.CardId.Value, cancellationToken);
			if (result != null)
				return result.MapToSmsInfoHistoryStatusViewModel();
			else
				throw new NotFoundRequestException();
		}
	}
}
