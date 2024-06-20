using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Countries.Requests.Handlers;
using Vostok.Hotline.Gateway.Client.Countries.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Countries.Services;
using Vostok.Hotline.Gateway.Client.Countries.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Countries.Bootstrappers
{
	public static class CountryBootstrapperExtension
	{
		public static void AddCountriesRules(this IServiceCollection services)
		{
			services.AddTransient<CountryService>();

			services.AddTransient<IRequestHandler<CountryListQuery, SearchRowsResponseRowModel<CountryViewModel>>, CountryListQueryHandler>();
		}
	}
}
