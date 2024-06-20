using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Gateway.Client.Addresses.Services.Models.Responses;
using Vostok.Hotline.Gateway.Client.Addresses.Services.SearchRequests;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Managers;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Gateway.Client.Addresses.ViewModels;
using Vostok.Hotline.Gateway.Client.Addresses.Mapper;

namespace Vostok.Hotline.Gateway.Client.Addresses.Services
{
	public class CountryService
	{
		private readonly HttpClientManager _httpClient;
		private readonly HotlineEnvironment _environment;
		
		public CountryService(HttpClientManager httpClient, HotlineEnvironment environment)
		{
			_httpClient = httpClient;
			_environment = environment;
		}

		public async Task<CountryViewModel> GetCountryDetailAsync(CountryDetailRequest request, CancellationToken cancellationToken)
		{
			var host = _environment.GetEnvironmentVariable("ApiEndpoint.CountryApi.GetCountryUrl");
			var query = request.GetUrlQuery();
			var uri = new Uri($"{host}?{query}");

			var jsonResult = await _httpClient.GetAsync(uri, cancellationToken);

			var result = JsonHelper.FromJson<CountryResponseModel>(jsonResult);
			if (result != null)
				return result.ToCountryViewModel();
			else
			{
				if (!(request?.NoExceptionForNotFound ?? false))
					throw new NotFoundRequestException();
				else
					return null;
			}
		}
	}
}
