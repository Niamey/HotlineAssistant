using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Data.Repository.Core.Models.Responses;
using Vostok.Hotline.Gateway.Client.Travels.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Travels.Mapper
{
	public static class TravelCardMapperExtensions
	{
		public static TravelCardViewModel ToTravelCardViewModel(this TravelCardResponseModel response)
		{
			return new TravelCardViewModel()
			{
				CardId = response.CardId,
				Number = response.CardMaskNumber
			};
		}

		public static List<TravelCardViewModel> ToListTravelCardViewModel(this List<TravelCardResponseModel> response)
		{
			var result = new List<TravelCardViewModel>();

			foreach (var item in response)
			{
				result.Add(item.ToTravelCardViewModel());
			}
			return result;
		}
	}
}
