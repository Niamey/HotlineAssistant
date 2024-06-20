using System;
using Vostok.Hotline.Api.Services.Responses.MDES.Enums;

namespace Vostok.Hotline.Api.Services.Responses.MDES
{
	public class TokenHistoryResponseModel
	{
		public AuditResponseModel? Audit { get; set; }
		public string CommentText { get; set; }
		public string Initiator { get; set; }
		public ReasonTypeServiceEnum ReasonCode { get; set; }
		public StatusCodeServiceEnum StatusCode { get; set; }
		public DateTime StatusDateTime { get; set; }
		public string StatusDescription { get; set; }
		public int? PrevCardId { get; set; }
	}
}
