using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Gateway.Client.Addresses.Managers;
using Vostok.Hotline.Gateway.Client.Addresses.Mapper;
using Vostok.Hotline.Gateway.Client.Addresses.Requests.Handlers;
using Vostok.Hotline.Gateway.Client.Addresses.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Addresses.Services;
using Vostok.Hotline.Gateway.Client.Addresses.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Addresses.Bootstrappers
{
	public static class AddressesBootstrapperExtension
    {
		public static void AddAddressesRules(this IServiceCollection services)
		{
			services.AddSingleton<DistrictManager>();
			services.AddSingleton<RegionManager>();
			services.AddSingleton<AddressManager>();
			services.AddSingleton<AddressMapper>();
			services.AddSingleton<AddressService>();			
			
			services.AddTransient<IRequestHandler<AddressDetailQuery, AddressViewModel>, AddressDetailQueryHandler>();
		
		}
    }
}