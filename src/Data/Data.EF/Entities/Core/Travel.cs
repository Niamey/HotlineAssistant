using System;
using System.Collections.Generic;
using Vostok.Hotline.Core.Data.Entities;
using Vostok.Hotline.Data.EF.Enums;

namespace Vostok.Hotline.Data.EF.Entities.Core
{
	public class Travel : BusinessEntityBase
	{
		public Travel()
		{
		}

		public int TravelId { get; set; }

		public long SolarId { get; set; }

		public TravelStatusEnum TravelStatus { get; set; }

		public ICollection<TravelCountry> TravelCountries { get; set; }

		public DateTime StartTravel { get; set; }

		public DateTime EndTravel { get; set; }

		public long ContactPhone { get; set; }

		public ICollection<TravelCard> TravelCards { get; set; }

		public decimal CashWithdrawalLimit { get; set; }

		public decimal LimitCardTransfers  { get; set; }
	}		
}
