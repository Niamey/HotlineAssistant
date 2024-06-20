using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Storefront.Requests.Handlers.Menu;
using Vostok.Hotline.Storefront.Requests.Queries.Menu;
using Vostok.Hotline.Storefront.Services.Menu;
using Vostok.Hotline.Storefront.ViewModels.Menu;

namespace Vostok.Hotline.Storefront.Bootstrappers
{
	public static class MenuBootstrapperExtension
	{
		public static void AddMenuRules(this IServiceCollection services)
		{
			services.AddTransient<MenuLeftService>();

			services.AddTransient<IRequestHandler<MenuLeftQuery, SearchRowsResponseRowModel<MenuItemViewModel>>, MenuLeftQueryHandler>();
		}
	}
}
