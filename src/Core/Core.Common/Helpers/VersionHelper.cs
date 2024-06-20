using System;

namespace Vostok.Hotline.Core.Common.Helpers
{
	public static class VersionHelper
	{
		public static Version SystemVersion { get; } = new Version(0, 9, 1);

		//static VersionHelper()
		//{
		//	var versionAssembly = typeof(VersionHelper).Assembly;
		//	SystemVersion = versionAssembly.GetName().Version;
		//}
	}
}