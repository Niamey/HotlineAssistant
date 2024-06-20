using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.Exceptions;
using Vostok.Hotline.Api.Services.Mapper.Messenger;
using Vostok.Hotline.Api.Services.Models;
using Vostok.Hotline.Api.Services.Responses.Messenger;
using Vostok.Hotline.Api.Services.SearchRequests;
using Vostok.Hotline.Core.Common.Enums.Messenger;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Managers;

namespace Vostok.Hotline.Api.Services.Providers
{
	internal class VostokMessengerProvider : IMessengerProvider
	{
		private readonly VostokMessengerSettings _vostokMessengerSettings;
		private readonly HotlineHttpClient _httpClient;
		public ProviderTypeEnum ProviderType => ProviderTypeEnum.VostokMessenger;

		public VostokMessengerProvider(IServiceProvider serviceProvider) {
			_vostokMessengerSettings = serviceProvider.GetRequiredService<VostokMessengerSettings>();
			_httpClient = serviceProvider.GetRequiredService<HotlineHttpClient>();
		}

		public async Task<SendingStatusApiModel> SendAsync<VostokMessagePackageApiModel>(IMessagePackageApiModel<VostokMessagePackageApiModel> msg, CancellationToken cancellationToken)
		{
			var messagePackageRequest = msg.GetMessage(); ;
			var tokenResult = await GetTokenAsync(cancellationToken);
			var host = _vostokMessengerSettings.Url;
			var uri = new Uri($"{host}/message");

			var headers = new Core.Common.Models.HttpClientModels.HttpClientHeaderCollectionModel();
			headers.Add("Authorization", $"Bearer {tokenResult.AccessToken}");

			var json = JsonHelper.ToJson(messagePackageRequest);

			var response = await _httpClient.PostTryAsync(uri, json, headers, cancellationToken);
			if (!response.IsSuccessStatusCode)
				throw new MessengerApiException(response);

			var result = JsonHelper.FromJson<MessageResponseModel>(response?.Response);
			return result.ToSendingStatusApiModel();
		}
		public async Task<SendingStatusApiModel> GetStatusAsync(MessageStatusRequestApiModel statusRequest, CancellationToken cancellationToken)
		{
			var tokenResult = await GetTokenAsync(cancellationToken);
			var host = _vostokMessengerSettings.Url;
			var uri = new Uri($"{host}/report");

			var headers = new Core.Common.Models.HttpClientModels.HttpClientHeaderCollectionModel();
			headers.Add("Authorization", $"Bearer {tokenResult.AccessToken}");

			var getStatusRequest = new GetStatusRequest()
			{
				ExternalId = statusRequest.ExternalId,
				SmsGateId = statusRequest.SmsGateId,
				GuidId = statusRequest.MessagePackageId
			};

			var json = JsonHelper.ToJson(getStatusRequest);

			var response = await _httpClient.PostTryAsync(uri, json, headers, cancellationToken);
			if (!response.IsSuccessStatusCode)
			{
				throw new MessengerApiException(response);
			}

			var result = JsonHelper.FromJson<MessageResponseModel>(response?.Response);
			return result.ToSendingStatusApiModel();
		}

		protected async Task<GetTokenResponseModel> GetTokenAsync(CancellationToken cancellationToken)
		{
			var host = _vostokMessengerSettings.Url;
			var uri = new Uri($"{host}/token");

			var getTokenRequest = new GetTokenRequest()
			{
				Client = _vostokMessengerSettings.Login,
				Password = _vostokMessengerSettings.Password
			};
			var json = JsonHelper.ToJson(getTokenRequest);

			var response = await _httpClient.PostTryAsync(uri, json, cancellationToken);
			if (!response.IsSuccessStatusCode)
			{
				throw new MessengerApiException(response);
			}

			var result = JsonHelper.FromJson<GetTokenResponseModel>(response?.Response);
			return result;
		}
	}
}
