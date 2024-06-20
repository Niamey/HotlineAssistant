using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Core.Common.Models.HttpClientModels;

namespace Vostok.Hotline.Core.Common.Abstractions
{
	public abstract class HotlineHttpBaseClient : IHotlineHttpClient
	{
		public const int DefaultTimeout = 60;

		public abstract Task<string> GetAsync(Uri requestUri, int timeoutSeconds, CancellationToken cancellationToken);

		public async Task<string> GetAsync(Uri requestUri, CancellationToken cancellationToken)
		{
			return await GetAsync(requestUri, DefaultTimeout, cancellationToken);
		}

		public abstract Task<HttpClientResponseModel> GetTryAsync(Uri requestUri, HttpClientHeaderCollectionModel headers, int timeoutSeconds, CancellationToken cancellationToken);

		public async Task<HttpClientResponseModel> GetTryAsync(Uri requestUri, CancellationToken cancellationToken)
		{
			return await GetTryAsync(requestUri, new HttpClientHeaderCollectionModel(), DefaultTimeout, cancellationToken);
		}

		public async Task<HttpClientResponseModel> GetTryAsync(Uri requestUri, HttpClientHeaderCollectionModel headers, CancellationToken cancellationToken)
		{
			return await GetTryAsync(requestUri, headers, DefaultTimeout, cancellationToken);
		}

		public abstract Task<string> PostAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, MediaContentTypeEnum mediaType, int timeoutSeconds, CancellationToken cancellationToken);

		public async Task<string> PostAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, CancellationToken cancellationToken)
		{
			return await PostAsync(requestUri, data, headers, MediaContentTypeEnum.ApplicationJson, DefaultTimeout, cancellationToken);
		}

		public async Task<string> PostAsync(Uri requestUri, string data, CancellationToken cancellationToken)
		{
			return await PostAsync(requestUri, data, null, MediaContentTypeEnum.ApplicationJson, DefaultTimeout, cancellationToken);
		}

		public abstract Task<HttpClientResponseModel> PostTryAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, MediaContentTypeEnum mediaType, int timeoutSeconds, CancellationToken cancellationToken);

		public async Task<HttpClientResponseModel> PostTryAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, CancellationToken cancellationToken)
		{
			return await PostTryAsync(requestUri, data, headers, MediaContentTypeEnum.ApplicationJson, DefaultTimeout, cancellationToken);
		}

		public async Task<HttpClientResponseModel> PostTryAsync(Uri requestUri, string data, CancellationToken cancellationToken)
		{
			return await PostTryAsync(requestUri, data, null, MediaContentTypeEnum.ApplicationJson, DefaultTimeout, cancellationToken);
		}

		public abstract Task<string> PutAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, MediaContentTypeEnum mediaType, int timeoutSeconds, CancellationToken cancellationToken);

		public async Task<string> PutAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, CancellationToken cancellationToken)
		{
			return await PutAsync(requestUri, data, headers, MediaContentTypeEnum.ApplicationJson, DefaultTimeout, cancellationToken);
		}

		public async Task<string> PutAsync(Uri requestUri, string data, CancellationToken cancellationToken)
		{
			return await PutAsync(requestUri, data, null, MediaContentTypeEnum.ApplicationJson, DefaultTimeout, cancellationToken);
		}

		public abstract Task<HttpClientResponseModel> PutTryAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, MediaContentTypeEnum mediaType, int timeoutSeconds, CancellationToken cancellationToken);

		public async Task<HttpClientResponseModel> PutTryAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, CancellationToken cancellationToken)
		{
			return await PutTryAsync(requestUri, data, headers, MediaContentTypeEnum.ApplicationJson, DefaultTimeout, cancellationToken);
		}

		public async Task<HttpClientResponseModel> PutTryAsync(Uri requestUri, string data, CancellationToken cancellationToken)
		{
			return await PutTryAsync(requestUri, data, null, MediaContentTypeEnum.ApplicationJson, DefaultTimeout, cancellationToken);
		}
	}
}
