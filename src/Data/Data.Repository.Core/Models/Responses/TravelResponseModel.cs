using System;
using System.Collections.Generic;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Data.Repository.Core.Models.Responses.Enums;

namespace Vostok.Hotline.Data.Repository.Core.Models.Responses
{
	public class TravelResponseModel : ResponseBaseModel
	{
		public int TravelId { get; set; }

		public long SolarId { get; set; }

		public TravelStatusResponseEnum TravelStatus { get; set; }

		public List<TravelCountryResponseModel> TravelCountries { get; set; }

		public DateTime StartTravel { get; set; }

		public DateTime EndTravel { get; set; }

		public long ContactPhone { get; set; }

		public List<TravelCardResponseModel> TravelCards { get; set; }

		public decimal CashWithdrawalLimit { get; set; }

		public decimal LimitCardTransfers { get; set; }
	}
}
