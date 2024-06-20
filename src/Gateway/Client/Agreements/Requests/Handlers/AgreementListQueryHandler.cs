using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Agreements.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Agreements.Services;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Agreements.Requests.Handlers
{
	public class AgreementListQueryHandler : IRequestHandler<AgreementListQuery, SearchRowsResponseRowModel<AgreementViewModel>>
	{
		private readonly AgreementService _agreementService;

		public AgreementListQueryHandler(AgreementService agreementService)
		{
			_agreementService = agreementService;
		}

		public async Task<SearchRowsResponseRowModel<AgreementViewModel>> Handle(AgreementListQuery searchRequest, CancellationToken cancellationToken)
		{
			return await _agreementService.GetAgreementListAsync(searchRequest, cancellationToken);			
		}
	}
}
