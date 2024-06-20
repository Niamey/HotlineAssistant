using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.References.Models;

namespace Vostok.Hotline.Api.References.Abstractions
{
	public interface ICountryApiService
	{
		Task<CountryApiModel> GetCountryAsync(int countryId, CancellationToken cancellationToken);
		Task<CountryCollectionApiModel> GetCountriesAsync(CancellationToken cancellationToken);
	}
}