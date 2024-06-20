using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Services.Abstractions;

namespace Vostok.Hotline.Api.Services.Managers
{
	public class SharedLinkApiManager
	{
		private readonly ISharedLinkApiService _sharedLinkApiService;
		public SharedLinkApiManager(ISharedLinkApiService sharedLinkApiService)
		{
			_sharedLinkApiService = sharedLinkApiService;
		}

		public async Task<string> GetEncodedLinkAsync(string url, string fileName, CancellationToken cancellationToken, int? timeToSecondLimit = null, bool displayMode = true)
		{
			var result = await _sharedLinkApiService.GetEncodedLinkAsync(url, fileName, timeToSecondLimit, displayMode, cancellationToken);

			return result;
		}
	}
}
