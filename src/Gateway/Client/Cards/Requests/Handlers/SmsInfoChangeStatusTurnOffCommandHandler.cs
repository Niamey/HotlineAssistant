using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Core.Common.Enums.Cards;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class SmsInfoChangeStatusTurnOffCommandHandler : IRequestHandler<SmsInfoChangeStatusTurnOffCommand, SmsInfoChangeStatusViewModel>
	{
		private readonly SmsInfoService _smsInfoService;

		public SmsInfoChangeStatusTurnOffCommandHandler(SmsInfoService smsInfoService)
		{
			_smsInfoService = smsInfoService;
		}

		public async Task<SmsInfoChangeStatusViewModel> Handle(SmsInfoChangeStatusTurnOffCommand request, CancellationToken cancellationToken)
		{
			return await _smsInfoService.ChangeStatusTurnOffAsync(request, cancellationToken);
		}
	}
}