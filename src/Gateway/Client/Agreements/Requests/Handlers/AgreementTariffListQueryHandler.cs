using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Api.Retail.SearchRequests.Agreements;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Agreements.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Agreements.Services;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Agreements.Requests.Handlers
{
	public class AgreementTariffListQueryHandler : IRequestHandler<AgreementTariffListQuery, SearchRowsResponseRowModel<AgreementTariffViewModel>>
	{
		private readonly AgreementTariffService _agreementTariffService;

		public AgreementTariffListQueryHandler(AgreementTariffService agreementTariffService)
		{
			_agreementTariffService = agreementTariffService;
		}

		public async Task<SearchRowsResponseRowModel<AgreementTariffViewModel>> Handle(AgreementTariffListQuery request, CancellationToken cancellationToken)
		{
			var searchRequest = new AgreementTariffCollectionSearchRequest
			{
				ClientId = request.SolarId,
				AgreementId = request.AgreementId
			};

			return await _agreementTariffService.GetAllTariffAsync(searchRequest, cancellationToken);
		}
	}
}
