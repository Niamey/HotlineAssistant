using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Api.Services.SearchRequests
{
	internal class SharedLinkOptionsSearchRequest
	{
		public string FileName { get; set; }
		public int TimeToLimit { get; set; }
		public bool DisplayMode { get; set; }
	}
}
