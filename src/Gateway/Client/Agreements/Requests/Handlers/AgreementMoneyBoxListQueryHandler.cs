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
	public class AgreementMoneyBoxListQueryHandler : IRequestHandler<AgreementMoneyBoxListQuery, SearchRowsResponseRowModel<AgreementMoneyBoxViewModel>>
	{
		private readonly MoneyBoxService _moneyBoxService;

		public AgreementMoneyBoxListQueryHandler(MoneyBoxService moneyBoxService)
		{
			_moneyBoxService = moneyBoxService;
		}

		public async Task<SearchRowsResponseRowModel<AgreementMoneyBoxViewModel>> Handle(AgreementMoneyBoxListQuery request, CancellationToken cancellationToken)
		{
			var searchRequest = new AgreementMoneyBoxCollectionSearchRequest()
			{
				AgreementId = request.AgreementId
			};

			return await _moneyBoxService.GetMoneyBoxListAsync(searchRequest, cancellationToken);
		}
	}
}
