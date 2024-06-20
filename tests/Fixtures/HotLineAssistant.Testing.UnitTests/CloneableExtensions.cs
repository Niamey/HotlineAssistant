using System;
using System.Collections.Generic;
using System.Linq;

namespace HotLineAssistant.Testing.UnitTests
{
	internal static class CloneableExtensions
	{
		public static T Clone<T>(this T subject) where T : class, ICloneable
		{
			return subject.Clone() as T;
		}

		public static IEnumerable<T> Clone<T>(this IEnumerable<ICloneable> subjects) where T : class, ICloneable
		{
			return subjects == null ? new List<T>() : subjects.Select(c => c.Clone()).Cast<T>();
		}

		public static IDictionary<TKey, TValue> Clone<TKey, TValue>(this IDictionary<TKey, TValue> subjects) where TValue : class, ICloneable
		{
			if (subjects == null)
				return new Dictionary<TKey, TValue>();

			var content = subjects.Select(kv => new KeyValuePair<TKey, TValue>(kv.Key, kv.Value.Clone<TValue>()));

			return new Dictionary<TKey, TValue>(content);
		}
	}
}
