using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;
using Vostok.Hotline.Gateway.Client.Agreements.Mapper;
using Vostok.Hotline.Api.Retail.Managers;
using Vostok.Hotline.Gateway.Client.Agreements.Requests.Queries;

namespace Vostok.Hotline.Gateway.Client.Agreements.Services
{
	public class AgreementService
	{
		private readonly AgreementApiManager _agreementManager;
		private readonly AgreementMapper _agreementMapper;
		private readonly AgreementBalanceMapper _agreementBalanceMapper;
		public AgreementService(AgreementApiManager agreementManager, AgreementMapper agreementMapper, AgreementBalanceMapper agreementBalanceMapper)
		{
			_agreementManager = agreementManager;
			_agreementMapper = agreementMapper;
			_agreementBalanceMapper = agreementBalanceMapper;
		}

		public async Task<SearchRowsResponseRowModel<AgreementViewModel>> GetAgreementListAsync(AgreementListQuery request, CancellationToken cancellationToken)
		{
			var result = await _agreementManager.GetAgreementCollectionAsync(request.SolarId.Value, cancellationToken);
			return await _agreementMapper.ToAgreementListViewModelAsync(result, cancellationToken);
		}

		public async Task<AgreementViewModel> GetAgreementDetailAsync(AgreementDetailQuery request, CancellationToken cancellationToken)
		{
			var result = await _agreementManager.GetAgreementAsync(request.SolarId.Value, request.AgreementId.Value, cancellationToken);
			if (result != null)
				return await _agreementMapper.ToAgreementViewModelAsync(result, cancellationToken);
			else
				return null;// throw new NotFoundRequestException();
		}

		public async Task<AgreementBalanceViewModel> GetBalanceAsync(AgreementBalanceQuery request, CancellationToken cancellationToken)
		{
			var result = await _agreementManager.GetBalanceAsync(request.SolarId.Value, request.AgreementId.Value, cancellationToken);
			if (result != null)
				return await _agreementBalanceMapper.MapToAgreementBalanceViewModelAsync(result, cancellationToken);
			else
				return null;// throw new NotFoundRequestException();
		}
	}
}