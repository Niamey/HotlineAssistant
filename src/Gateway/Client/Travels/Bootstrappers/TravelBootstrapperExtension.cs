using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Gateway.Client.Travels.Services;
using Vostok.Hotline.Gateway.Client.Travels.Requests.Queries;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Travels.ViewModels;
using Vostok.Hotline.Gateway.Client.Travels.Requests.Handlers;
using Vostok.Hotline.Gateway.Client.Travels.Requests.Commands;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Gateway.Client.Travels.Mapper;

namespace Vostok.Hotline.Gateway.Client.Travels.Bootstrappers
{
	public static class TravelBootstrapperExtension
	{
		public static void AddTravelsRules(this IServiceCollection services)
		{
			services.AddTransient<TravelService>();
			
			services.AddTransient<IRequestHandler<TravelPagedQuery, SearchPagedResponseRowModel<TravelViewModel>>, TravelPagedQueryHandler>();
			services.AddTransient<IRequestHandler<TravelDetailQuery, TravelViewModel>, TravelDetailQueryHandler>();
			services.AddTransient<IRequestHandler<TravelDeleteCommand, StatusCommandViewModel>, TravelDeleteCommandHandler>();
			services.AddTransient<IRequestHandler<TravelNewCommand, StatusCommandViewModel>, TravelNewCommandHandler>();
			services.AddTransient<IRequestHandler<TravelCountQuery, TravelCountViewModel>, TravelCountQueryHandler>();


			services.AddScoped<TravelCountryMapper>();
			services.AddScoped<TravelMapper>();
		}
	}
}
