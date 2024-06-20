using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class SmsInfoQueryHandler : IRequestHandler<SmsInfoQuery, SmsInfoViewModel>
	{
		private readonly SmsInfoService _smsInfoService;

		public SmsInfoQueryHandler(SmsInfoService smsInfoService)
		{
			_smsInfoService = smsInfoService;
		}

		public async Task<SmsInfoViewModel> Handle(SmsInfoQuery searchRequest, CancellationToken cancellationToken)
		{
			return await _smsInfoService.GetStatusAsync(searchRequest, cancellationToken);
		}
	}
}