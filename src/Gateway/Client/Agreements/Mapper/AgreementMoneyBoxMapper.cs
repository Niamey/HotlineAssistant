using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.References.Models;
using Vostok.Hotline.Api.Retail.Models;
using Vostok.Hotline.Api.Retail.Models.MoneyBox;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Agreements.Mapper
{
	public class AgreementMoneyBoxMapper
	{

		public async Task<AgreementMoneyBoxViewModel> ToMoneyBoxViewModelAsync(MoneyBoxApiModel response, CurrencyApiModel currency, CancellationToken cancellationToken)
		{
			var result = new AgreementMoneyBoxViewModel
			{
				Id = response.Id,
				SavingId = response.SavingId,
				Name = response.Name,
				Amount = new MoneyViewModel()
				{
					Amount = response.Amount.Amount,
					Currency = currency == null ? response.Amount.CurrencyCode : currency.CurrencyShortName,
				},
				Status = response.Status.ToAgreementMoneyBoxStatusEnum()

			};

			return result;
		}

		public async Task<AgreementMoneyBoxViewModel> ToMoneyBoxViewModelAsync(MoneyBoxApiModel response, CancellationToken cancellationToken)
		{
			return await ToMoneyBoxViewModelAsync(response, null, cancellationToken);
		}

		public async Task<SearchRowsResponseRowModel<AgreementMoneyBoxViewModel>> ToMoneyBoxListViewModelAsync(MoneyBoxCollectionApiModel collections, CurrencyCollectionApiModel currencies, CancellationToken cancellationToken)
		{
			var result = new SearchRowsResponseRowModel<AgreementMoneyBoxViewModel>();
			foreach (var row in collections)
			{
				CurrencyApiModel value;
				if (currencies.TryGetValue(row.Amount.CurrencyCode, out value))
					result.Rows.Add(await ToMoneyBoxViewModelAsync(row, value, cancellationToken));
				else
					result.Rows.Add(await ToMoneyBoxViewModelAsync(row, cancellationToken));
			}

			return result;
		}

		public async Task<SearchRowsResponseRowModel<AgreementMoneyBoxViewModel>> ToMoneyBoxListViewModelAsync(MoneyBoxCollectionApiModel collections, CancellationToken cancellationToken)
		{
			return await ToMoneyBoxListViewModelAsync(collections, null, cancellationToken);
		}
	}
}
