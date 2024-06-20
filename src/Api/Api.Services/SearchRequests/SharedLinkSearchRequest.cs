using System.Web;

namespace Vostok.Hotline.Api.Services.SearchRequests
{
	internal class SharedLinkSearchRequest
	{
		public SharedLinkSearchRequest(string url, string fileName,  int? timeToSecondLimit, bool displayMode)
		{
			Url = url;
			Options = new SharedLinkOptionsSearchRequest()
			{
				FileName = fileName,
				TimeToLimit = timeToSecondLimit ?? 0,
				DisplayMode = displayMode

			};
		}

		public string Url { get; set; }
		public SharedLinkOptionsSearchRequest Options { get; set; }
	}
}
