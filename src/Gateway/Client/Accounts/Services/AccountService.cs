using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Gateway.Client.Accounts.ViewModels;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Accounts.Mapper;
using System.Linq;
using Vostok.Hotline.Api.References.Managers;
using Vostok.Hotline.Gateway.Client.Accounts.Requests.Queries;
using Vostok.Hotline.Api.Retail.Managers;

namespace Vostok.Hotline.Gateway.Client.Accounts.Services
{
	public class AccountService
	{
		private readonly CurrencyApiManager _currencyManager;
		private readonly AccountApiManager _accountManager;

		public AccountService(AccountApiManager accountManager, CurrencyApiManager currencyManager)
		{
			_accountManager = accountManager;
			_currencyManager = currencyManager;
		}

		public async Task<SearchRowsResponseRowModel<AccountViewModel>> GetAccountListAsync(AccountListQuery searchRequest, CancellationToken cancellationToken)
		{
			var result = await _accountManager.GetAccountCollectionAsync(searchRequest.SolarId.Value, cancellationToken);
			if (result?.Count > 0)
			{
				var currencyCodes = result.Select(x => x.CurrencyCode).Distinct().ToArray();
				var currencyList = await _currencyManager.GetCurrencyCollectionAsync(currencyCodes, cancellationToken);
				return result.ToAccountListViewModel(currencyList);
			}

			return result.ToAccountListViewModel();
		}
	}
}