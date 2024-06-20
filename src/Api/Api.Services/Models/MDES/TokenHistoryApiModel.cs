using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.MDES;

namespace Vostok.Hotline.Api.Services.Models.MDES
{
	public class TokenHistoryApiModel : ResponseBaseModel
	{
		public AuditApiModel? Audit { get; set; }
		public string CommentText { get; set; }
		public InitiatorMdesEnum Initiator { get; set; }
		public ReasonTypeMdesEnum ReasonCode { get; set; }
		public StatusCodeMdesEnum StatusCode { get; set; }
		public DateTime StatusDateTime { get; set; }
		public string StatusDescription { get; set; }
		public int? PrevCardId { get; set; }
	}
}
