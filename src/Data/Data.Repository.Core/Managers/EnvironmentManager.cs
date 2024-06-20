using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Data.Repository.Core.Managers.Base;

namespace Vostok.Hotline.Data.Repository.Core.Managers
{
	public class EnvironmentManager : BaseCoreManager
	{
		private const string _cacheKey = "Vostok.Hotline.Data.Repository.Core.Managers.EnvironmentManager";

		private readonly HotlineEnvironment _environment;
		private readonly IMemoryCache _memoryCache;
		private static SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

		public EnvironmentManager(IServiceProvider serviceProvider)
			: base(serviceProvider)
		{
			_environment = serviceProvider.GetRequiredService<HotlineEnvironment>();
			_memoryCache = serviceProvider.GetRequiredService<IMemoryCache>();
		}

		public async Task<string> GetSettingValueAsync(string key, CancellationToken cancellationToken)
		{
			string findKey = $"{_environment.EnvironmentName}:{key}";
			string keyPath = $"{_cacheKey}.{nameof(GetSettingValueAsync)}.{findKey}";

			string result;
			if (_memoryCache.TryGetValue(keyPath, out result))
				return result;

			var collection = await GetSettingsValueAsync(cancellationToken);
			collection.TryGetValue(findKey, out result);

			_memoryCache.Set(keyPath, result, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));

			return result;
		}

		private async Task<Dictionary<string, string>> GetSettingsValueAsync(CancellationToken cancellationToken)
		{
			string keyPath = $"{_cacheKey}.{nameof(GetSettingsValueAsync)}";

			Dictionary<string, string> result;
			if (_memoryCache.TryGetValue(keyPath, out result))
				return result;

			await _semaphoreSlim.WaitAsync(cancellationToken);
			try
			{
				if (_memoryCache.TryGetValue(keyPath, out result))
					return result;

				result = await DbContext.EnvironmentSettings.ToDictionaryAsync(x=>x.EnvironmentKey, x=>x.EnvironmentValue);

				_memoryCache.Set(keyPath, result, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));
			}
			finally
			{
				_semaphoreSlim.Release();
			}
			
			return result;
		}
	}
}
