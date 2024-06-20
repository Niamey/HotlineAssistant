using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Api.CRM.Enums;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Gateway.Client.Counterpart.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Counterpart.Requests.Queries
{
	public class ClientSearchQuery : IRequest<SearchPagedResponseRowModel<ClientViewModel>>
	{
		[BindRequired]
		public string SearchFilter { get; set; }
		
		[BindRequired]
		public SearchTypeEnum SearchType { get; set; }

		public int? PageIndex { get; set; }

		public int PageSize { get; set; }

		public string SortColumn { get; set; }

		public SortDirectionEnum? SortDirection { get; set; }
	}
}
