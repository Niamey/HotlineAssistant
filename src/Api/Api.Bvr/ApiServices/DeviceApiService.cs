using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Bvr.Abstractions;
using Vostok.Hotline.Api.Bvr.Mapper;
using Vostok.Hotline.Api.Bvr.Models;
using Vostok.Hotline.Api.Bvr.Responses;
using Vostok.Hotline.Api.Bvr.SearchRequests;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Managers;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Api.Bvr.ApiServices
{
	internal class DeviceApiService : IDeviceApiService
	{
		private readonly HotlineHttpClient _httpClient;
		private readonly EnvironmentManager _environmentManager;

		internal DeviceApiService(IServiceProvider serviceProvider)
		{
			_httpClient = serviceProvider.GetRequiredService<HotlineHttpClient>();
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
		}

		public async Task<DeviceApiModel> GetActiveDeviceAsync(string phoneNumber, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.BvrApi.GetActiveDeviceUrl, cancellationToken);
			var query = new DeviceSearchRequest(phoneNumber).GetUrlQuery();

			var uri = new Uri($"{host}?{query}");

			var response = await _httpClient.GetTryAsync(uri, cancellationToken);
			if (response.IsSuccessStatusCode)
			{
				var result = JsonHelper.FromJson<DeviceResponseModel>(response.Response);

				return result?.ToDeviceApiModel();
			}
			else
				return null;
		}

		public async Task<StatusCommandApiViewModel> ResetPINDeviceAsync(string phoneNumber, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.BvrApi.GetResetPINDeviceUrl, cancellationToken);
			var query = new ResetPINDeviceRequest(phoneNumber).GetUrlQuery();

			var uri = new Uri($"{host}/{query}");

			var response = await _httpClient.GetTryAsync(uri, cancellationToken);

			return new StatusCommandApiViewModel()
			{
				Success = response.IsSuccessStatusCode,
				Message = response.IsSuccessStatusCode ? "PIN мобільного додатку успішно скинутий" : "Помилка при скиданні PIN мобільного додатка"
			};
		}
	}
}
