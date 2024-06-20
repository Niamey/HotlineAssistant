using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vostok.Hotline.Core.Common.Expressions;

namespace Vostok.Hotline.Core.Common.Extensions.Enumerables
{
	public static class EnumerableExtensions
	{
		public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> source, string sortingColumn, bool isDescending)
		{
			var sortingLambda = ExpressionHelper.CreateSortingExpression<T>(sortingColumn);

			return ExpressionHelper.ApplyOrderBy(source, sortingLambda, isDescending);
		}

		public static IEnumerable<JoinResult<TL, TR>> FullOuterJoin<TL, TR, TKey>(this IEnumerable<TL> outerQuery, 
				IEnumerable<TR> innerQuery,
				Func<TL, TKey> outerKeySelector,
				Func<TR, TKey> innerKeySelector)
		{
			var leftResult = outerQuery.LeftOuterJoin(innerQuery, outerKeySelector, innerKeySelector);

			var rightResult = outerQuery.RightOuterJoin(innerQuery, outerKeySelector, innerKeySelector).Where(x => x.Outer == null);

			return leftResult.Concat(rightResult);
		}


		public static IEnumerable<JoinResult<TL, TR>> LeftOuterJoin<TL, TR, TKey>(this IEnumerable<TL> outerQuery, 
			IEnumerable<TR> innerQuery,
			Func<TL, TKey> outerKeySelector, 
			Func<TR, TKey> innerKeySelector)
		{
			var joinedQuery = outerQuery.GroupJoin(
				innerQuery,
				outerKeySelector,
				innerKeySelector,
				(outer, innerCollection) => new
				{
					Outer = outer,
					InnerCollection = innerCollection
				}
			)
			.SelectMany(
				x => x.InnerCollection.DefaultIfEmpty(),
				(x, inner) => new JoinResult<TL, TR>
				{
					Outer = x.Outer,
					Inner = inner
				}
			);

			return joinedQuery;
		}

		public static IEnumerable<JoinResult<TL, TR>> RightOuterJoin<TL, TR, TKey>(this IEnumerable<TL> outerQuery, 
				IEnumerable<TR> innerQuery,
				Func<TL, TKey> outerKeySelector, 
				Func<TR, TKey> innerKeySelector)
		{
			var joinedQuery = innerQuery.GroupJoin(
				outerQuery,
				innerKeySelector,
				outerKeySelector,
				(outer, innerCollection) => new
				{
					Outer = outer,
					InnerCollection = innerCollection
				}
			)
			.SelectMany(
				x => x.InnerCollection.DefaultIfEmpty(),
				(x, inner) => new JoinResult<TL, TR>
				{
					Outer = inner,
					Inner = x.Outer
				}
			);

			return joinedQuery;
		}

		public static IEnumerable<JoinResult<TL, TR>> InnerJoin<TL, TR, TKey>(this IEnumerable<TL> outerQuery, 
			IEnumerable<TR> innerQuery,
			Func<TL, TKey> outerKeySelector, 
			Func<TR, TKey> innerKeySelector)
		{
			var joinedQuery = outerQuery.Join(
				innerQuery,
				outerKeySelector,
				innerKeySelector,
				(outer, inner) => new JoinResult<TL, TR>
				{
					Outer = outer,
					Inner = inner
				}
			);

			return joinedQuery;
		}
	}
}
