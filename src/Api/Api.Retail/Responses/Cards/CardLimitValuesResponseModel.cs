using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Api.Retail.Responses.Cards
{
	internal class CardLimitValuesResponseModel
	{
		public decimal Current { get; set; }
		public decimal Remaining { get; set; }

		public CardLimitThresholdValueResponseModel Threshold { get; set; }
	}
}
