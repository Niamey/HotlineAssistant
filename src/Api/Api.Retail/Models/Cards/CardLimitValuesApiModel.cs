using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Api.Retail.Models.Cards
{
	public class CardLimitValuesApiModel
	{
		public decimal Current { get; set; }
		public decimal Remaining { get; set; }

		public CardLimitThresholdValueApiModel Threshold { get; set; }
	}
}
