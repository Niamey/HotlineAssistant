using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vostok.Hotline.Api.Retail.Requests.Commands.Cards
{
	public class CardChangeStatusCommentCommand
	{
		public string Comment { get; set; }
		public string SystemName { get; set; }
		public string UserLogin { get; set; }
		public bool IsEmployee { get; set; }
		public DateTime ModifyDate { get; set; }
	}
}
