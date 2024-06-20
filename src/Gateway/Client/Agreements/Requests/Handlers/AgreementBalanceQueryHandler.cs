using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Agreements.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Agreements.Services;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Agreements.Requests.Handlers
{
	public class AgreementBalanceQueryHandler : IRequestHandler<AgreementBalanceQuery, AgreementBalanceViewModel>
	{
		private readonly AgreementService _agreementService;

		public AgreementBalanceQueryHandler(AgreementService agreementService)
		{
			_agreementService = agreementService;
		}
		public async Task<AgreementBalanceViewModel> Handle(AgreementBalanceQuery request, CancellationToken cancellationToken)
		{
			return await _agreementService.GetBalanceAsync(request, cancellationToken);
		}
	}
}
