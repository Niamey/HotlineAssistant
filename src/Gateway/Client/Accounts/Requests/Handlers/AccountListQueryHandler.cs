using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Accounts.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Accounts.Services;
using Vostok.Hotline.Gateway.Client.Accounts.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Accounts.Requests.Handlers
{
	public class AccountListQueryHandler : IRequestHandler<AccountListQuery, SearchRowsResponseRowModel<AccountViewModel>>
	{
		private readonly AccountService _accountService;

		public AccountListQueryHandler(AccountService accountService)
		{
			_accountService = accountService;
		}

		public async Task<SearchRowsResponseRowModel<AccountViewModel>> Handle(AccountListQuery searchRequest, CancellationToken cancellationToken)
		{
			return await _accountService.GetAccountListAsync(searchRequest, cancellationToken);
		}
	}
}
