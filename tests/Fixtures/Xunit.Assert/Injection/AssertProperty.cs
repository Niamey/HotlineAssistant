using System;
using System.Reflection;
using Xunit.Internal;
using Xunit.Sdk;

namespace Xunit.Injection
{
	public class AssertProperty
	{
		private readonly object _testable;

		private PropertyInfo PropertyInfo { get; }

		private object Get() => PropertyInfo.GetMethod.Invoke(_testable, null);
		private void Set(object value) => PropertyInfo.SetMethod.Invoke(_testable, new[] { value });

		internal AssertProperty(object testable, string propertyName)
		{
			_testable = testable;
			PropertyInfo = _testable.GetType().GetProperty(propertyName);
		}

		public AssertProperty HasDefault()
		{
			Assert.False(Get() == null, "Does not have default value.");

			return this;
		}

		public AssertProperty DoesOverride()
		{
			var value = MoqFactory.Create(PropertyInfo.GetMethod.ReturnType).Object;

			Set(value);

			Assert.True(ReferenceEquals(value, Get()), "Does not override default value.");

			return this;
		}

		public AssertProperty HasNullGuard()
		{
			try
			{
				var e = Assert.ThrowsAny<TargetInvocationException>(() => Set(null));
				Assert.IsType<ArgumentNullException>(e.InnerException);
			}
			catch (XunitException)
			{
				Assert.True(false, "Does not throw on null value.");
			}

			return this;
		}
	}
}