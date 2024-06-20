using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.References.Abstractions;
using Vostok.Hotline.Api.References.ApiServices;
using Vostok.Hotline.Api.References.Managers;
using Vostok.Hotline.Api.References.Mapper;

namespace Vostok.Hotline.Api.References.Bootstrappers
{
	public static class AddressBootstrapperExtension
	{
		public static void AddApiAddressRules(this IServiceCollection services)
		{
			services.AddSingleton<AddressMapper>(serviceProvider =>
			{
				return new AddressMapper(serviceProvider);
			});
			services.AddSingleton<IAddressApiService, AddressApiService>(serviceProvider => 
			{
				return new AddressApiService(serviceProvider); 
			});
			services.AddSingleton<AddressApiManager>();
		}
	}
}
