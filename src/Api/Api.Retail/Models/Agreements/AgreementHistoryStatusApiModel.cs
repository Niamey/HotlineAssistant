using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Agreements;

namespace Vostok.Hotline.Api.Retail.Models.Agreements
{
	public class AgreementHistoryStatusApiModel : ResponseBaseModel
	{
		public AgreementStatusEnum CurrentStatus { get; set; }
		public DateTime ModifyDate { get; set; }
		public string Comment { get; set; }
		public string SystemName { get; set; }
		public string UserLogin { get; set; }
		public bool? IsEmployee { get; set; }
	}
}
