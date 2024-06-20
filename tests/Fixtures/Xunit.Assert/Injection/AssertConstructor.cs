using System;
using System.Linq;
using System.Reflection;
using Moq;
using Xunit.Sdk;

namespace Xunit.Injection
{
	public class AssertConstructor
	{
		private readonly Type _type;
		private readonly ParameterInfo[] _parameters;

		internal AssertConstructor(Type type)
		{
			_type = type;
			_parameters = _type.GetConstructors().Single().GetParameters();
		}

		public AssertConstructor HasNullGuard(string parameterName)
		{
			var parameters = CreateParameters(parameterName);

			try
			{
				var e = Assert.ThrowsAny<TargetInvocationException>(() =>
				{
					var unused = Activator.CreateInstance(_type, parameters);
				});

				var error = Assert.IsType<ArgumentNullException>(e.InnerException);
				Assert.Equal(parameterName, error.ParamName);
			}
			catch (XunitException)
			{
				Assert.True(false, $"Does not throw on null value for {parameterName}.");
			}

			return this;
		}

		public AssertConstructor HasNullGuard()
		{
			foreach (var parameter in _parameters)
				HasNullGuard(parameter.Name);

			return this;
		}

		private object[] CreateParameters(string parameterName)
		{
			return _parameters.Select(p => p.Name == parameterName ? null : CreateParameter(p))
				.Select(m => m?.Object)
				.ToArray();
		}

		private static Mock CreateParameter(ParameterInfo p)
		{
			return (Mock)Activator.CreateInstance(typeof(Mock<>).MakeGenericType(p.ParameterType));
		}
	}
}