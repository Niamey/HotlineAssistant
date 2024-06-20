using System.Web;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Api.Retail.SearchRequests.Agreements
{
	public class AgreementMoneyBoxCollectionSearchRequest : SearchRequestBaseModel
	{
		public long? AgreementId { get; set; }

		public override string GetUrlQuery()
		{
			var parameters = HttpUtility.ParseQueryString(string.Empty);

			parameters[nameof(AgreementId)] = AgreementId.ToString();

			return parameters.ToString();
		}
	}
}
