using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Api.Retail.Models.Cards
{
	public class CardLimitThresholdValueApiModel
	{
		public CardLimitValueAmountApiModel Amount { get; set; }
		public CardLimitValuePeriodApiModel Period { get; set; }
		public decimal Count { get; set; }
	}
}
