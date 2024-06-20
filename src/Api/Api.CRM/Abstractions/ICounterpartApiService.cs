using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Api.CRM.Responses.SearchRequests;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Api.CRM.Abstractions
{
	public interface ICounterpartApiService
	{
		Task<CounterpartCountApiModel> GetTotalCountAsync(CounterpartCountRequest request, CancellationToken cancellationToken);
		Task<SearchPagedResponseRowModel<CounterpartApiModel>> SearchAsync(CounterpartRequest request, CancellationToken cancellationToken);
	}
}