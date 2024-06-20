using Autofac;
using MediatR;
using System;
using System.Linq;
using System.Reflection;
using Vostok.HotLineAssistant.Common.Helpers;
using Vostok.HotLineAssistant.WebApi.Authorization;

namespace Vostok.HotLineAssistant.WebApi.AutofacModules
{
	public class AuthorizationFiltersModule : Autofac.Module
	{
		private readonly Assembly _api;
		private readonly Assembly _application;

		public AuthorizationFiltersModule(Assembly api, Assembly application)
		{
			_api = Assure.ArgumentNotNull(api, nameof(api));
			_application = Assure.ArgumentNotNull(application, nameof(application));
		}

		protected override void Load(ContainerBuilder builder)
		{
			_application.GetTypes()
				.Where(t => t.IsAssignableTo<IBaseRequest>())
				.ToList()
				.ForEach(r => RegisterFilter(builder, r));

			builder.RegisterType<HttpCurrentUserProvider>()
				.As<ICurrentUserProvider>()
				.InstancePerLifetimeScope();
		}

		private void RegisterFilter(ContainerBuilder builder, Type request)
		{
			var response = request.GetInterfaces()
				.Single(i => i.IsGenericType)
				.GetGenericArguments()
				.Single();

			var filterBase = typeof(RequestAuthorizationFilter<,>)
				.MakeGenericType(request, response);

			var filter = _api.GetTypes().SingleOrDefault(t => filterBase.IsAssignableFrom(t));
			if (filter == null)
				return;

			var behaviour = typeof(IPipelineBehavior<,>).MakeGenericType(request, response);

			builder.RegisterType(filter).As(behaviour);
		}
	}
}