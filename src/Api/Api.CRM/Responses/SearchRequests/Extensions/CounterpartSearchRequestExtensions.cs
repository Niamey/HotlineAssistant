namespace Vostok.Hotline.Api.CRM.Responses.SearchRequests.Extensions
{
	public static class CounterpartSearchRequestExtensions
	{
		public static CounterpartRequest ToSearchRequest(this CounterpartCountRequest request)
		{
			return new CounterpartRequest
			{
				Code = request.Code,
				Criteria = request.Criteria,
				Type = request.Type
			};
		}
	}
}
