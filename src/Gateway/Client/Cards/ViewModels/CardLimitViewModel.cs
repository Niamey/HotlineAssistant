using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vostok.Hotline.Gateway.Client.Cards.ViewModels
{
	public class CardLimitViewModel
	{
		public bool Enabled => Limits.Any(x => x.Limit > 0);
		public List<AmountCardLimitViewModel> Limits { get; set; }
	}
}
