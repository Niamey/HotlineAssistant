using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Data.Repository.References.Managers;
using Vostok.Hotline.Gateway.Client.Countries.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Countries.ViewModels;
using Vostok.Hotline.Gateway.Client.Countries.Mapper;

namespace Vostok.Hotline.Gateway.Client.Countries.Services
{
	public class CountryService
	{
		private readonly CountryManager _countryManager;

		public CountryService(CountryManager countryManager)
		{
			_countryManager = countryManager;
		}

		public async Task<SearchRowsResponseRowModel<CountryViewModel>> GetCountriesAsync(CountryListQuery request, CancellationToken cancellationToken)
		{
			
			var result = await _countryManager.GetCountriesAsync(cancellationToken);

			return result.ToCardListViewModel();
		}
	}
}
