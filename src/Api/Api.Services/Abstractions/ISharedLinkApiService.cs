using System.Threading;
using System.Threading.Tasks;

namespace Vostok.Hotline.Api.Services.Abstractions
{
	public interface ISharedLinkApiService
	{
		Task<string> GetEncodedLinkAsync(string url, string fileName, int? timeToSecondLimit, bool displayMode, CancellationToken cancellationToken);
	}
}
