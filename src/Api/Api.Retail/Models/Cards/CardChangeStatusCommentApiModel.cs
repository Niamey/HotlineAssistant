using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Api.Retail.Models.Cards
{
	public class CardChangeStatusCommentApiModel
	{
		public string Comment { get; set; }
		public string SystemName { get; set; }
		public string UserLogin { get; set; }
		public bool IsEmployee { get; set; }
		public DateTime ModifyDate { get; set; }
	}
}
