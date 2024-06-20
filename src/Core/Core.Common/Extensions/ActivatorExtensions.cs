using System;
using System.Linq;
using System.Reflection;

namespace Vostok.Hotline.Core.Common.Extensions
{
	public static class ActivatorExtensions
	{
		public static object CreateInstance(this IServiceProvider provider, Type type)
		{
			ConstructorInfo constructor = type.GetConstructors()[0];
			if (constructor != null)
			{
				object[] args = constructor
					.GetParameters()
					.Select(o => o.ParameterType)
					.Select(o => provider.GetService(o))
					.ToArray();

				return Activator.CreateInstance(type, args);
			}

			return null;
		}
	}
}
