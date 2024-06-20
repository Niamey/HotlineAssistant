using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.References.Managers;
using Vostok.Hotline.Api.Retail.Managers;
using Vostok.Hotline.Api.Retail.SearchRequests.Agreements;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Agreements.Mapper;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Agreements.Services
{

	public class MoneyBoxService
	{
		private readonly MoneyBoxApiManager _moneyBoxManager;
		private readonly AgreementMoneyBoxMapper _agreementMoneyBoxMapper;
		private readonly CurrencyApiManager _currencyManager;

		public MoneyBoxService(MoneyBoxApiManager moneyBoxManager, AgreementMoneyBoxMapper agreementMoneyBoxMapper, CurrencyApiManager currencyManager)
		{
			_moneyBoxManager = moneyBoxManager;
			_agreementMoneyBoxMapper = agreementMoneyBoxMapper;
			_currencyManager = currencyManager;

		}

		public async Task<SearchRowsResponseRowModel<AgreementMoneyBoxViewModel>> GetMoneyBoxListAsync(AgreementMoneyBoxCollectionSearchRequest searchRequest, CancellationToken cancellationToken)
		{
			var result = await _moneyBoxManager.GetMoneyBoxCollectionAsync(searchRequest.AgreementId.Value, cancellationToken);
			if (result?.Count > 0)
			{
				var currencyCodes = result.Select(x => x.Amount.CurrencyCode).Distinct().ToArray();
				var currencyList = await _currencyManager.GetCurrencyCollectionAsync(currencyCodes, cancellationToken);
				return await _agreementMoneyBoxMapper.ToMoneyBoxListViewModelAsync(result, currencyList, cancellationToken);
			}

			return await _agreementMoneyBoxMapper.ToMoneyBoxListViewModelAsync(result, cancellationToken);
		}
	}
}
