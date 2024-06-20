using System;

namespace Vostok.Hotline.Core.Data.Abstractions
{
	public interface ITransaction : IDisposable
	{
		void Complete();
	}
}
