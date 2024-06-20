using System;
using System.Linq;

namespace Vostok.Hotline.Core.Common.Helpers
{
	public static class ObjectHelper
	{
		public static void CopyValues<T>(T source, T target)
		{
			Type t = typeof(T);

			var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

			foreach (var prop in properties)
			{
				var value = prop.GetValue(source, null);
				if (value != null)
					prop.SetValue(target, value, null);
			}
		}
	}
}
