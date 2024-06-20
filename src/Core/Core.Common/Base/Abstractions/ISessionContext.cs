using System;
using System.Threading;
using Vostok.Hotline.Core.Common.Security;

namespace Vostok.Hotline.Core.Common.Base.Abstractions
{
	public interface ISessionContext
	{
		public const string SessionKey = "hl-session-context-id";

		Guid SessionId { get; }
		CancellationToken CancellationToken { get; }
	}
}
