using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Api.CRM.Enums;
using Vostok.Hotline.Gateway.Client.Counterpart.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Counterpart.Requests.Queries
{
	public class ClientCountQuery : IRequest<ClientCountViewModel>
	{
		[BindRequired]
		public string SearchFilter { get; set; }
		
		[BindRequired]
		public SearchTypeEnum SearchType { get; set; }
	}
}
