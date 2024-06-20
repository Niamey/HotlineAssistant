using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Abstractions;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Models.HttpClientModels;

namespace Vostok.Hotline.Core.Common.Managers
{
	public class HotlineHttpClient: HotlineHttpBaseClient
	{
		public override async Task<string> PostAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, MediaContentTypeEnum mediaType, int timeoutSeconds, CancellationToken cancellationToken)
		{
			using var clientHandler = new HttpClientHandler()
			{
				ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
			};

			using var client = new System.Net.Http.HttpClient(clientHandler)
			{
				Timeout = TimeSpan.FromSeconds(timeoutSeconds),
			};

			if (headers?.Count > 0)
			{
				foreach (var header in headers)
				{
					client.DefaultRequestHeaders.Add(header.Name, header.Value);
				}
			}

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
			using var clientHandler = new HttpClientHandler()
			{
				ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
			};
			
			using var client = new System.Net.Http.HttpClient(clientHandler)
			{
				Timeout = TimeSpan.FromSeconds(timeoutSeconds),
			};

			if (headers?.Count > 0)
			{
				foreach (var header in headers)
				{
					client.DefaultRequestHeaders.Add(header.Name, header.Value);
				}
			}

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

			var erroResult = await response.Content.ReadAsStringAsync();
			return new HttpClientResponseModel
			{
				RequestUrl = requestUri,
				Request = data,
				Response = erroResult,
				StatusCode = response.StatusCode,
				IsSuccessStatusCode = response.IsSuccessStatusCode
			};
		}

		public override async Task<string> GetAsync(Uri requestUri, int timeoutSeconds, CancellationToken cancellationToken)
		{
			using var clientHandler = new HttpClientHandler()
			{
				ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
			};

			using var client = new System.Net.Http.HttpClient(clientHandler)
			{
				Timeout = TimeSpan.FromSeconds(timeoutSeconds)
			};

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
			var cookieContainer = new CookieContainer();
			using var clientHandler = new HttpClientHandler()
			{
				ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
				CookieContainer = cookieContainer
			};

			using var client = new System.Net.Http.HttpClient(clientHandler)
			{
				Timeout = TimeSpan.FromSeconds(timeoutSeconds),
			};

			if (headers?.Count > 0)
			{
				foreach (var header in headers)
				{
					client.DefaultRequestHeaders.Add(header.Name, header.Value);
				}
			}

			using var response = await client.GetAsync(requestUri, cancellationToken);
			if (response.IsSuccessStatusCode)
			{
				byte[] resByte = await response.Content.ReadAsByteArrayAsync();
				string result = Encoding.UTF8.GetString(resByte);

				var cookies = cookieContainer.GetCookies(requestUri).Cast<Cookie>().ToList();
			
				return new HttpClientResponseModel
				{
					RequestUrl = requestUri,
					Response = result,
					StatusCode = response.StatusCode,
					IsSuccessStatusCode = response.IsSuccessStatusCode,
					Cookies = cookies
				};
			}

			var erroResult = await response.Content.ReadAsStringAsync();
			return new HttpClientResponseModel
			{
				RequestUrl = requestUri,
				Response = erroResult,
				StatusCode = response.StatusCode,
				IsSuccessStatusCode = response.IsSuccessStatusCode
			};
		}

		public override async Task<string> PutAsync(Uri requestUri, string data, HttpClientHeaderCollectionModel headers, MediaContentTypeEnum mediaType, int timeoutSeconds, CancellationToken cancellationToken)
		{
			using var clientHandler = new HttpClientHandler()
			{
				ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
			};

			using var client = new System.Net.Http.HttpClient(clientHandler)
			{
				Timeout = TimeSpan.FromSeconds(timeoutSeconds),
			};

			if (headers?.Count > 0)
			{
				foreach (var header in headers)
				{
					client.DefaultRequestHeaders.Add(header.Name, header.Value);
				}
			}

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
			using var clientHandler = new HttpClientHandler()
			{
				ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
		    };

			using var client = new System.Net.Http.HttpClient(clientHandler)
			{
				Timeout = TimeSpan.FromSeconds(timeoutSeconds),
			};

			if (headers?.Count > 0)
			{
				foreach (var header in headers)
				{
					client.DefaultRequestHeaders.Add(header.Name, header.Value);
				}
			}

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

			var erroResult = await response.Content.ReadAsStringAsync();
			return new HttpClientResponseModel
			{
				RequestUrl = requestUri,
				Request = data,
				Response = erroResult,
				StatusCode = response.StatusCode,
				IsSuccessStatusCode = response.IsSuccessStatusCode
			};
		}
				
		public async Task<string> SendAsync(Uri requestUri, HttpRequestMessage httpRequestMessage, int timeoutSeconds, CancellationToken cancellationToken)
		{
			var cookieContainer = new CookieContainer();
			using var clientHandler = new HttpClientHandler()
			{
				CookieContainer = cookieContainer,
				ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
				AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
			};

			using var client = new System.Net.Http.HttpClient(clientHandler)
			{			
				Timeout = TimeSpan.FromSeconds(timeoutSeconds),
			};

			using var response = await client.SendAsync(httpRequestMessage, cancellationToken);
			if (response.IsSuccessStatusCode)
			{
				byte[] resByte = await response.Content.ReadAsByteArrayAsync();
				string result = Encoding.UTF8.GetString(resByte);
				return result;
			}

			var ex = new Exception(await response.Content.ReadAsStringAsync());

			throw new FailedRequestException("HTTP request error", ex);
		}	

		public async Task<HttpClientResponseModel> SendTryAsync(Uri requestUri, HttpRequestMessage httpRequestMessage, int timeoutSeconds, CancellationToken cancellationToken)
		{
			var cookieContainer = new CookieContainer();
			using var clientHandler = new HttpClientHandler()
			{
				CookieContainer = cookieContainer,
				ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
				AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
			};

			using var client = new System.Net.Http.HttpClient(clientHandler)
			{
				Timeout = TimeSpan.FromSeconds(timeoutSeconds)
			};			
		
			var response = await client.SendAsync(httpRequestMessage, cancellationToken);
			if (response.IsSuccessStatusCode)
			{
				byte[] resByte = await response.Content.ReadAsByteArrayAsync();
				string result = Encoding.UTF8.GetString(resByte);

				var cookies = cookieContainer.GetCookies(requestUri).Cast<Cookie>().ToList();

				return new HttpClientResponseModel
				{
					RequestUrl = requestUri,
					Request = httpRequestMessage,
					Response = result,
					ResponseRaw = resByte,
					StatusCode = response.StatusCode,
					IsSuccessStatusCode = response.IsSuccessStatusCode,
					Cookies = cookies
				};
			}

			var erroResult = await response.Content.ReadAsStringAsync();
			return new HttpClientResponseModel
			{
				RequestUrl = requestUri,
				Request = httpRequestMessage,
				Response = erroResult,
				StatusCode = response.StatusCode,
				IsSuccessStatusCode = response.IsSuccessStatusCode
			};
		}

	}
}