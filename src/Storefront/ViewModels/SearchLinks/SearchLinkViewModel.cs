using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Storefront.ViewModels.SearchLinks
{
	public class SearchLinkViewModel : ResponseBaseModel
	{
		public int Id { get; set; }
		public string ImageUrl { get; set; }
		public bool IsCustomIcon { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Url { get; set; }
	}
}