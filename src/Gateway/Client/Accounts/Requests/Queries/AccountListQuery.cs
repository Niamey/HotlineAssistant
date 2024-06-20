using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Accounts.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Accounts.Requests.Queries
{
	public class AccountListQuery : IRequest<SearchRowsResponseRowModel<AccountViewModel>>
	{
		[BindRequired]
		public long? SolarId { get; set; }
	}
}
