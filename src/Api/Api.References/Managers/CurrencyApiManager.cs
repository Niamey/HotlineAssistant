using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.References.Abstractions;
using Vostok.Hotline.Api.References.Models;
using Vostok.Hotline.Core.Common.Extensions;

namespace Vostok.Hotline.Api.References.Managers
{
	public class CurrencyApiManager
	{
		protected readonly ConcurrentDictionary<string, Task<CurrencyApiModel>> currencyCache = new ConcurrentDictionary<string, Task<CurrencyApiModel>>();
		private readonly ICurrencyApiService _currencyApiService;
		public CurrencyApiManager(ICurrencyApiService currencyApiService)
		{
			_currencyApiService = currencyApiService;
		}

		public async Task<CurrencyCollectionApiModel> GetCurrencyCollectionAsync(string[] currencyCodes, CancellationToken cancellationToken)
		{
			var collection = new CurrencyCollectionApiModel();
			foreach (var currencyCode in currencyCodes)
			{
				var response = await GetCurrencyAsync(currencyCode, cancellationToken);
				if (response != null)
					collection.Add(currencyCode, response);
			}
			return collection;
		}

		public async Task<CurrencyApiModel> GetCurrencyAsync(string currencyCode, CancellationToken cancellationToken)
		{
			return await currencyCache.GetOrAddAsync(currencyCode, async currencyCode =>
			{
				return await _currencyApiService.GetCurrencyAsync(currencyCode, cancellationToken);
			});
		}

		public async Task<string> GetCurrencyShortNameAsync(string currencyCode, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(currencyCode))
				return "";

			var result = await GetCurrencyAsync(currencyCode, cancellationToken);

			return result?.CurrencyShortName;
		}
	}
}
