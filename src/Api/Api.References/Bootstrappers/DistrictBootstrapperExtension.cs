using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.References.Managers;

namespace Vostok.Hotline.Api.References.Bootstrappers
{
	public static class DistrictBootstrapperExtension
	{
		public static void AddApiDistrictRules(this IServiceCollection services)
		{
			services.AddSingleton<DistrictApiManager>();
		}
	}
}
