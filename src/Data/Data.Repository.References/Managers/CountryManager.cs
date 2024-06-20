using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Vostok.Hotline.Api.References.Managers;
using Vostok.Hotline.Data.Repository.References.Managers.Base;
using Vostok.Hotline.Data.Repository.References.Models.Responses;

namespace Vostok.Hotline.Data.Repository.References.Managers
{
	public class CountryManager : BaseReferencesManager
	{
		private const string _cacheKey = "Vostok.Hotline.Data.Repository.References.Managers.CountryManager";
		private readonly IMemoryCache _memoryCache;
		private readonly CountryApiManager _countryApiManager;
		public CountryManager(IServiceProvider serviceProvider, CountryApiManager countryApiManager, IMemoryCache memoryCache)
			: base(serviceProvider)
		{
			_memoryCache = memoryCache;
			_countryApiManager = countryApiManager;
		}

		public async Task<CountryCollectionResponseModel> GetCountriesAsync(CancellationToken cancellationToken)
		{
			string keyPath = $"{_cacheKey}.{nameof(GetCountriesAsync)}";
			CountryCollectionResponseModel result;
			if (_memoryCache.TryGetValue(keyPath, out result))
				return result;

			var countryCollection = await _countryApiManager.GetCountriesAsync(cancellationToken);
			var countrySettings = await DbContext.CountrySettings.ToListAsync(cancellationToken);

			result = new CountryCollectionResponseModel();
			foreach (var country in countryCollection)
			{
				result.Add(new CountryResponseModel()
				{
					CountryId = country.CountryId,
					CountryA2 = country.CountryA2,
					CountryA3 = country.CountryA3,
					CountryName = country.CountryName,
					CountryCode = country.CountryCode,
					IsCountryRisk = countrySettings?.FirstOrDefault(x => x.CountryCode == country.CountryCode)?.IsCountryRisk ?? false,
				});
			}

			_memoryCache.Set(keyPath, result, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));

			return result;
		}

		public async Task<CountryResponseModel> GetCountryByIdAsync(int countryId, CancellationToken cancellationToken)
		{
			var countries = await GetCountriesAsync(cancellationToken);
			
			return countries.FirstOrDefault(x => x.CountryId == countryId);
		}

		public async Task<CountryResponseModel> GetCountryByCodeAsync(string countryCode, CancellationToken cancellationToken)
		{
			var countries = await GetCountriesAsync(cancellationToken);

			return countries.FirstOrDefault(x => x.CountryCode == countryCode);
		}
	}
}