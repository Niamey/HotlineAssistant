using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Gateway.Client.Addresses.Services;
using Vostok.Hotline.Gateway.Client.Addresses.Services.SearchRequests;
using Vostok.Hotline.Gateway.Client.Addresses.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Addresses.Managers
{
	public class CountryManager
	{
		private readonly CountryService _countryService;

		public CountryManager(CountryService countryService)
		{
			_countryService = countryService;
		}

		public async Task<CountryViewModel> GetCountryAsync(int? countryId, CancellationToken cancellationToken)
		{
			var request = new CountryDetailRequest
			{
				CountryId = countryId,
				NoExceptionForNotFound = true
			};

			var response = await _countryService.GetCountryDetailAsync(request, cancellationToken);
			return response;
		}
	}	
}