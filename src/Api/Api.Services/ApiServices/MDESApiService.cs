using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.Mapper.MDES;
using Vostok.Hotline.Api.Services.Models.MDES;
using Vostok.Hotline.Api.Services.Responses.MDES;
using Vostok.Hotline.Api.Services.SearchRequests.MDES;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Enums.MDES;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Managers;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Api.Services.ApiServices
{
	public class MDESApiService : IMDESApiService
	{
		private readonly HotlineHttpClient _httpClient;
		private readonly EnvironmentManager _environmentManager;

		internal MDESApiService(IServiceProvider serviceProvider)
		{
			_httpClient = serviceProvider.GetRequiredService<HotlineHttpClient>();
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
		}

		public async Task<TokenCollectionApiModel> GetTokensByPanAsync(string cardNumber, PaymentSystemTypeMdesEnum paymentType, CancellationToken cancellationToken) {
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.MDES.GetTokensByPanUrl, cancellationToken);
			var userId = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.MDES.UserId, cancellationToken);

			var uri = new Uri($"{host}");
			var request = new GetTokensByPanRequest(cardNumber, userId, paymentType);
			var json = JsonHelper.ToJson(request);
			
			var jsonResult = await _httpClient.PostTryAsync(uri, json, cancellationToken);
			if (!jsonResult.IsSuccessStatusCode)
				throw new FailedRequestException(jsonResult);

			var result = JsonHelper.FromJson<TokenListResponseModel>(jsonResult?.Response);

			return result.ToTokenListApiModel();
		}

		public async Task<TokenHistoryCollectionApiModel> GetHistoryByTokenAsync(string tokenUniqueReference, PaymentSystemTypeMdesEnum paymentType, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.MDES.GetHistoryByTokenUrl, cancellationToken);
			var userId = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.MDES.UserId, cancellationToken);

			var uri = new Uri($"{host}");
			var request = new GetHistoryByTokenRequest(tokenUniqueReference, userId, paymentType);
			var json = JsonHelper.ToJson(request);

			var jsonResult = await _httpClient.PostTryAsync(uri, json, cancellationToken);
			if (!jsonResult.IsSuccessStatusCode)
				throw new FailedRequestException(jsonResult);

			var result = JsonHelper.FromJson<TokenHistoryListResponseModel>(jsonResult?.Response);

			return result.ToTokenHistoryListApiModel();
		}

		private async Task<StatusCommandApiViewModel> TokenActionAsync(string host, string tokenUniqueReference, string commentText, ReasonTypeMdesEnum reasonCode, PaymentSystemTypeMdesEnum paymentType, CancellationToken cancellationToken)
		{
			var userId = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.MDES.UserId, cancellationToken);

			var uri = new Uri($"{host}");
			var request = new TokenActionRequest(tokenUniqueReference, commentText, reasonCode, userId, paymentType);
			var json = JsonHelper.ToJson(request);
			
			var jsonResult = await _httpClient.PostTryAsync(uri, json, cancellationToken);
			if (!jsonResult.IsSuccessStatusCode)
				throw new FailedRequestException(jsonResult);

			var result = JsonHelper.FromJson<TokenActionResponseModel>(jsonResult?.Response);
						
			return result.ToStatusCommandApiViewModel();
		}

		public async Task<StatusCommandApiViewModel> TokenDeleteAsync(string tokenUniqueReference, string commentText, ReasonTypeMdesEnum reasonCode, PaymentSystemTypeMdesEnum paymentType, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.MDES.TokenDeleteUrl, cancellationToken);
			var result = await TokenActionAsync(host, tokenUniqueReference, commentText, reasonCode, paymentType, cancellationToken);
			return result;
		}

		public async Task<StatusCommandApiViewModel> TokenActivateAsync(string tokenUniqueReference, PaymentSystemTypeMdesEnum paymentType, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.MDES.TokenActivateUrl, cancellationToken);
			var result = await TokenActionAsync(host, tokenUniqueReference, "", ReasonTypeMdesEnum.Auth, paymentType, cancellationToken);
			return result;
		}
	}
}
