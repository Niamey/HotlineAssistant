using System;

namespace Vostok.Hotline.Core.Common.Helpers
{
	public static class ReflectionHelper
	{
		public static TAttribute GetCustomAttribute<TAttribute>(object obj, string fieldName)
			where TAttribute : Attribute
		{
			var attributes = obj.GetType().GetProperty(fieldName).GetCustomAttributes(true);
			if (attributes != null)
				foreach (var attr in attributes)
				{
					if (attr is TAttribute)
						return (TAttribute)attr;
				}

			return null;
		}
	}
}
