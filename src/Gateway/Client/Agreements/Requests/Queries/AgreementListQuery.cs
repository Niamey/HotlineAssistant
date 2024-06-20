using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Agreements.Requests.Queries
{
	public class AgreementListQuery : IRequest<SearchRowsResponseRowModel<AgreementViewModel>>
	{
		[BindRequired]
		public long? SolarId { get; set; }
	}
}
