using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Api.Retail.Models.Agreements;

namespace Vostok.Hotline.Api.Retail.Managers
{
	public class AgreementHistoryApiManager
	{
		private readonly IAgreementHistoryApiService _agreementHistoryApiService;
		public AgreementHistoryApiManager(IAgreementHistoryApiService agreementHistoryApiService)
		{
			_agreementHistoryApiService = agreementHistoryApiService;
		}

		public async Task<AgreementCollectionHistoryStatusApiModel> GetStatusesAsync(long agreementId, CancellationToken cancellationToken)
		{
			return await _agreementHistoryApiService.GetAgreementHistoryStatusAsync(agreementId, cancellationToken);
		}
	}
}
