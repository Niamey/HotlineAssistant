using System;

namespace Xunit.Injection
{
	public class AssertFactory
	{
		public AssertProperty OfProperty(object testable, string propertyName) => new AssertProperty(testable, propertyName);

		public AssertConstructor OfConstructor(Type type) => new AssertConstructor(type);
	}
}
