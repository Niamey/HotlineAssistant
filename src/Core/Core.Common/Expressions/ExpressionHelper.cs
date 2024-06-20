using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Vostok.Hotline.Core.Common.Extensions.Enumerables;
using Vostok.Hotline.Core.Common.Helpers;

namespace Vostok.Hotline.Core.Common.Expressions
{
	public static class ExpressionHelper
	{
		public static IOrderedQueryable<T> ApplyOrderBy<T>(IQueryable<T> query, LambdaExpression sortingKeyExpression, bool isDescending)
		{
			var type = typeof(T);

			var sortingMethodName = isDescending ? nameof(System.Linq.Queryable.OrderByDescending) : nameof(System.Linq.Queryable.OrderBy);

			var method = typeof(Queryable).GetMethods(BindingFlags.Static | BindingFlags.Public).Single(x => x.Name == sortingMethodName && x.GetParameters().Length == 2);
			var genericMethod = method.MakeGenericMethod(type, sortingKeyExpression.ReturnType);

			var orderedQuery = (IOrderedQueryable<T>)genericMethod.Invoke(null, new object[] { query, sortingKeyExpression });

			return orderedQuery;
		}

		public static LambdaExpression CreateSortingExpression<T>(string field)
		{
			var parameter = Expression.Parameter(typeof(T));
			var memberExpression = Expression.PropertyOrField(parameter, field);

			var isNullable = false;
			var originalMemberType = memberExpression.Type;
			var memberType = memberExpression.Type;

			if (memberType.IsGenericType && memberType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				isNullable = true;
				memberType = Nullable.GetUnderlyingType(memberType);
			}

			if (memberType.IsEnum)
			{
				var enumDictionary = EnumHelper.GetEnumTranslation(memberType);
				var itemsBatches = enumDictionary.OrderBy(x => x.Value).Select((x, idx) => new
				{
					Value = x.Key,
					Index = idx
				}).OrderBy(x => x.Index).Batch(10).Select(x => x.ToList()).ToList();

				var isMultipleBatches = itemsBatches.Count > 1;

				Expression orderByExpression = null;

				foreach (var itemsBatch in itemsBatches)
				{
					ConditionalExpression conditionalExpression = null;

					for (var i = itemsBatch.Count - 1; i >= 1; i--)
					{
						var currentItem = itemsBatch[i];
						var previousItem = itemsBatch[i - 1];

						if (conditionalExpression == null)
						{
							Expression trueExpression = Expression.Constant(previousItem.Index + 1);
							Expression elseExpression = isMultipleBatches ? Expression.Constant(0) : Expression.Constant(currentItem.Index + 1);

							conditionalExpression = Expression.Condition(
										Expression.Equal(memberExpression, isNullable ? (Expression)Expression.Convert(Expression.Constant(currentItem.Value), originalMemberType)
																					  : Expression.Constant(currentItem.Value)),
										trueExpression,
										elseExpression
								);
						}

						Expression trueNextExpression = Expression.Constant(previousItem.Index);
						conditionalExpression = Expression.Condition(
									Expression.Equal(memberExpression, isNullable ? (Expression)Expression.Convert(Expression.Constant(previousItem.Value), originalMemberType) : Expression.Constant(previousItem.Value)),
									trueNextExpression,
									conditionalExpression
						);
					}

					if (isMultipleBatches)
					{
						orderByExpression = orderByExpression != null
							? (Expression)Expression.Add(orderByExpression, conditionalExpression)
							: conditionalExpression;
					}
					else
					{
						orderByExpression = conditionalExpression;
					}
				}

				var lambda = Expression.Lambda<Func<T, int>>(orderByExpression, parameter);

				return lambda;
			}
			else
			{
				var lambda = Expression.Lambda(memberExpression, parameter);

				return lambda;
			}
		}

		public static IOrderedEnumerable<T> ApplyOrderBy<T>(IEnumerable<T> enumerable, LambdaExpression sortingKeyExpression, bool isDescending)
		{
			var type = typeof(T);

			var sortingMethodName = isDescending ? nameof(System.Linq.Enumerable.OrderByDescending) : nameof(System.Linq.Enumerable.OrderBy);

			var method = typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public).Single(x => x.Name == sortingMethodName && x.GetParameters().Length == 2);
			var genericMethod = method.MakeGenericMethod(type, sortingKeyExpression.ReturnType);

			var orderedEnumerable = (IOrderedEnumerable<T>)genericMethod.Invoke(null, new object[] { enumerable, sortingKeyExpression.Compile() });

			return orderedEnumerable;
		}
	}
}
