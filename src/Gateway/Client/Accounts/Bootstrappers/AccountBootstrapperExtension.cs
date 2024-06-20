using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Accounts.Requests.Handlers;
using Vostok.Hotline.Gateway.Client.Accounts.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Accounts.Services;
using Vostok.Hotline.Gateway.Client.Accounts.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Accounts.Bootstrappers
{
	public static class AccountBootstrapperExtension
	{
		public static void AddAccountsRules(this IServiceCollection services)
		{
			services.AddTransient<AccountService>();

			services.AddTransient<IRequestHandler<AccountListQuery, SearchRowsResponseRowModel<AccountViewModel>>, AccountListQueryHandler>();
		}
	}
}
