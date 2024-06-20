using System;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Vostok.Hotline.Core.Common.Base.Abstractions;
using Vostok.Hotline.Core.Common.Conversions;

namespace Vostok.Hotline.Core.Common.SessionContext
{
	public class RequestSessionContextAdapter : ISessionContext
	{
		public RequestSessionContextAdapter(IHttpContextAccessor httpContextAccessor)
		{
			SessionId = Converter.ToGuidNullable(httpContextAccessor.HttpContext?.Request?.Headers[ISessionContext.SessionKey]) ?? Guid.NewGuid();

			if (httpContextAccessor.HttpContext?.RequestAborted != null)
				CancellationToken = httpContextAccessor.HttpContext.RequestAborted;
		}

		public Guid SessionId { get; }
		public CancellationToken CancellationToken { get; }
	}
}