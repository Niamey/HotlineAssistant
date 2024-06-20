using System;
using System.Web;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Authorization.FzBank.Requests
{
	public class FzBankVerifyRequest : SearchRequestBaseModel
	{
		public FzBankVerifyRequest(Guid sessionId)
		{
			SessionId = sessionId;
		}

		public Guid SessionId { get; set; }

		public override string GetUrlQuery()
		{
			var parameters = HttpUtility.ParseQueryString(string.Empty);
			parameters[nameof(SessionId)] = SessionId.ToString();
			return parameters.ToString();
		}
	}
}