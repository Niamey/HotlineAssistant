using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Storefront.Requests.Handlers.SearchLinks;
using Vostok.Hotline.Storefront.Requests.Queries.SearchLinks;
using Vostok.Hotline.Storefront.Services.SearchLinks;
using Vostok.Hotline.Storefront.ViewModels.SearchLinks;

namespace Vostok.Hotline.Storefront.Bootstrappers
{
	public static class SearchLinkBootstrapperExtension
	{
		public static void AddSearchLinksRules(this IServiceCollection services)
		{
			services.AddTransient<SearchLinkService>();

			services.AddTransient<IRequestHandler<SearchLinkListQuery, SearchRowsResponseRowModel<SearchLinkViewModel>>, SearchLinkListQueryHandler>();
		}
	}
}
