using System.ComponentModel.DataAnnotations;
using MediatR;
using Vostok.Hotline.Storefront.ViewModels.SearchLinks;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Storefront.Requests.Queries.SearchLinks
{
	public class SearchLinkListQuery : IRequest<SearchRowsResponseRowModel<SearchLinkViewModel>>
	{
		[Required]
		public int? Count { get; set; }
	}
}
