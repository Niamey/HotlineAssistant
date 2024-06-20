using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Addresses.Requests.Handlers;
using Vostok.Hotline.Gateway.Client.References.Managers;
using Vostok.Hotline.Gateway.Client.References.Requests.Queries;
using Vostok.Hotline.Gateway.Client.References.Services;
using Vostok.Hotline.Gateway.Client.References.ViewModels;

namespace Vostok.Hotline.Gateway.Client.References.Bootstrappers
{
	public static class CurrencyBootstrapperExtension
	{
		public static void AddCurrencyRules(this IServiceCollection services)
		{
			services.AddSingleton<CurrencyManager>();
			services.AddSingleton<CurrencyService>();

			services.AddTransient<IRequestHandler<CurrencyDetailQuery, CurrencyViewModel>, CurrencyDetailQueryHandler>();
			services.AddTransient<IRequestHandler<CurrencyListQuery, SearchRowsResponseRowModel<CurrencyViewModel>>, CurrencyListQueryHandler>();

		}
	}
}
