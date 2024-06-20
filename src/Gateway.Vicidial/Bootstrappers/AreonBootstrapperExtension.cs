using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Vicidial.Requests.Handlers;
using Vostok.Hotline.Gateway.Vicidial.Requests.Commands;
using Vostok.Hotline.Gateway.Vicidial.Services;
using Vostok.Hotline.Gateway.Vicidial.ViewModels;
using Vostok.Hotline.Gateway.Vicidial.Mapper;

namespace Vostok.Hotline.Gateway.Vicidial.Bootstrappers
{
	public static class AreonBootstrapperExtension
	{
		public static void AddAreonRules(this IServiceCollection services)
		{
			services.AddTransient<AreonService>();
			services.AddSingleton<AreonNewCallMapper>();
			services.AddTransient<IRequestHandler<AreonNewCallCommand, AreonNewCallResponseViewModel>, AreonNewCallCommandHandler>();
			services.AddTransient<IRequestHandler<AreonNewDetailCallCommand, AreonNewDetailCallResponseViewModel>, AreonNewDetailCallCommandHandler>();
		}
	}
}

