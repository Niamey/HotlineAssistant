using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.References.Models;

namespace Vostok.Hotline.Api.References.Abstractions
{
	public interface ICurrencyApiService
	{
		public Task<CurrencyApiModel> GetCurrencyAsync(string currencyCode, CancellationToken cancellationToken);
	}
}
