using Vostok.Hotline.Api.Retail.Models;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Mapper
{
	public static class CardTariffMapperExtension
	{
		public static CardTariffViewModel ToCardTariffViewModel(this TariffApiModel response)
		{
			var result = new CardTariffViewModel
			{
				TariffId = response.TariffId,
				TariffName = response.TariffName,
				TariffStart = response.TariffStart,
				TariffEnd = response.TariffEnd,
				TariffUrl = response.TariffUrl
			};
			return result;
		}

		public static SearchRowsResponseRowModel<CardTariffViewModel> ToCardTariffViewModel(this SearchRowsResponseRowModel<TariffApiModel> response)
		{
			var result = new SearchRowsResponseRowModel<CardTariffViewModel>();
			foreach (var row in response.Rows)
			{
				result.Rows.Add(ToCardTariffViewModel(row));
			}
			return result;
		}
	}
}
