using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Agreements.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Agreements.Services;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Agreements.Requests.Handlers
{
	public class AgreementDetailQueryHandler : IRequestHandler<AgreementDetailQuery, AgreementViewModel>
	{
		private readonly AgreementService _agreementService;

		public AgreementDetailQueryHandler(AgreementService agreementService)
		{
			_agreementService = agreementService;
		}

		public async Task<AgreementViewModel> Handle(AgreementDetailQuery searchRequest, CancellationToken cancellationToken)
		{
			return await _agreementService.GetAgreementDetailAsync(searchRequest, cancellationToken);			
		}
	}
}
