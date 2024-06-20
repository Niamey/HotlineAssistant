using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Gateway.Client.Travels.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Travels.Requests.Queries
{
	public class TravelPagedQuery : IRequest<SearchPagedResponseRowModel<TravelViewModel>>
	{
		[BindRequired]
		public long? SolarId { get; set; }

		public int? PageIndex { get; set; }

		public int PageSize { get; set; }

		public string SortColumn { get; set; }

		public SortDirectionEnum? SortDirection { get; set; }
	}
}
