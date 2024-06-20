using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Classifiers.Requests.Handlers;
using Vostok.Hotline.Gateway.Client.Classifiers.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Classifiers.Services;
using Vostok.Hotline.Gateway.Client.Classifiers.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Classifiers.Bootstrappers
{
	public static class ClassifierBootstrapperExtension
	{
		public static void AddClassifiersRules(this IServiceCollection services)
		{
			services.AddTransient<ClassifierService>();
			services.AddTransient<IRequestHandler<AgreementClassifierListQuery, SearchRowsResponseRowModel<ClassifierViewModel>>, AgreementClassifierListQueryHandler>();
		}
	}
}
