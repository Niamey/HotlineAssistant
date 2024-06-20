using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.CRM.Abstractions;
using Vostok.Hotline.Api.CRM.Mapper;
using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Api.CRM.Responses.Enums;
using Vostok.Hotline.Api.CRM.Responses.Enums.Mapper;
using Vostok.Hotline.Api.CRM.Responses.SearchRequests;
using Vostok.Hotline.Api.CRM.SearchRequests;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.CRM.Managers
{
	public class CounterpartApiManager
	{
		private readonly ICounterpartApiService _counterpartApiService;
		public CounterpartApiManager(ICounterpartApiService counterpartApiService)
		{
			_counterpartApiService = counterpartApiService;
		}

		public async Task<SearchPagedResponseRowModel<CounterpartApiModel>> SearchAsync(CounterpartSearchRequest request, CancellationToken cancellationToken)
		{
			var searchRequest = new CounterpartRequest
			{
				Criteria = request.SearchFilter,
				Type = CounterpartSearchTypeCrmEnum.None,
				Paging = new SearchPagingModel
				{
					CurrentPage = request.PageIndex,
					PageSize = request.PageSize
				}
			};

			if (!string.IsNullOrEmpty(request.SortColumn))
			{
				searchRequest.Sorting = new SearchSortingModel
				{
					Column = request.SortColumn,
					IsDescending = (request.SortDirection ?? SortDirectionEnum.Ascending) == SortDirectionEnum.Ascending ? false : true
				};
			}

			searchRequest.Code = request.SearchType.ToCounterpartSearchCodeEnum();

			return await _counterpartApiService.SearchAsync(searchRequest, cancellationToken);
		}


		public async Task<CounterpartCountApiModel> GetTotalCountAsync(CounterpartCountSearchRequest request, CancellationToken cancellationToken)
		{
			var searchRequest = new CounterpartCountRequest
			{
				Criteria = request.SearchFilter,
				Type = CounterpartSearchTypeCrmEnum.None
			};

			searchRequest.Code = request.SearchType.ToCounterpartSearchCodeEnum();

			return await _counterpartApiService.GetTotalCountAsync(searchRequest, cancellationToken);
		}

		public async Task<CounterpartSearchByPhoneApiModel> SearchFirstByPhoneAsync(CounterpartSearchByPhoneRequest request, CancellationToken cancellationToken)
		{
			var searchRequest = new CounterpartRequest
			{
				Criteria = request.SearchFilter,
				Code = CounterpartSearchCodeCrmEnum.P_FINANCIALPHONE
			};

			var result = await _counterpartApiService.SearchAsync(searchRequest, cancellationToken);
			if (result?.Rows?.Any() ?? false)
				return result.Rows.First().ToCounterpartSearchByPhoneApiModel();
			else
				return null;
		}

		public async Task<CounterpartApiModel> GetBySolarIdAsync(long solarId, CancellationToken cancellationToken)
		{
			var searchRequest = new CounterpartRequest
			{
				Criteria = solarId.ToString(),
				Code = CounterpartSearchCodeCrmEnum.SOLAR_ID
			};

			var result = await _counterpartApiService.SearchAsync(searchRequest, cancellationToken);
			if (result?.Rows?.Any() ?? false)
			{
				return result.Rows.First();
			}
			else
				return null;
		}
	}
}
