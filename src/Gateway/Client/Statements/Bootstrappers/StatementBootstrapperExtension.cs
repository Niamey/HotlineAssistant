using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Gateway.Client.Statements.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Statements.Requests.Handlers;
using Vostok.Hotline.Gateway.Client.Statements.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Statements.Services;
using Vostok.Hotline.Gateway.Client.Statements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Statements.Bootstrappers
{
	public static class StatementBootstrapperExtension
	{
		public static void AddStatementsRules(this IServiceCollection services)
		{
			services.AddTransient<StatementService>();
	
			services.AddTransient<IRequestHandler<StatementQuery, StatementViewModel>, StatementQueryHandler>();
			services.AddTransient<IRequestHandler<StatementSendEmailCommand, StatusCommandViewModel>, StatementSendEmailCommandHandler>();
		}
	}
}
