using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Api.Services.Models.Statement
{
	public class AuthLogoutApiModel : ResponseBaseModel
	{
		public string JSessionId { get; set; }
		public string XSRFToken { get; set; }
	}
}
