using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Vostok.Hotline.Core.Common.Base.Abstractions
{
	public interface ISessionContext
	{
		public const string SessionKey = "hl-session-context-id";

		Guid SessionId { get; }
		CancellationToken CancellationToken { get; }
	}
}
