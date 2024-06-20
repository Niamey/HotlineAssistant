namespace Vostok.Hotline.Core.Common.Base.Abstractions.Requests
{
	public abstract class SearchRequestBaseModel : ISearchRequestModel
	{
		public abstract string GetUrlQuery();
	}
}
