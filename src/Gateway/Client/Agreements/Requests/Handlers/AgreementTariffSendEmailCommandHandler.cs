using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Gateway.Client.Agreements.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Agreements.Services;

namespace Vostok.Hotline.Gateway.Client.Agreements.Requests.Handlers
{
	public class AgreementTariffSendEmailCommandHandler : IRequestHandler<AgreementTariffSendEmailCommand, StatusCommandViewModel>
	{
		private readonly AgreementTariffService _agreementTariffService;

		public AgreementTariffSendEmailCommandHandler(AgreementTariffService agreementTariffService)
		{
			_agreementTariffService = agreementTariffService;
		}
		public async Task<StatusCommandViewModel> Handle(AgreementTariffSendEmailCommand command, CancellationToken cancellationToken)
		{
			return await _agreementTariffService.SendEmailAsync(command, cancellationToken);
		}
	}
}
