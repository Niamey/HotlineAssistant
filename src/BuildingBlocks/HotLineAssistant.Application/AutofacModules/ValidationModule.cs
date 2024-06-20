using Autofac;
using FluentValidation;
using System.Reflection;

namespace Vostok.HotLineAssistant.Application.AutofacModules
{
	public class ValidationModule : Autofac.Module
	{
		private readonly Assembly[] _assemblyTypes;

		public ValidationModule(params Assembly[] assemblyTypes)
		{
			_assemblyTypes = assemblyTypes;
		}

		protected override void Load(ContainerBuilder builder)
		{
			builder
				.RegisterAssemblyTypes(_assemblyTypes)
				.Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
				.AsImplementedInterfaces();
		}
	}
}