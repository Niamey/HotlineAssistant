using System;

namespace Vostok.Hotline.Core.Common.Exeptions.Abstractions
{
	public interface IExceptionUid
	{
		Guid ExceptionUid { get; }
	}
}
