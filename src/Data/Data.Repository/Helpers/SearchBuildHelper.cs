using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Core.Common.Extensions.Enumerables;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Hotline.Data.Repository.Solar")]
[assembly: InternalsVisibleTo("Hotline.Data.Repository.Core")]

namespace Vostok.Hotline.Data.Repository.Helpers
{
	internal class SearchBuildHelper
	{
		private const int _defaultPageSize = 10;
		private const int _maxPageSize = 200;

		public static IOrderedQueryable<TRowViewModel> ApplySorting<TRowViewModel, T>(
			IQueryable<TRowViewModel> query,
			Expression<Func<TRowViewModel, T>> defaultSortColumn,
			ISearchRequestModel request,
			string[] sortableColumnNames = null,
			bool isDefaultDescending = false)
		{
			var sorting = (request as ISearchRequestPagedModel)?.Sorting;
			if (string.IsNullOrEmpty(sorting?.Column) || (sortableColumnNames != null && !sortableColumnNames.Contains(sorting?.Column)))
			{
				return isDefaultDescending ? query.OrderByDescending(defaultSortColumn) : query.OrderBy(defaultSortColumn);
			}
			else
			{
				return query.OrderBy(sorting.Column, sorting.IsDescending);
			}
		}

		public static async Task<SearchPagedResponseRowModel<TRowViewModel>> BuildResponseAsync<TRowViewModel>(
				IQueryable<TRowViewModel> query,
				ISearchRequestModel request,
				CancellationToken cancellationToken)
			where TRowViewModel : ResponseBaseModel
		{
			var response = new SearchPagedResponseRowModel<TRowViewModel>();

			await ReadRecordsAsync(response, query, request, cancellationToken);

			return response;
		}

		public static async Task ReadRecordsAsync<TRowViewModel>(
				SearchPagedResponseRowModel<TRowViewModel> targetResponse,
				IQueryable<TRowViewModel> query,
				ISearchRequestModel request,
				CancellationToken cancellationToken)
			where TRowViewModel : ResponseBaseModel
		{
			var paging = (request as ISearchRequestPagedModel)?.Paging;

			var skipTake = ConvertPagingToSkipTakeValues(paging);
			if (skipTake == null)
			{
				targetResponse.Rows = await query.AsNoTracking().ToListAsync(cancellationToken);
				targetResponse.IsNextPageAvailable = false;
			}
			else
			{
				var (skip, take) = skipTake.Value;

				var records = await query.AsNoTracking().Skip(skip).Take(take + 1).ToListAsync(cancellationToken);

				var isNextPageAvailable = records.Count > take;
				if (isNextPageAvailable)
				{
					records.RemoveAt(records.Count - 1);
				}

				targetResponse.Rows = records;
				targetResponse.IsNextPageAvailable = isNextPageAvailable;
			}
		}

		public static (int Skip, int Take)? ConvertPagingToSkipTakeValues(SearchPagingModel paging)
		{
			int skip, take;

			if (paging == null)
			{
				return null;
			}
			else
			{
				take = paging.PageSize <= 0 ? _defaultPageSize : Math.Min(paging.PageSize, _maxPageSize);
				if (paging.CurrentPage.HasValue)
				{
					paging.CurrentPage--;
				}
				skip = paging.CurrentPage.GetValueOrDefault(0) * take;
			}

			return (skip, take);
		}

		public static (int From, int To, int PageSize)? ConvertPagingToBetweenValues(SearchPagingModel paging)
		{
			int from, to, pageSize;

			if (paging == null)
			{
				return null;
			}
			else
			{
				pageSize = paging.PageSize <= 0 ? _defaultPageSize : Math.Min(paging.PageSize, _maxPageSize);
				to = pageSize * paging.CurrentPage.GetValueOrDefault(1);
				from = to - pageSize + 1;
			}

			return (from, to, pageSize);
		}
	}
}
