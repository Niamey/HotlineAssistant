using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;

namespace Vostok.Hotline.Api.Retail.Models.Cards
{
	public class CardBlockingResultApiModel
	{
		public StatusCommandApiViewModel Status { get; set; }
		public CardBlockingOperationStatusCollectionApiModel OperationStatuses { get; set; }
		public string ResultForOperator { get; set; }
	}
}
