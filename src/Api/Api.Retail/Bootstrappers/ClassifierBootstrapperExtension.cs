using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Retail.Abstractions;
using Vostok.Hotline.Api.Retail.ApiServices;
using Vostok.Hotline.Api.Retail.Managers;

namespace Vostok.Hotline.Api.Retail.Bootstrappers
{
	public static class ClassifierBootstrapperExtension
	{
		public static void AddApiClassifierRules(this IServiceCollection services)
		{
			services.AddSingleton<IClassifierApiService, ClassifierApiService>(serviceProvider =>
			{
				return new ClassifierApiService(serviceProvider);
			});
			services.AddSingleton<ClassifierApiManager>();
		}
	}
}
