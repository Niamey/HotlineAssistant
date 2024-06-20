using MediatR;
using System.Runtime.Serialization;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Dtos;
using Vostok.HotLineAssistant.Common.Response.Common;

namespace Vostok.HotLineAssistant.AgreementManager.Application.AgreementBalances.Queries
{
	[DataContract]
	public class AgreementBalanceByNumberQuery : IRequest<ModelResponse<BalanceDto>>
	{
		[DataMember]
		public string Number { get; set; }
	}
}