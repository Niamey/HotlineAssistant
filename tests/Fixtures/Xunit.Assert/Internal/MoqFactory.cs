using System;
using Moq;

namespace Xunit.Internal
{
	static class MoqFactory
	{
		public static Mock Create(Type type)
		{
			var mockInfo = typeof(Mock<>).MakeGenericType(type);

			return (Mock)Activator.CreateInstance(mockInfo);
		}
	}
}