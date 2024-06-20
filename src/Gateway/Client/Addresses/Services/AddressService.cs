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
	public class AddressService
	{
		private readonly HttpClientManager _httpClient;
		private readonly HotlineEnvironment _environment; 
		private readonly AddressMapper _addressMapper;

		public AddressService(HttpClientManager httpClient, HotlineEnvironment environment,
			AddressMapper addressMapper)
		{
			_httpClient = httpClient;
			_environment = environment;
			_addressMapper = addressMapper;
		}
			
		public async Task<AddressViewModel> GetAddressDetailAsync(AddressDetailRequest request, CancellationToken cancellationToken)
		{
			var host = _environment.GetEnvironmentVariable("ApiEndpoint.AddressApi.GetFullAddressUrl");
			var query = request.GetUrlQuery();
			var uri = new Uri($"{host}/{query}");

			var jsonResult = await _httpClient.GetAsync(uri, cancellationToken);

			var result = JsonHelper.FromJson<PostalAddressResponseModel>(jsonResult);
		    if (result != null)
				return _addressMapper.MapToAddressViewModel(result);
			else{
				if (!(request?.NoExceptionForNotFound ?? false))
					throw new NotFoundRequestException();
				else 
					return null;
			}
		}
	}
}