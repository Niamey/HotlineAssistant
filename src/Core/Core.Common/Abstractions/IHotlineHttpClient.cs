using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Core.Common.Models.HttpClientModels;

namespace Vostok.Hotline.Core.Common.Abstractions
{
	public interface IHotlineHttpClient
	{
		Task<string> PostAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, MediaContentTypeEnum mediaType, int timeoutSeconds, CancellationToken cancellationToken);
		Task<HttpClientResponseModel> PostTryAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, MediaContentTypeEnum mediaType, int timeoutSeconds, CancellationToken cancellationToken);
		Task<string> GetAsync(Uri requestUri, int timeoutSeconds, CancellationToken cancellationToken);
		Task<HttpClientResponseModel> GetTryAsync(Uri requestUri, HttpClientHeaderCollectionModel headers, int timeoutSeconds, CancellationToken cancellationToken);
		Task<string> PutAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, MediaContentTypeEnum mediaType, int timeoutSeconds, CancellationToken cancellationToken);
		Task<HttpClientResponseModel> PutTryAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, MediaContentTypeEnum mediaType, int timeoutSeconds, CancellationToken cancellationToken);
	}
}
