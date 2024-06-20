using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.Responses.SharedLink;
using Vostok.Hotline.Api.Services.SearchRequests;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Managers;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Api.Services.ApiServices
{
	internal class SharedLinkApiService : ISharedLinkApiService
	{
		private readonly HotlineHttpClient _httpClient;
		private readonly EnvironmentManager _environmentManager;


		internal SharedLinkApiService(IServiceProvider serviceProvider)
		{
			_httpClient = serviceProvider.GetRequiredService<HotlineHttpClient>();
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
		}

		public async Task<string> GetEncodedLinkAsync(string url, string fileName, int? timeToSecondLimit, bool displayMode, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.SharedLink.EncodedLinkUrl, cancellationToken);
			var uri = new Uri($"{host}");

			var shLinkRequest = new SharedLinkSearchRequest(url, fileName, timeToSecondLimit, displayMode);
			var json = JsonHelper.ToJson(shLinkRequest);

			var response = await _httpClient.PostTryAsync(uri, json, cancellationToken);
			if (!response.IsSuccessStatusCode)
				throw new FailedRequestException(response);

			var result = JsonHelper.FromJson<SharedLinkResponseModel>(response?.Response);

			return result.Link;
		}
	}
}
