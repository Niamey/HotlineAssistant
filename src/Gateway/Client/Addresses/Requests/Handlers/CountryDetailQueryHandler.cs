using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Addresses.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Addresses.Services;
using Vostok.Hotline.Gateway.Client.Addresses.Services.SearchRequests;
using Vostok.Hotline.Gateway.Client.Addresses.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Addresses.Requests.Handlers
{
	public class CountryDetailQueryHandler : IRequestHandler<CountryDetailQuery, CountryViewModel>
	{
		private readonly CountryService _countryService;

		public CountryDetailQueryHandler(CountryService countryService)
		{
			_countryService = countryService;
		}

		public async Task<CountryViewModel> Handle(CountryDetailQuery request, CancellationToken cancellationToken)
		{
			var searchRequest = new CountryDetailRequest
			{
				CountryId = request.CountryId,
				NoExceptionForNotFound = true
			};

			return await _countryService.GetCountryDetailAsync(searchRequest, cancellationToken);
		}
	}
}
