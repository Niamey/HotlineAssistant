using MediatR;
using System.Runtime.Serialization;
using Vostok.HotLineAssistant.Common.Requests.Base;
using Vostok.HotLineAssistant.Common.Response.Common;

namespace Vostok.HotLineAssistant.Common.Requests.Common
{
	public class ByPageRequest<T> : ByPageRequest, IRequest<PageResponse<T>>
	{
	}

	public class ByPageRequest : BaseRequest
	{
		[DataMember]
		public int? PageNumber { get; set; }

		[DataMember]
		public int? PageSize { get; set; }
	}
}