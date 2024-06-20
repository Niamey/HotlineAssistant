using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Api.Retail.Responses.Cards
{
	internal class CardLimitThresholdValueResponseModel
	{
		public CardLimitValueAmountResponseModel Amount { get; set; }
		public CardLimitValuePeriodResponseModel Period { get; set; }
        public decimal Count { get; set; }
	}
}
