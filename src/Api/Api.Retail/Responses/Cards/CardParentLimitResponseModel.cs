using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Api.Retail.Responses.Cards
{
	internal class CardParentLimitResponseModel
	{
		public int ParentId { get; set; }
		public string ParentType { get; set; }
		public string State { get; set; }
	}
}
