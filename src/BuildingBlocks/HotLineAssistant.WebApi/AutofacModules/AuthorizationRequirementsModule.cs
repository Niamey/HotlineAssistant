using Autofac;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

namespace Vostok.HotLineAssistant.WebApi.AutofacModules
{
	public class AuthorizationRequirementsModule : Autofac.Module
	{
		private readonly Assembly[] _assemblyTypes;

		public AuthorizationRequirementsModule(params Assembly[] assemblyTypes)
		{
			_assemblyTypes = assemblyTypes;
		}

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(_assemblyTypes)
				.As<IAuthorizationHandler>()
				.AsImplementedInterfaces()
				.PreserveExistingDefaults();
		}
	}
}