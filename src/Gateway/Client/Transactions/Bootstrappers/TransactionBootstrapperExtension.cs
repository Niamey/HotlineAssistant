using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Transactions.Mapper;
using Vostok.Hotline.Gateway.Client.Transactions.Requests.Handlers;
using Vostok.Hotline.Gateway.Client.Transactions.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Transactions.Services;
using Vostok.Hotline.Gateway.Client.Transactions.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Transactions.Bootstrappers
{
	public static class TransactionBootstrapperExtension
	{
		public static void AddTransactionsRules(this IServiceCollection services)
		{
			services.AddTransient<TransactionService>();
			services.AddSingleton<TransactionMapper>();
			services.AddSingleton<TransactionFeeMapper>();
			services.AddSingleton<TransactionFinancialMapper>();
			services.AddSingleton<TransactionChildMapper>();

			services.AddTransient<IRequestHandler<TransactionListQuery, SearchPagedResponseRowModel<TransactionViewModel>>, TransactionListQueryHandler>();
			services.AddTransient<IRequestHandler<TransactionListFInancialBlockingQuery, SearchPagedResponseRowModel<TransactionFinancialViewModel>>, TransactionListFinancialBlockingQueryHandler>();
			services.AddTransient<IRequestHandler<TransactionListFinancialUnblockingQuery, SearchPagedResponseRowModel<TransactionFinancialViewModel>>, TransactionListFinancialUnblockingQueryHandler>();

			services.AddTransient<IRequestHandler<TransactionReversalListQuery, TransactionViewModel>, TransactionReversalListQueryHandler>();
		}
	}
}
