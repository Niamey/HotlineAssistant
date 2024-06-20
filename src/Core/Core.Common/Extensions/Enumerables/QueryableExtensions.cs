using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vostok.Hotline.Core.Common.Expressions;

namespace Vostok.Hotline.Core.Common.Extensions.Enumerables
{
	public static class QueryableExtensions
	{
		public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string sortingColumn, bool isDescending)
		{
			var sortingLambda = ExpressionHelper.CreateSortingExpression<T>(sortingColumn);

			return ExpressionHelper.ApplyOrderBy(query, sortingLambda, isDescending);
		}
	}
}
