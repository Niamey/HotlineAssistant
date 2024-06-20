using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Agreements.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Agreements.Services;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Agreements.Requests.Handlers
{
	public class AgreementHistoryStatusQueryHandler : IRequestHandler<AgreementHistoryStatusQuery, AgreementHistoryStatusViewModel>
	{
		private readonly AgreementHistoryService _agreementHistoryService;

		public AgreementHistoryStatusQueryHandler(AgreementHistoryService agreementHistoryService)
		{
			_agreementHistoryService = agreementHistoryService;
		}

		public async Task<AgreementHistoryStatusViewModel> Handle(AgreementHistoryStatusQuery searchRequest, CancellationToken cancellationToken)
		{
			return await _agreementHistoryService.GetAgreementHistoryStatusAsync(searchRequest, cancellationToken);			
		}
	}
}
