using Vostok.Hotline.Api.Retail.Models;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Agreements.Mapper
{
	public static class AgreementTariffMapperExtension
	{
		public static AgreementTariffViewModel ToAgreementTariffViewModel(this TariffApiModel response)
		{
			var result = new AgreementTariffViewModel
			{
				TariffId = response.TariffId,
				TariffName = response.TariffName,
				TariffStart = response.TariffStart,
				TariffEnd = response.TariffEnd,
				TariffUrl = response.TariffUrl
			};
			return result;
		}

		public static SearchRowsResponseRowModel<AgreementTariffViewModel> ToAgreementTariffViewModel(this SearchRowsResponseRowModel<TariffApiModel> response)
		{
			var result = new SearchRowsResponseRowModel<AgreementTariffViewModel>();
			foreach (var row in response.Rows)
			{
				result.Rows.Add(ToAgreementTariffViewModel(row));
			}
			return result;
		}
	}
}
