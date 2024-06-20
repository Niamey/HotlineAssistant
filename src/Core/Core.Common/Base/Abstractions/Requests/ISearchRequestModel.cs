namespace Vostok.Hotline.Core.Common.Base.Abstractions.Requests
{
	public interface ISearchRequestModel : IRequestModel
	{
		string GetUrlQuery();
	}	
}
