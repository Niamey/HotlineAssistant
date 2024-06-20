using System.Linq;
using Vostok.Hotline.Api.References.Models;
using Vostok.Hotline.Api.Retail.Models;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Accounts;
using Vostok.Hotline.Gateway.Client.Accounts.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Accounts.Mapper
{
	public static class AccountMapperExtensions
	{
		public static AccountViewModel ToAccountViewModel(this AccountApiModel response, CurrencyApiModel currency = null)
		{
			var result = new AccountViewModel
			{
				AccountId = response.AccountId, 				 
				AgreementId = response.AgreementId,
				SolarId = response.SolarId,
				Currency = currency == null ? response.CurrencyCode : currency.CurrencyShortName,
				Number = response.Number,
				Status = response.Status,
				Type = response.Type
			};

			return result;
		}

		public static SearchRowsResponseRowModel<AccountViewModel> ToAccountListViewModel(this AccountCollectionApiModel response, CurrencyCollectionApiModel currencies = null)
		{
			var result = new SearchRowsResponseRowModel<AccountViewModel>();
			var orderedResponse = response.OrderByDescending(s => s.Status == AccountStatusEnum.Active);

			foreach (var row in orderedResponse)
			{
				CurrencyApiModel value;
				if (currencies.TryGetValue(row.CurrencyCode, out value))
					result.Rows.Add(row.ToAccountViewModel(value));
				else
					result.Rows.Add(row.ToAccountViewModel());
			}

			return result;
		}
	}
}