using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using Vostok.HotLineAssistant.Application.AutofacModules;

namespace Vostok.HotLineAssistant.Application.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static ContainerBuilder GetAutofacContainerBuilder(this IServiceCollection services,
			params Assembly[] assemblyTypes)
		{
			var container = new ContainerBuilder();

			container.Populate(services);

			container.RegisterModule(new MediatorModule(assemblyTypes));
			container.RegisterModule(new ValidationModule(assemblyTypes));

			return container;
		}

		public static IServiceCollection AddMediatorHandlers(this IServiceCollection services, Assembly assembly)
		{
			var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

			foreach (var type in classTypes)
			{
				var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

				foreach (var handlerType in interfaces.Where(i =>
					i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
				{
					services.AddTransient(handlerType.AsType(), type.AsType());
				}

				foreach (var handlerType in interfaces.Where(i =>
					i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
				{
					services.AddTransient(handlerType.AsType(), type.AsType());
				}
			}

			return services;
		}

		public static void RegisterEntityCommandHandler(this ContainerBuilder builder, Type entity, Type genericCommand,
			Type genericHandler, Type genericValidator)
		{
			var commandType = genericCommand.MakeGenericType(entity);
			var handlerType = genericHandler.MakeGenericType(commandType, entity);
			var handlerAbstractType = typeof(IRequestHandler<,>).MakeGenericType(commandType, typeof(Unit));

			builder.RegisterType(handlerType)
				.IfNotRegistered(handlerAbstractType)
				.As(handlerAbstractType);

			var validatorType = genericValidator.MakeGenericType(entity);
			var validatorAbstractType = typeof(IValidator<>).MakeGenericType(commandType);

			builder.RegisterType(validatorType)
				.IfNotRegistered(validatorAbstractType)
				.As(validatorAbstractType);
		}
	}
}