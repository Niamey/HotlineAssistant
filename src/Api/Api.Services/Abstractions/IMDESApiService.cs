using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Services.Models.MDES;
using Vostok.Hotline.Core.Common.Enums.MDES;

namespace Vostok.Hotline.Api.Services.Abstractions
{
	public interface IMDESApiService
	{
		Task<TokenCollectionApiModel> GetTokensByPanAsync(string cardNumber, PaymentSystemTypeMdesEnum paymentType, CancellationToken cancellationToken);
		Task<TokenHistoryCollectionApiModel> GetHistoryByTokenAsync(string tokenUniqueReference, PaymentSystemTypeMdesEnum paymentType, CancellationToken cancellationToken);
		Task<StatusCommandApiViewModel> TokenDeleteAsync(string tokenUniqueReference, string commentText, ReasonTypeMdesEnum reasonCode, PaymentSystemTypeMdesEnum paymentType, CancellationToken cancellationToken);
		Task<StatusCommandApiViewModel> TokenActivateAsync(string tokenUniqueReference, PaymentSystemTypeMdesEnum paymentType, CancellationToken cancellationToken);
	}
}
