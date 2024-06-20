using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Data.EF.Entities.Core;
using Vostok.Hotline.Data.Repository.Core.Models.Responses;

namespace Vostok.Hotline.Data.Repository.Core.Mapper
{
	internal static class TravelCardMapperExtensions
	{
		public static TravelCardResponseModel ToTravelCardResponseModel(this TravelCard response)
		{
			var result = new TravelCardResponseModel()
			{
				CardId = response.CardId,

				CardMaskNumber = response.CardMaskNumber
			};

			return result;
		}

		public static List<TravelCardResponseModel> ToListTravelCardResponseModel(this ICollection<TravelCard> response)
		{
			var result = new List<TravelCardResponseModel>();

			foreach (var item in response)
			{
				result.Add(item.ToTravelCardResponseModel());
			}

			return result;
		}
	}
}
