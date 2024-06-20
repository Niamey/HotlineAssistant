using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vostok.Hotline.Core.Common.Extensions
{
	public static class ConcurrentDictionaryExtensions
	{
		public static Task<TValue> GetOrAddAsync<TKey, TValue>(
			this ConcurrentDictionary<TKey, Task<TValue>> source, 
			TKey key,
			Func<TKey, Task<TValue>> valueFactory)
		{
			if (!source.TryGetValue(key, out var currentTask))
			{
				Task<TValue> newTask = null;
				var newTaskTask = new Task<Task<TValue>>(async () =>
				{
					try { return await valueFactory(key).ConfigureAwait(false); }
					catch
					{
						((ICollection<KeyValuePair<TKey, Task<TValue>>>)source)
							.Remove(new KeyValuePair<TKey, Task<TValue>>(key, newTask));
						throw;
					}
				});
				newTask = newTaskTask.Unwrap();
				currentTask = source.GetOrAdd(key, newTask);
				if (currentTask == newTask)
					newTaskTask.RunSynchronously(TaskScheduler.Default);
			}
			return currentTask;
		}
	}
}
