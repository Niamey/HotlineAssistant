using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Api.Services.SearchRequests
{
	internal class GetTokenRequest
	{
		public string Client { get; set; }
		public string Password { get; set; }
	}
}
