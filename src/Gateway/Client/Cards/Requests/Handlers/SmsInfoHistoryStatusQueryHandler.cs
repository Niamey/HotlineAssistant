using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class SmsInfoHistoryStatusQueryHandler : IRequestHandler<SmsInfoHistoryStatusQuery, SmsInfoHistoryStatusViewModel>
	{
		private readonly SmsInfoService _smsInfoService;

		public SmsInfoHistoryStatusQueryHandler(SmsInfoService smsInfoService)
		{
			_smsInfoService = smsInfoService;
		}

		public async Task<SmsInfoHistoryStatusViewModel> Handle(SmsInfoHistoryStatusQuery searchRequest, CancellationToken cancellationToken)
		{
			return await _smsInfoService.GetHistoryStatusAsync(searchRequest, cancellationToken);			
		}
	}
}
