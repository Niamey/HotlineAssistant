using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.References.Managers;
using Vostok.Hotline.Api.Retail.Models.Agreements;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Agreements;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Agreements.Mapper
{
	public class AgreementMapper
	{
		private readonly CurrencyApiManager _currencyManager;

		public AgreementMapper(CurrencyApiManager currencyManager)
		{
			_currencyManager = currencyManager;
		}

		public async Task<AgreementViewModel> ToAgreementViewModelAsync(AgreementApiModel response, CancellationToken cancellationToken)
		{
			var result = new AgreementViewModel
			{
				AgreementId = response.AgreementId,
				SolarId = response.SolarId,
				ClientName = response.ClientName,
				AgreementNumber = response.Number,
				ProductName = response.ProductName,
				Status = response.Status,
				Type = response.Type,
				Bonus = response.Bonus,
				ViewType = AgreementViewTypeEnum.Bvr
			};

			result.Currency = await _currencyManager.GetCurrencyShortNameAsync(response.CurrencyCode, cancellationToken);

			return result;
		}

		public async Task<SearchRowsResponseRowModel<AgreementViewModel>> ToAgreementListViewModelAsync(AgreementCollectionApiModel collections, CancellationToken cancellationToken)
		{
			var result = new SearchRowsResponseRowModel<AgreementViewModel>();
			var orderedCollection = collections.OrderByDescending(s => s.Status == AgreementStatusEnum.Active);
			foreach (var row in orderedCollection)
			{
				result.Rows.Add(await ToAgreementViewModelAsync(row, cancellationToken));
			}

			return result;
		}
	} 
}