using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;

namespace Vostok.Hotline.Gateway.Client.Agreements.Requests.Commands
{
	public class AgreementTariffSendViberCommand : IRequest<StatusCommandViewModel>
	{
		[BindRequired]
		public long? ClientId { get; set; }
		[BindRequired]
		public long? AgreementId { get; set; }
	}
}
