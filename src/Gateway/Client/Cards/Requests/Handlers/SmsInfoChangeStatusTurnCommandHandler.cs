using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Enums.Cards;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class SmsInfoChangeStatusTurnCommandHandler : IRequestHandler<SmsInfoChangeStatusTurnOnCommand, SmsInfoChangeStatusViewModel>
	{
		private readonly SmsInfoService _smsInfoService;

		public SmsInfoChangeStatusTurnCommandHandler(SmsInfoService smsInfoService)
		{
			_smsInfoService = smsInfoService;
		}

		public async Task<SmsInfoChangeStatusViewModel> Handle(SmsInfoChangeStatusTurnOnCommand request, CancellationToken cancellationToken)
		{
			return await _smsInfoService.ChangeStatusTurnOnAsync(request, cancellationToken);
		}
	}
}