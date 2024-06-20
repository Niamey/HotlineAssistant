using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Mapper
{
	public static class CardCurrentStatusMapperExtensions
	{
		public static CardCurrentStatusViewModel ToCardCurrentStatusViewModel(this CardHistoryStatusApiModel response)
		{
			var result = new CardCurrentStatusViewModel
			{
				Status = response.Status,
				DateChangeStatus = response.ModifyDate,
				Reason = response.Comment,
				SystemName = response.SystemName,
				UserLogin = response.UserLogin,
				IsEmployee = response.IsEmployee
			};

			return result;
		}
	}
}