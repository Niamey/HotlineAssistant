using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Api.Retail.SearchRequests.Agreements;
using Vostok.Hotline.Gateway.Client.Agreements.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Agreements.Services;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Agreements.Requests.Handlers
{
	public class AgreementTariffDetailQueryHandler : IRequestHandler<AgreementTariffDetailQuery, AgreementTariffViewModel>
	{
		private readonly AgreementTariffService _agreementTariffService;

		public AgreementTariffDetailQueryHandler(AgreementTariffService agreementTariffService)
		{
			_agreementTariffService = agreementTariffService;
		}

		public async Task<AgreementTariffViewModel> Handle(AgreementTariffDetailQuery request, CancellationToken cancellationToken)
		{
			var searchRequest = new AgreementTariffSearchRequest
			{
				ClientId = request.SolarId,
				AgreementId = request.AgreementId
			};

			return await _agreementTariffService.GetCurrentTariffAsync(searchRequest, cancellationToken);
		}
	}
}
