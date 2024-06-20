using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.Models.Statement;
using Vostok.Hotline.Api.Services.Responses.Statement;
using Vostok.Hotline.Api.Services.SearchRequests.Statement;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Managers;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Api.Services.ApiServices
{
	public class StatementApiService : IStatementApiService
	{
		private readonly HotlineHttpClient _httpClient;
		private readonly EnvironmentManager _environmentManager;

		internal StatementApiService(IServiceProvider serviceProvider)
		{
			_httpClient = serviceProvider.GetRequiredService<HotlineHttpClient>();
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
		}

		public async Task<AuthLogoutApiModel> AuthLogoutAsync(CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.Statement.Host, cancellationToken);
			var logoutUrl = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.Statement.LogoutUrl, cancellationToken);
			var uri = new Uri($"{host}{logoutUrl}");

			var result = await _httpClient.GetTryAsync(uri, cancellationToken);
			if (!result.IsSuccessStatusCode)
				throw new FailedRequestException(result);

			if (result.Cookies == null
				|| !result.Cookies.Exists(x => x.Name == "JSESSIONID")
				|| !result.Cookies.Exists(x => x.Name == "XSRF-TOKEN"))
			{
				throw new NotFoundRequestException();
			}

			return new AuthLogoutApiModel {
				JSessionId = result.Cookies.Find(x => x.Name == "JSESSIONID")?.Value,
				XSRFToken = result.Cookies.Find(x => x.Name == "XSRF-TOKEN")?.Value
			};

		}

		public async Task<AuthLoginApiModel> AuthLoginAsync(string xsrfToken, string jSessionId, CancellationToken cancellationToken)
		{
			var username = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.Statement.Username, cancellationToken);
			var password = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.Statement.Password, cancellationToken);

			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.Statement.Host, cancellationToken);
			var loginUrl = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.Statement.LoginUrl, cancellationToken);
			var uri = new Uri($"{host}{loginUrl}");


			var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

			httpRequestMessage.Headers.Add("Host", uri.Authority);
			httpRequestMessage.Headers.Add("Connection", "keep-alive");
			//   httpRequestMessage.Headers.Add("Content-Length", "138");
			httpRequestMessage.Headers.Add("Pragma", "no-cache");
			httpRequestMessage.Headers.Add("Cache-Control", "no-cache");
			httpRequestMessage.Headers.Add("Origin", host);
			httpRequestMessage.Headers.Add("Upgrade-Insecure-Requests", "1");
			//httpRequestMessage.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
			httpRequestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
			httpRequestMessage.Headers.Add("Accept", " text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
			httpRequestMessage.Headers.Add("Referer", $"{host}{loginUrl}");
			httpRequestMessage.Headers.Add("Accept-Encoding", "gzip, deflate");
			httpRequestMessage.Headers.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
			httpRequestMessage.Headers.Add("Cookie", $"XSRF-TOKEN={xsrfToken}; JSESSIONID={jSessionId}");

			var postParams = new Dictionary<string, string>();
			postParams.Add("_csrf", xsrfToken);
			postParams.Add("username", username);
			postParams.Add("password", password);
			httpRequestMessage.Content = new FormUrlEncodedContent(postParams);

			var result = await _httpClient.SendTryAsync(uri, httpRequestMessage, 60, cancellationToken);
			if (!result.IsSuccessStatusCode)
				throw new FailedRequestException(result);

			return new AuthLoginApiModel
			{
				JSessionId = result.Cookies.Find(x => x.Name == "JSESSIONID")?.Value,
				XSRFToken = result.Cookies.Find(x => x.Name == "XSRF-TOKEN")?.Value
			};
		}

		public async Task<long?> GenerateDocumentAsync(string xsrfToken, string jSessionId, long agreementId, DateTime dateStart, DateTime dateEnd, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.Statement.Host, cancellationToken);
			var genDocUrl = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.Statement.GenerateDocumentUrl, cancellationToken);
			var uri = new Uri($"{host}{genDocUrl}");

			var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
			httpRequestMessage.Headers.Add("Host", uri.Authority);
			httpRequestMessage.Headers.Add("Connection", "keep-alive");
			//   httpRequestMessage.Headers.Add("Content-Length", "138");
			httpRequestMessage.Headers.Add("Pragma", "no-cache");
			httpRequestMessage.Headers.Add("Cache-Control", "no-cache");
			httpRequestMessage.Headers.Add("Origin", host);
			httpRequestMessage.Headers.Add("Upgrade-Insecure-Requests", "1");
			// httpRequestMessage.Headers.Add("Content-Type", "application/json; charset=UTF-8");
			httpRequestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36");
			httpRequestMessage.Headers.Add("Accept", "application/json, text/plain, */*");
			httpRequestMessage.Headers.Add("Referer", $"{host}/solar-air/form/bo-csw.agreement?id={agreementId}");
			httpRequestMessage.Headers.Add("Accept-Encoding", "gzip, deflate, br");
			httpRequestMessage.Headers.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
			httpRequestMessage.Headers.Add("Cookie", $"XSRF-TOKEN={xsrfToken}; JSESSIONID={jSessionId}");
			httpRequestMessage.Headers.Add("X-Requested-With", "XMLHttpRequest");
			httpRequestMessage.Headers.Add("X-XSRF-TOKEN", xsrfToken);

			httpRequestMessage.Headers.Add("Sec-Fetch-Dest", "empty");
			httpRequestMessage.Headers.Add("Sec-Fetch-Mode", "cors");
			httpRequestMessage.Headers.Add("Sec-Fetch-Site", "same-origin");

			var request = new GenerateDocumentRequest {
				Parameters = new GenerateDocumentParams(agreementId, dateStart, dateEnd)
			};
			var json = JsonHelper.ToJson(request);

			httpRequestMessage.Content = new StringContent(json, Encoding.UTF8, MediaContentTypeEnum.ApplicationJson.GetDescription());

			var jsonResult = await _httpClient.SendTryAsync(uri, httpRequestMessage, 60, cancellationToken);
			if (!jsonResult.IsSuccessStatusCode)
				throw new FailedRequestException(jsonResult);

			var result = JsonHelper.FromJson<GenerateDocumentResponseModel>(jsonResult?.Response);
			if (result != null && result.Data.Count > 0)
			{
				return result.Data.First()?.Id;
			} else
				return null;
		}

		public async Task<byte[]> GetDocumentAsync(string xsrfToken, string jSessionId, long agreementId, long genId, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.Statement.Host, cancellationToken);
			var getDocUrl = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.Statement.GetDocumentUrl, cancellationToken);
			getDocUrl = getDocUrl.Replace("{genId}", genId.ToString());
			var uri = new Uri($"{host}{getDocUrl}");

			var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
			httpRequestMessage.Headers.Add("Host", uri.Authority);
			httpRequestMessage.Headers.Add("Connection", "keep-alive");
			//   httpRequestMessage.Headers.Add("Content-Length", "138");
			httpRequestMessage.Headers.Add("Pragma", "no-cache");
			httpRequestMessage.Headers.Add("Cache-Control", "no-cache");
			httpRequestMessage.Headers.Add("Origin", host);
			httpRequestMessage.Headers.Add("Upgrade-Insecure-Requests", "1");
			//httpRequestMessage.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
			httpRequestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
			httpRequestMessage.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
			httpRequestMessage.Headers.Add("Referer", $"{host}/solar-air/form/bo-csw.agreement?id={agreementId}");
			httpRequestMessage.Headers.Add("Accept-Encoding", "gzip, deflate");
			httpRequestMessage.Headers.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
			httpRequestMessage.Headers.Add("Cookie", $"XSRF-TOKEN={xsrfToken}; JSESSIONID={jSessionId}");
		

			//httpRequestMessage.Content = new StringContent(json, Encoding.UTF8, MediaContentTypeEnum.ApplicationJson.GetDescription());

			var result = await _httpClient.SendTryAsync(uri, httpRequestMessage, 60, cancellationToken);
			if (!result.IsSuccessStatusCode)
				throw new FailedRequestException(result);
			
			return result.ResponseRaw;
		}
	}
}
