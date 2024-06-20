using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Abstractions;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.GlobalContext;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Loggers.Abstractions;
using Vostok.Hotline.Core.Common.Models.HttpClientModels;

namespace Vostok.Hotline.Api.Base.Abstractions
{
	public abstract class KeyCloakHttpBaseClient: HotlineHttpBaseClient
	{
		protected readonly Lazy<ILoggingService> logger;
		protected abstract string KeyClockConfigName { get; }

		private IHttpClientFactory _httpClientFactory;

		public KeyCloakHttpBaseClient(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
			logger = new Lazy<ILoggingService>(() =>
			{
				var type = this.GetType();
				var loggerType = typeof(ILoggingService<>).MakeGenericType(type);

				return GlobalHttpContextAccessor.Current.RequestServices.GetService(loggerType) as ILoggingService;
			});
		}

		protected System.Net.Http.HttpClient CreateInstance(int timeoutSeconds)
		{
			return CreateInstance(null, timeoutSeconds);
		}

		protected System.Net.Http.HttpClient CreateInstance(HttpClientHeaderCollectionModel headers, int timeoutSeconds)
		{
			var client = _httpClientFactory.CreateClient(KeyClockConfigName);

			client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);

			if (headers?.Count > 0)
			{
				foreach (var header in headers)
				{
					client.DefaultRequestHeaders.Add(header.Name, header.Value);
				}				
			}

			return client;
		}

		public override async Task<string> PostAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, MediaContentTypeEnum mediaType, int timeoutSeconds, CancellationToken cancellationToken)
		{
			logger.Value.LogInformation($"{nameof(PostAsync)} {requestUri}", data);

			using var client = CreateInstance(headers, timeoutSeconds);
			using var content = new StringContent(data, Encoding.UTF8, mediaType.GetDescription());
			using var response = await client.PostAsync(requestUri, content, cancellationToken);
			if (response.IsSuccessStatusCode)
			{
				byte[] resByte = await response.Content.ReadAsByteArrayAsync();
				string result = Encoding.UTF8.GetString(resByte);
				return result;
			}

			var ex = new Exception(await response.Content.ReadAsStringAsync());

			throw new FailedRequestException("HTTP request error", ex);
		}

		public override async Task<HttpClientResponseModel> PostTryAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, MediaContentTypeEnum mediaType, int timeoutSeconds, CancellationToken cancellationToken)
		{
			logger.Value.LogInformation($"{nameof(PostTryAsync)} {requestUri}", data);

			using var client = CreateInstance(headers, timeoutSeconds);
			using var content = new StringContent(data, Encoding.UTF8, mediaType.GetDescription());
			using var response = await client.PostAsync(requestUri, content, cancellationToken);
			if (response.IsSuccessStatusCode)
			{
				byte[] resByte = await response.Content.ReadAsByteArrayAsync();
				string result = Encoding.UTF8.GetString(resByte);

				return new HttpClientResponseModel
				{
					RequestUrl = requestUri,
					Request = data,
					Response = result,
					StatusCode = response.StatusCode,
					IsSuccessStatusCode = response.IsSuccessStatusCode
				};
			}

			var errorResult = await response.Content.ReadAsStringAsync();
			return new HttpClientResponseModel
			{
				RequestUrl = requestUri,
				Request = data,
				Response = errorResult,
				StatusCode = response.StatusCode,
				IsSuccessStatusCode = response.IsSuccessStatusCode
			};
		}

		public override async Task<string> GetAsync(Uri requestUri, int timeoutSeconds, CancellationToken cancellationToken)
		{
			logger.Value.LogInformation($"{nameof(GetAsync)} {requestUri}");

			using var client = CreateInstance(timeoutSeconds);
			using var response = await client.GetAsync(requestUri, cancellationToken);
			if (response.IsSuccessStatusCode)
			{
				byte[] resByte = await response.Content.ReadAsByteArrayAsync();
				string result = Encoding.UTF8.GetString(resByte);
				return result;
			}

			var ex = new Exception(await response.Content.ReadAsStringAsync());
			if (response.StatusCode == HttpStatusCode.NotFound)
			{
				throw new NotFoundRequestException(ex.Message);
			}

			throw new FailedRequestException("HTTP request error", ex);
		}

		public override async Task<HttpClientResponseModel> GetTryAsync(Uri requestUri, HttpClientHeaderCollectionModel headers, int timeoutSeconds, CancellationToken cancellationToken)
		{
			logger.Value.LogInformation($"{nameof(GetTryAsync)} {requestUri}");

			using var client = CreateInstance(headers, timeoutSeconds);
			using var response = await client.GetAsync(requestUri, cancellationToken);
			if (response.IsSuccessStatusCode)
			{
				byte[] resByte = await response.Content.ReadAsByteArrayAsync();
				string result = Encoding.UTF8.GetString(resByte);

				return new HttpClientResponseModel
				{
					RequestUrl = requestUri,
					Response = result,
					StatusCode = response.StatusCode,
					IsSuccessStatusCode = response.IsSuccessStatusCode
				};
			}

			var errorResult = await response.Content.ReadAsStringAsync();
			return new HttpClientResponseModel
			{
				RequestUrl = requestUri,
				Response = errorResult,
				StatusCode = response.StatusCode,
				IsSuccessStatusCode = response.IsSuccessStatusCode
			};
		}

		public override async Task<string> PutAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, MediaContentTypeEnum mediaType, int timeoutSeconds, CancellationToken cancellationToken)
		{
			logger.Value.LogInformation($"{nameof(PutAsync)} {requestUri}", data);

			using var client = CreateInstance(headers, timeoutSeconds);
			using var content = new StringContent(data, Encoding.UTF8, mediaType.GetDescription());
			using var response = await client.PutAsync(requestUri, content, cancellationToken);
			if (response.IsSuccessStatusCode)
			{
				byte[] resByte = await response.Content.ReadAsByteArrayAsync();
				string result = Encoding.UTF8.GetString(resByte);
				return result;
			}

			var ex = new Exception(await response.Content.ReadAsStringAsync());

			throw new FailedRequestException("HTTP request error", ex);
		}

		public override async Task<HttpClientResponseModel> PutTryAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, MediaContentTypeEnum mediaType, int timeoutSeconds, CancellationToken cancellationToken)
		{
			logger.Value.LogInformation($"{nameof(PutTryAsync)} {requestUri}", data);

			using var client = CreateInstance(headers, timeoutSeconds);
			using var content = new StringContent(data, Encoding.UTF8, mediaType.GetDescription());
			using var response = await client.PutAsync(requestUri, content, cancellationToken);
			if (response.IsSuccessStatusCode)
			{
				byte[] resByte = await response.Content.ReadAsByteArrayAsync();
				string result = Encoding.UTF8.GetString(resByte);

				return new HttpClientResponseModel
				{
					RequestUrl = requestUri,
					Request = data,
					Response = result,
					StatusCode = response.StatusCode,
					IsSuccessStatusCode = response.IsSuccessStatusCode
				};
			}

			var errorResult = await response.Content.ReadAsStringAsync();
			return new HttpClientResponseModel
			{
				RequestUrl = requestUri,
				Request = data,
				Response = errorResult,
				StatusCode = response.StatusCode,
				IsSuccessStatusCode = response.IsSuccessStatusCode
			};
		}
	}
}
