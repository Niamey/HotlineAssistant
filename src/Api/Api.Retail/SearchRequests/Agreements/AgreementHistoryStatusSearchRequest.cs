using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Api.Retail.SearchRequests.Agreements
{
	internal class AgreementHistoryStatusSearchRequest : SearchRequestBaseModel
	{
		internal AgreementHistoryStatusSearchRequest(long agreementId)
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
