using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Api.Retail.Models.Agreements;

namespace Vostok.Hotline.Api.Retail.Managers
{
	public class AgreementApiManager
	{
		private readonly IAgreementApiService _agreementApiService;
		public AgreementApiManager(IAgreementApiService agreementApiService)
		{
			_agreementApiService = agreementApiService;
		}

		public async Task<AgreementCollectionApiModel> GetAgreementCollectionAsync(long clientId, CancellationToken cancellationToken)
		{
			var result = await _agreementApiService.GetAgreementCollectionAsync(clientId, cancellationToken);
			if (result != null)
				return new AgreementCollectionApiModel(result.Where(x => x.SolarId == clientId));

			return result;
		}

		public async Task<AgreementApiModel> GetAgreementAsync(long clientId, long agreementId, CancellationToken cancellationToken)
		{
			var result = await _agreementApiService.GetAgreementAsync(clientId, agreementId, cancellationToken);
			if (result != null)
			{
				if (result.SolarId != clientId || result.AgreementId != agreementId)
					return null;
			}

			return result;
		}

		public async Task<string> GetAgreementNumberAsync(long clientId, long agreementId, CancellationToken cancellationToken)
		{
			var result = await _agreementApiService.GetAgreementAsync(clientId, agreementId, cancellationToken);
			return result?.Number;			
		}

		public async Task<AgreementBalanceApiModel> GetBalanceAsync(long? clientId, long agreementId, CancellationToken cancellationToken)
		{
			return await _agreementApiService.GetBalanceAsync(clientId, agreementId, cancellationToken);
		}		public async Task<AgreementCreditParamsApiModel> GetCreditParamsAsync(long agreementId, CancellationToken cancellationToken)
		{
			var result = await _agreementApiService.GetCreditParamsAsync(agreementId, cancellationToken);
			return result;
		}

	}
}
