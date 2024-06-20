using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Api.Services.Responses.Messenger
{
	internal class GetTokenResponseModel
	{
		public string AccessToken { get; set; }
		public long ClientId { get; set; }
		public DateTime Expires { get; set; }
	}
}
