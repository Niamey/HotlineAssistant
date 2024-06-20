using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Gateway.Vicidial.Mapper;
using Vostok.Hotline.Gateway.Vicidial.Requests.Handlers;
using Vostok.Hotline.Gateway.Vicidial.Requests.Queries;
using Vostok.Hotline.Gateway.Vicidial.Services;
using Vostok.Hotline.Gateway.Vicidial.ViewModels;

namespace Vostok.Hotline.Gateway.Vicidial.Bootstrappers
{
	public static class ChatterBootstrapperExtension
	{
		public static void AddChatterRules(this IServiceCollection services)
		{
			services.AddTransient<ChatterService>();
			services.AddSingleton<ChatterNewCallMapper>();
			services.AddTransient<IRequestHandler<ChatterNewCallQuery, ChatterNewCallResponseViewModel>, ChatterNewCallQueryHandler>();
		}
	}
}

