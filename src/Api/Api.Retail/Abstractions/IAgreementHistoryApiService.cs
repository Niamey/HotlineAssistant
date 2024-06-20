using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Models.Agreements;

namespace Vostok.Hotline.Api.Retail.ApiServices.Abstractions
{
	public interface IAgreementHistoryApiService
	{
		Task<AgreementCollectionHistoryStatusApiModel> GetAgreementHistoryStatusAsync(long agreementId, CancellationToken cancellationToken);
	}
}