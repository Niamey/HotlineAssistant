using System;
using Vostok.Hotline.Data.EF.Entities.Core;
using Vostok.Hotline.Data.EF.Enums;
using Vostok.Hotline.Data.Repository.Core.Models.Responses;
using Vostok.Hotline.Data.Repository.Core.Models.Responses.Enums;

namespace Vostok.Hotline.Data.Repository.Core.Mapper
{
	internal static class TravelMapperExtensions
	{
		public static TravelResponseModel ToTravelResponseModel(this Travel response)
		{
			var result = new TravelResponseModel()
			{
				TravelId = response.TravelId,
				SolarId = response.SolarId,
				TravelStatus = response.TravelStatus.ToTravelStatusResponseEnum(response),
				TravelCountries = response.TravelCountries?.ToListTravelCountryResponseModel(),
				StartTravel = response.StartTravel,
				EndTravel = response.EndTravel,
				ContactPhone = response.ContactPhone,
				TravelCards = response.TravelCards?.ToListTravelCardResponseModel(),
				CashWithdrawalLimit = response.CashWithdrawalLimit,
				LimitCardTransfers = response.LimitCardTransfers
			};

			return result;
		}

		public static TravelStatusResponseEnum ToTravelStatusResponseEnum(this TravelStatusEnum response, Travel travel)
		{
			if (response == TravelStatusEnum.Deleted)
				return TravelStatusResponseEnum.Deleted;
			
			if (response == TravelStatusEnum.Error)
				return TravelStatusResponseEnum.Error;

			if (response == TravelStatusEnum.Active)
			{
				if (DateTime.Now < travel.StartTravel)
					return TravelStatusResponseEnum.Planned;
				
				if (DateTime.Now.Date > travel.EndTravel)
					return TravelStatusResponseEnum.Finished;
				
				if (travel.StartTravel < DateTime.Now && DateTime.Now.Date <= travel.EndTravel)
					return TravelStatusResponseEnum.Active;
			}

			return TravelStatusResponseEnum.Undefined;
		}
	}
}
