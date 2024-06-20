using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Bvr.Abstractions;
using Vostok.Hotline.Api.Bvr.ApiServices;
using Vostok.Hotline.Api.Bvr.Managers;

namespace Vostok.Hotline.Api.Bvr.Bootstrappers
{
	public static class DeviceBootstrapperExtension
	{
		public static void AddApiDeviceRules(this IServiceCollection services)
		{
			services.AddScoped<IDeviceApiService, DeviceApiService>(serviceProvider =>
			{
				return new DeviceApiService(serviceProvider);
			});
			services.AddScoped<DeviceApiManger>();
		}
	}
}
