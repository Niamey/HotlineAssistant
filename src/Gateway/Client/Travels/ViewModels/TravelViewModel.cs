using System;
using System.Collections.Generic;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Travels;

namespace Vostok.Hotline.Gateway.Client.Travels.ViewModels
{
	public class TravelViewModel : ResponseBaseModel
	{
		public int RowNum { get; set; }

		public int TravelId { get; set; }

		public long SolarId { get; set; }

		public TravelStatusEnum Status { get; set; }

		public List<TravelCountryViewModel> Countries { get; set; }

		public DateTime StartTravel { get; set; }

		public DateTime EndTravel { get; set; }

		public long ContactPhone { get; set; }

		public List<TravelCardViewModel> Cards { get; set; }

		public decimal CashWithdrawalLimit { get; set; }

		public decimal LimitCardTransfers { get; set; }
	}
}
