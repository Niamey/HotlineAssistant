using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.References.Managers;
using Vostok.Hotline.Api.Retail.Models.Agreements;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Agreements.Mapper
{
	public class AgreementBalanceMapper
	{
		private readonly CurrencyApiManager _currencyManager;

		public AgreementBalanceMapper(CurrencyApiManager currencyManager)
		{
			_currencyManager = currencyManager;
		}


		public async Task<AgreementBalanceViewModel> MapToAgreementBalanceViewModelAsync(AgreementBalanceApiModel response, CancellationToken cancellationToken)
		{
			var result = new AgreementBalanceViewModel
			{
				Available = response.Available,
				Date = response.Date,
				HasCreditLimit = response.CreditLimit > 0,
				CreditLimit = response.CreditLimit,
				UsedCreditLimit = response.Loan
			};

			result.Currency = await _currencyManager.GetCurrencyShortNameAsync(response.CurrencyCode, cancellationToken);
			return result;
		}
	}
}
