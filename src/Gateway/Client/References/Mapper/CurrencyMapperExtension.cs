using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.References.Services.Models.Responses;
using Vostok.Hotline.Gateway.Client.References.ViewModels;

namespace Vostok.Hotline.Gateway.Client.References.Mapper
{
	public static class CurrencyMapperExtension
	{

		public static SearchRowsResponseRowModel<CurrencyViewModel> ToCurrencyListViewModel(this CurrencyCollectionViewModel model)
		{
			var result = new SearchRowsResponseRowModel<CurrencyViewModel>();

			foreach (var row in model)
			{
				result.Rows.Add(row.Value);
			}

			return result;
		}
		public static CurrencyViewModel ToCurrencyViewModel(this CurrencyResponseModel response)
		{
			var result = new CurrencyViewModel
			{
				Id = response.Id,
				CountryId = response.CountryId,
				CurrencyCode = response.CurrencyCode,
				CurrencyShortName = response.CurrencyShortName,
				CurrencyName = response.CurrencyName,
				CurrencyNameUa = response.CurrencyNameUa,
				CurrencyNameRu = response.CurrencyNameRu
			};

			return result;
		}
	}
}
