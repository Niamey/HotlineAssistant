using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Models.Agreements;

namespace Vostok.Hotline.Api.Retail.ApiServices.Abstractions
{
	public interface IAgreementApiService
	{
		Task<AgreementApiModel> GetAgreementAsync(long? clientId, long agreementId, CancellationToken cancellationToken);
		Task<AgreementCollectionApiModel> GetAgreementCollectionAsync(long clientId, CancellationToken cancellationToken);
		Task<AgreementBalanceApiModel> GetBalanceAsync(long? clientId, long agreementId, CancellationToken cancellationToken);
		Task<AgreementCreditParamsApiModel> GetCreditParamsAsync(long agreementId, CancellationToken cancellationToken);
	}
}