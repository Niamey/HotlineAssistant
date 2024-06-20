using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vostok.Hotline.Core.Common.Extensions
{
	public static class DictionaryExtensions
	{
		public static void Merge<TKey, TValue>(this Dictionary<TKey, TValue> first, Dictionary<TKey, TValue> second)
		{
			foreach (var item in second)
			{
				if (!first.ContainsKey(item.Key))
					first.Add(item.Key, item.Value);
			}
		}

		public static Dictionary<TKey, TValue> ConcatDict<TKey, TValue>(this Dictionary<TKey, TValue> first, Dictionary<TKey, TValue> second)
		{
			return first.Concat(second).ToDictionary(x => x.Key, x => x.Value);
		}
	}
}
