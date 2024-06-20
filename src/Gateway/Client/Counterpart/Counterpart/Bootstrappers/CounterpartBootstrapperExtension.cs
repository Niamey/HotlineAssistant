using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Counterpart.Mapper;
using Vostok.Hotline.Gateway.Client.Counterpart.Requests.Handlers;
using Vostok.Hotline.Gateway.Client.Counterpart.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Counterpart.Requests.Validations;
using Vostok.Hotline.Gateway.Client.Counterpart.Services;
using Vostok.Hotline.Gateway.Client.Counterpart.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Counterpart.Bootstrappers
{
	public static class CounterpartBootstrapperExtension
	{
		public static void AddCounterpartRules(this IServiceCollection services)
		{
			services.AddSingleton<ClientMapper>();
			services.AddSingleton<ClientService>();

			services.AddTransient<IValidator<ClientSearchQuery>, ClientSearchQueryValidator>();
			services.AddTransient<IValidator<ClientCountQuery>, ClientCountQueryValidator>();

			services.AddTransient<IRequestHandler<ClientSearchQuery, SearchPagedResponseRowModel<ClientViewModel>>, ClientSearchQueryHandler>();
			services.AddTransient<IRequestHandler<ClientCountQuery, ClientCountViewModel>, ClientCountQueryHandler>();
			services.AddTransient<IRequestHandler<ClientDetailQuery, ClientViewModel>, ClientDetailQueryHandler>();
		}
	}
}
