using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.Mapper.BVR;
using Vostok.Hotline.Api.Services.Responses.BVR;
using Vostok.Hotline.Api.Services.SearchRequests.BVR;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Managers;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Api.Services.ApiServices
{
	public class BVRApiService : IBVRApiService
	{
		private readonly HotlineHttpClient _httpClient;
		private readonly EnvironmentManager _environmentManager;

		internal BVRApiService(IServiceProvider serviceProvider)
		{
			_httpClient = serviceProvider.GetRequiredService<HotlineHttpClient>();
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
		}
		
		public async Task<StatusCommandApiViewModel> AddPermanentBlockingAsync(string phoneNumber, string description, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.BVR.AddPermanentBlockingUrl, cancellationToken);
			

			var uri = new Uri($"{host}");
			var request = new AddPermanentBlockingRequest(phoneNumber, description);
			var json = JsonHelper.ToJson(request);
						
			var jsonResult = await _httpClient.PostTryAsync(uri, json, cancellationToken);
			if (!jsonResult.IsSuccessStatusCode)
				throw new FailedRequestException(jsonResult);

			/*
			var result = JsonHelper.FromJson<AddPermanentBlockingResponseModel>(jsonResult?.Response);
			return result.ToStatusCommandApiViewModel();
			*/

			return new StatusCommandApiViewModel { Success = true };
		}
	}
}
