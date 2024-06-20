using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Base.Abstractions;
using Vostok.Hotline.Api.Retail.ProviderClients.Configurations;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Models.HttpClientModels;

namespace Vostok.Hotline.Api.Retail.ProviderClients
{
	internal class RetailSystemInformationProviderClient : KeyCloakHttpBaseClient
	{
		public RetailSystemInformationProviderClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
		{
		}

		protected override string KeyClockConfigName => nameof(RetailSystemInformationConfig);


		public override async Task<string> PostAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, MediaContentTypeEnum mediaType, int timeoutSeconds, CancellationToken cancellationToken)
		{
			logger.Value.LogInformation($"{nameof(PostAsync)} {requestUri}", data);

			using var client = CreateInstance(headers, timeoutSeconds);
			using var content = new StringContent(data, Encoding.UTF8, mediaType.GetDescription());
			using var response = await client.PostAsync(requestUri, content, cancellationToken);
			return await CheckResponseAsync<string>(response);
		}

		public override async Task<HttpClientResponseModel> PostTryAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, MediaContentTypeEnum mediaType, int timeoutSeconds, CancellationToken cancellationToken)
		{
			logger.Value.LogInformation($"{nameof(PostTryAsync)} {requestUri}", data);

			using var client = CreateInstance(headers, timeoutSeconds);
			using var content = new StringContent(data, Encoding.UTF8, mediaType.GetDescription());
			using var response = await client.PostAsync(requestUri, content, cancellationToken);
			return await CheckResponseAsync<HttpClientResponseModel>(response, requestUri, data);

		}

		public override async Task<string> GetAsync(Uri requestUri, int timeoutSeconds, CancellationToken cancellationToken)
		{
			logger.Value.LogInformation($"{nameof(GetAsync)} {requestUri}");

			using var client = CreateInstance(timeoutSeconds);
			using var response = await client.GetAsync(requestUri, cancellationToken);
			return await CheckResponseAsync<string>(response);
		}

		public override async Task<HttpClientResponseModel> GetTryAsync(Uri requestUri, HttpClientHeaderCollectionModel headers, int timeoutSeconds, CancellationToken cancellationToken)
		{
			logger.Value.LogInformation($"{nameof(GetTryAsync)} {requestUri}");

			using var client = CreateInstance(headers, timeoutSeconds);
			using var response = await client.GetAsync(requestUri, cancellationToken);
			return await CheckResponseAsync<HttpClientResponseModel>(response, requestUri);
		}

		public override async Task<string> PutAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, MediaContentTypeEnum mediaType, int timeoutSeconds, CancellationToken cancellationToken)
		{
			logger.Value.LogInformation($"{nameof(PutAsync)} {requestUri}", data);

			using var client = CreateInstance(headers, timeoutSeconds);
			using var content = new StringContent(data, Encoding.UTF8, mediaType.GetDescription());
			using var response = await client.PutAsync(requestUri, content, cancellationToken);
			return await CheckResponseAsync<string>(response);
		}

		public override async Task<HttpClientResponseModel> PutTryAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, MediaContentTypeEnum mediaType, int timeoutSeconds, CancellationToken cancellationToken)
		{
			logger.Value.LogInformation($"{nameof(PutTryAsync)} {requestUri}", data);

			using var client = CreateInstance(headers, timeoutSeconds);
			using var content = new StringContent(data, Encoding.UTF8, mediaType.GetDescription());
			using var response = await client.PutAsync(requestUri, content, cancellationToken);
			return await CheckResponseAsync<HttpClientResponseModel>(response, requestUri, data);
		}

		private async Task<T> CheckResponseAsync<T>(HttpResponseMessage response, Uri requestUri, string data)
		{
			if (response.IsSuccessStatusCode)
			{
				byte[] resByte = await response.Content.ReadAsByteArrayAsync();
				if (typeof(T) == typeof(string))
				{
					return (T)(object)Encoding.UTF8.GetString(resByte);
				}
				if (typeof(T) == typeof(HttpClientResponseModel))
				{
					string result = Encoding.UTF8.GetString(resByte);

					return (T)(object)new HttpClientResponseModel
					{
						RequestUrl = requestUri,
						Request = data,
						Response = result,
						StatusCode = response.StatusCode,
						IsSuccessStatusCode = response.IsSuccessStatusCode
					};
				}
			}
			var ex = new Exception(await response.Content.ReadAsStringAsync());
			if (response.StatusCode == HttpStatusCode.NotFound)
			{
				throw new NotFoundRequestException(ex.Message);
			}
			throw new RetailApiFailedException(ex);
		}

		private async Task<T> CheckResponseAsync<T>(HttpResponseMessage response, Uri requestUri)
		{
			return await CheckResponseAsync<T>(response, requestUri, null);
		}

		private async Task<T> CheckResponseAsync<T>(HttpResponseMessage response)
		{
			return await CheckResponseAsync<T>(response, null, null);
		}
	}
}