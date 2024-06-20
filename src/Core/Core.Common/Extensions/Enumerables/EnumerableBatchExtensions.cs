using System;
using System.Collections.Generic;
using System.Linq;

namespace Vostok.Hotline.Core.Common.Extensions.Enumerables
{
	public static class EnumerableBatchExtensions
	{
		public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(
		  this IEnumerable<TSource> source, int size)
		{
			return Batch(source, size, x => x);
		}

		public static IEnumerable<TResult> Batch<TSource, TResult>(
				  this IEnumerable<TSource> source, int size, Func<IEnumerable<TSource>, TResult> selector)
		{
			TSource[] bucket = null;
			var count = 0;

			foreach (var item in source)
			{
				if (bucket == null)
					bucket = new TSource[size];

				bucket[count++] = item;
				if (count != size)
					continue;

				yield return selector(bucket.Select(x => x));

				bucket = null;
				count = 0;
			}

			if (bucket != null && count > 0)
				yield return selector(bucket.Take(count));
		}
	}
}
