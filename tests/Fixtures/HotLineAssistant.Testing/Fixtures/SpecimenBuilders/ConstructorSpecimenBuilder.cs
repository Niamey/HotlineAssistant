using AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace HotLineAssistant.Testing.Fixtures.SpecimenBuilders
{
	public class ConstructorSpecimenBuilder<T> : ISpecimenBuilder
	{
		protected readonly Dictionary<string, object> Parameters = new Dictionary<string, object>();

		protected ConstructorSpecimenBuilder()
		{
		}

		public ConstructorSpecimenBuilder(object parameters)
		{
			foreach (var property in parameters.GetType().GetProperties())
			{
				AddParameter(property.Name, property.GetValue(parameters));
			}
		}

		public object Create(object request, ISpecimenContext context)
		{
			var type = typeof(T);
			if (!(request is SeededRequest seededRequest) || !seededRequest.Request.Equals(type))
				return new NoSpecimen();

			var ctor = SelectConstructor(type);
			if (ctor == null)
				return new NoSpecimen();

			return ctor.Invoke(
				BindingFlags.CreateInstance, null, GetConstructorValues(context, ctor), CultureInfo.InvariantCulture);
		}

		private object[] GetConstructorValues(ISpecimenContext context, ConstructorInfo ctor)
		{
			return ctor.GetParameters()
				.Select(p => GetValue(context, p))
				.ToArray();
		}

		protected virtual object GetValue(ISpecimenContext context, ParameterInfo parameter)
		{
			return Parameters.ContainsKey(parameter.Name)
				? Parameters[parameter.Name]
				: context.Resolve(parameter.ParameterType);
		}

		protected virtual ConstructorInfo SelectConstructor(Type type)
		{
			return type
				.GetConstructors(BindingFlags.Instance | BindingFlags.Public)
				.FirstOrDefault(ctor => ctor.GetParameters().All(p => p.ParameterType != type));
		}

		public void AddParameter(string name, object value)
		{
			Parameters.Add(name, value);
		}

    }
}