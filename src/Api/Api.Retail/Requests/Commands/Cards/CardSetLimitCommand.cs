using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Api.Retail.Requests.Commands.Cards
{
	internal class CardSetLimitCommand
	{
		internal CardSetLimitCommand(long cardId, int limitId, bool enabled)
		{
			CardId = cardId;
			LimitId = limitId;
			Enabled = enabled;

		}

		public long CardId { get; set; }
		public int LimitId { get; set; }
		public bool Enabled { get; set; }
	}
}
