using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.Models.MDES;
using Vostok.Hotline.Core.Common.Enums.MDES;

namespace Vostok.Hotline.Api.Services.Managers
{
	public class MDESApiManager
	{
		private readonly IMDESApiService _mdesApiService;

		public MDESApiManager(IMDESApiService mdesApiService) {
			_mdesApiService = mdesApiService;
		}

		public async Task<TokenCollectionApiModel> GetTokensByPanAsync(string cardNumber, PaymentSystemTypeMdesEnum paymentType, CancellationToken cancellationToken) {
			var response = await _mdesApiService.GetTokensByPanAsync(cardNumber, paymentType, cancellationToken);
			if (response != null)
				return new TokenCollectionApiModel(response?.OrderBy(i => i.THdateTime));
			else
				return null;
		}

		public async Task<TokenHistoryCollectionApiModel> GetHistoryByTokenAsync(string tokenUniqueReference, PaymentSystemTypeMdesEnum paymentType, CancellationToken cancellationToken)
		{
			return await _mdesApiService.GetHistoryByTokenAsync(tokenUniqueReference, paymentType, cancellationToken);
		}

		public async Task<TokenHistoryApiModel> GetLastStatusAsync(string tokenUniqueReference, PaymentSystemTypeMdesEnum paymentType, CancellationToken cancellationToken)
		{
			var response = await _mdesApiService.GetHistoryByTokenAsync(tokenUniqueReference, paymentType, cancellationToken);
			return response?.OrderBy(i => i.StatusDateTime).Last();
		}

		public async Task<StatusCommandApiViewModel> TokenDeleteAsync(string tokenUniqueReference, string commentText, ReasonTypeMdesEnum reasonCode, PaymentSystemTypeMdesEnum paymentType, CancellationToken cancellationToken)
		{
			return await _mdesApiService.TokenDeleteAsync(tokenUniqueReference, commentText, reasonCode, paymentType, cancellationToken);
		}

		public async Task<StatusCommandApiViewModel> TokenActivateAsync(string tokenUniqueReference, PaymentSystemTypeMdesEnum paymentType, CancellationToken cancellationToken)
		{
			return await _mdesApiService.TokenActivateAsync(tokenUniqueReference, paymentType, cancellationToken);
		}

	}
}
