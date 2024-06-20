using System;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Api.Retail.Models.Cards
{
	public class SmsInfoHistoryStatusApiModel
	{
		public SmsInfoHistoryStatusEnum CurrentStatus { get; set; }
		public DateTime DateChangeStatus { get; set; }
		public string Comment { get; set; }
	}
}
