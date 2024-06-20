using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Gateway.Client.Agreements.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Agreements.Services;

namespace Vostok.Hotline.Gateway.Client.Agreements.Requests.Handlers
{
	class AgreementTariffSendViberCommandHandler : IRequestHandler<AgreementTariffSendViberCommand, StatusCommandViewModel>
	{
		private readonly AgreementTariffService _agreementTariffService;

		public AgreementTariffSendViberCommandHandler(AgreementTariffService agreementTariffService)
		{
			_agreementTariffService = agreementTariffService;
		}

		public async Task<StatusCommandViewModel> Handle(AgreementTariffSendViberCommand command, CancellationToken cancellationToken)
		{
			return await _agreementTariffService.SendViberAsync(command, cancellationToken);
		}
	}
}
