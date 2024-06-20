using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.References.Abstractions;
using Vostok.Hotline.Api.References.Models;
using Vostok.Hotline.Core.Common.Extensions;

namespace Vostok.Hotline.Api.References.Managers
{
	public class CountryApiManager
	{
		protected readonly ConcurrentDictionary<string, Task<CountryCollectionApiModel>> countryCollectionCache = new ConcurrentDictionary<string, Task<CountryCollectionApiModel>>();
		protected readonly ConcurrentDictionary<int, Task<CountryApiModel>> currencyCache = new ConcurrentDictionary<int, Task<CountryApiModel>>();
		private readonly ICountryApiService _countryApiService;
		public CountryApiManager(ICountryApiService countryApiService)
		{
			_countryApiService = countryApiService;
		}

		public async Task<CountryApiModel> GetCountryAsync(int countryId, CancellationToken cancellationToken)
		{
			return await currencyCache.GetOrAddAsync(countryId, async countryId =>
			{
				return await _countryApiService.GetCountryAsync(countryId, cancellationToken);
			});			
		}

		public async Task<string> GetCountryNameAsync(int countryId, CancellationToken cancellationToken)
		{
			var result = await GetCountryAsync(countryId, cancellationToken);

			return result?.CountryName;
		}

		public async Task<CountryCollectionApiModel> GetCountriesAsync(CancellationToken cancellationToken)
		{
			var keyPath = "countryList";
			return await countryCollectionCache.GetOrAddAsync(keyPath, async countryList =>
			{
				return await _countryApiService.GetCountriesAsync(cancellationToken);
			});
		}
	}
}
