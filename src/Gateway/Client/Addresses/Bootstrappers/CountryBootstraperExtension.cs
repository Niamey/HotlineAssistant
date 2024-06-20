using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Gateway.Client.Addresses.Managers;
using Vostok.Hotline.Gateway.Client.Addresses.Requests.Handlers;
using Vostok.Hotline.Gateway.Client.Addresses.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Addresses.Services;
using Vostok.Hotline.Gateway.Client.Addresses.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Addresses.Bootstrappers
{
	public static class CountryBootstraperExtension
	{
		public static void AddCountryRules(this IServiceCollection services)
		{
			services.AddSingleton<CountryService>();
			services.AddSingleton<CountryManager>();
			services.AddTransient<IRequestHandler<CountryDetailQuery, CountryViewModel>, CountryDetailQueryHandler>();
		}
	}
}
