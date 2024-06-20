using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Managers;
using Vostok.Hotline.Api.Retail.Models.Agreements;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Gateway.Client.Agreements.Mapper;
using Vostok.Hotline.Gateway.Client.Agreements.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Agreements.Services
{
	public class AgreementHistoryService
	{
		private readonly AgreementHistoryApiManager _agreementHistoryManager;
		public AgreementHistoryService(AgreementHistoryApiManager agreementHistoryManager)
		{
			_agreementHistoryManager = agreementHistoryManager;
		}

		public async Task<AgreementHistoryStatusViewModel> GetAgreementHistoryStatusAsync(AgreementHistoryStatusQuery request, CancellationToken cancellationToken)
		{
			AgreementCollectionHistoryStatusApiModel result = await _agreementHistoryManager.GetStatusesAsync(request.AgreementId.Value, cancellationToken);
			if (result != null)
				return result.ToAgreementHistoryStatusViewModel();
			else
				throw new NotFoundRequestException();
		}
	}
}
