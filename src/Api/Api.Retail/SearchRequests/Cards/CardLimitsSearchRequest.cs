using System.Web;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Api.Retail.SearchRequests.Cards
{
	internal class CardLimitsSearchRequest : SearchRequestBaseModel
	{
		internal CardLimitsSearchRequest(string limiterCode)
		{
			LimiterCode = limiterCode;
		}

		public string LimiterCode { get; set; }

		public override string GetUrlQuery()
		{
			var parameters = HttpUtility.ParseQueryString(string.Empty);

			if (!string.IsNullOrEmpty(LimiterCode))
				parameters[nameof(LimiterCode)] = LimiterCode;

			return parameters.ToString();
		}
	}
}
