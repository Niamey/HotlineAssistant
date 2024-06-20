using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Storefront.Requests.Handlers.SearchLinks;
using Vostok.Hotline.Storefront.Requests.Queries.Modules;
using Vostok.Hotline.Storefront.Services.Menu;
using Vostok.Hotline.Storefront.ViewModels.Modules;

namespace Vostok.Hotline.Storefront.Bootstrappers
{
	public static class ModulesBootstrapperExtension
	{
		public static void AddModulesRules(this IServiceCollection services)
		{
			services.AddTransient<PersonalDataService>();

			services.AddTransient<IRequestHandler<PersonalDataConfigurationQuery, SearchRowsResponseRowModel<PersonalDataConfigurationViewModel>>, PersonalDataConfigurationQueryHandler>();
		}
	}
}
