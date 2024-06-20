using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Api.Retail.SearchRequests.Agreements
{
	internal class AgreementCreditParamsSearchRequest : SearchRequestBaseModel
	{
		internal AgreementCreditParamsSearchRequest(long agreementId)
		{
			AgreementId = agreementId;
		}
		public long AgreementId { get; set; }

		public override string GetUrlQuery()
		{
			return AgreementId.ToString();
		}
	}
}
