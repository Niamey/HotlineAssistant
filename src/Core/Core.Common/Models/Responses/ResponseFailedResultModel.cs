using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Core.Common.Models.Responses
{
	public class ResponseFailedResultModel<TResponseFailed>
		where TResponseFailed : IResponseFailed
	{
		public Guid? SessionId { get; set; }
		public TResponseFailed Exception { get; }

		public ResponseFailedResultModel(TResponseFailed responseFailed, Guid sessionId)
		{
			SessionId = sessionId;
			Exception = responseFailed;
		}
	}
}
