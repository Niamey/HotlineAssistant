using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.References.Abstractions;
using Vostok.Hotline.Api.References.ApiServices;
using Vostok.Hotline.Api.References.Managers;
using Vostok.Hotline.Api.References.Mapper;

namespace Vostok.Hotline.Gateway.Vicidial.Bootstrappers
{
	public static class DivisionBootstrapperExtension
	{
		public static void AddApiDivisionRules(this IServiceCollection services)
		{
			services.AddSingleton<DivisionMapper>();
			services.AddSingleton<IDivisionApiService, DivisionApiService>(serviceProvider =>
			{
				return new DivisionApiService(serviceProvider);
			});
			services.AddSingleton<DivisionApiManager>();
		}
	}
}
