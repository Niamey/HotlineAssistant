using Autofac;
using MediatR;
using MediatR.Pipeline;
using System.Reflection;
using Vostok.HotLineAssistant.Application.Behaviors;

namespace Vostok.HotLineAssistant.Application.AutofacModules
{
	public class MediatorModule : Autofac.Module
	{
		private readonly Assembly[] _assemblyTypes;

		public MediatorModule(params Assembly[] assemblyTypes)
		{
			_assemblyTypes = assemblyTypes;
		}

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

			var mediatrOpenTypes = new[]
			{
				typeof(IRequestHandler<,>),
				typeof(INotificationHandler<>)
			};

			foreach (var mediatrOpenType in mediatrOpenTypes)
			{
				builder
					.RegisterAssemblyTypes(_assemblyTypes)
					.AsClosedTypesOf(mediatrOpenType)
					.AsImplementedInterfaces()
					.PreserveExistingDefaults();
			}

			builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
			builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
			builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
			builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

			builder.Register<ServiceFactory>(ctx =>
			{
				var c = ctx.Resolve<IComponentContext>();
				return t => c.Resolve(t);
			});
		}
    }
}