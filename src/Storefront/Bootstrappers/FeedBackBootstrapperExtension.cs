using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Storefront.Requests.Commands;
using Vostok.Hotline.Storefront.Requests.Handlers.FeedBack;
using Vostok.Hotline.Storefront.Services.FeedBack;

namespace Vostok.Hotline.Storefront.Bootstrappers
{
	public static class FeedBackBootstrapperExtension
	{
		public static void AddFeedBackRules(this IServiceCollection services)
		{
			services.AddTransient<FeedBackService>();

			services.AddTransient<IRequestHandler<SendFeedBackCommand, StatusCommandViewModel>, SendFeedBackCommandHandler>();
			services.AddTransient<IRequestHandler<ReportExceptionsCommand, StatusCommandViewModel>, ReportExceptionsCommandHandler>();
		}
	}
}
