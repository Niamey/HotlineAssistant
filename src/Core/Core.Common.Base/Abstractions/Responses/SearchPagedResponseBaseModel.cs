namespace Vostok.Hotline.Core.Common.Base.Abstractions.Responses
{
	public abstract class SearchPagedResponseBaseModel : ResponseBaseModel
	{
		public bool IsNextPageAvailable { get; set; }
	}	
}