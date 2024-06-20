using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Agreements.Requests.Queries
{
	public class AgreementHistoryStatusQuery : IRequest<AgreementHistoryStatusViewModel>
	{
		[BindRequired]
		public long? AgreementId { get; set; }
	}
}
