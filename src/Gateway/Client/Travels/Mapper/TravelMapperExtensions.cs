using System.Collections.Generic;
using Vostok.Hotline.Data.EF.Entities.Core;
using Vostok.Hotline.Gateway.Client.Travels.Requests.Commands;

namespace Vostok.Hotline.Gateway.Client.Travels.Mapper
{
	public static class TravelMapperExtensions
	{
		public static List<TravelCard> ToListTravelCard(this List<TravelCardCommand> response)
		{
			var result = new List<TravelCard>();

			foreach (var item in response)
			{
				result.Add(new TravelCard()
				{
					CardId = item.CardId,
					CardMaskNumber = item.Number
				});
			}

			return result;
		}

		public static List<TravelCountry> ToListTravelCountry(this List<TravelCountryCommand> response)
		{
			var result = new List<TravelCountry>();

			foreach (var item in response)
			{
				result.Add(new TravelCountry()
				{
					CountryCode = item.CountryCode,
					RiskCountry = item.IsRisky
				});
			}

			return result;
		}
	}
}
