using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Api.Services.Models.MDES;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Mapper
{
	public static class CardTokenMapperExtensions
	{
		public static CardTokenViewModel ToCardTokenViewModel(this TokenApiModel response)
		{
			return new CardTokenViewModel
			{
				TokenId = response.TokenUniqueReference,
				TokenStatus = response.TokenStatusName,
				TokenTime = response.THdateTime,
				DeviceName = response.DeviceName,
				Wallet = response.Wallet,
				RequestorName = response.RequestorName
			};
		}

		public static SearchRowsResponseRowModel<CardTokenViewModel> ToCardTokenListViewModel(this TokenCollectionApiModel response)
		{
			var result = new SearchRowsResponseRowModel<CardTokenViewModel>();
			foreach (var row in response)
			{
				result.Rows.Add(row.ToCardTokenViewModel());
			}

			return result;
		}
	}
}
