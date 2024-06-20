using System.Web;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Api.References.SearchRequests
{
	internal class DivisionSearchRequest : SearchRequestBaseModel
	{
		public DivisionSearchRequest(int divisionId)
		{
			DivisionId = divisionId;
		}

		public int DivisionId { get; set; }

		public override string GetUrlQuery()
		{
			var parameters = HttpUtility.ParseQueryString(string.Empty);
			parameters[nameof(DivisionId)] = DivisionId.ToString();
			return parameters.ToString();
		}
	}
}