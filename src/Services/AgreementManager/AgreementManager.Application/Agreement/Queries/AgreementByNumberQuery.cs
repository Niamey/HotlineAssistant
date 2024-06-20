using MediatR;
using System.Runtime.Serialization;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Dtos;
using Vostok.HotLineAssistant.Common.Response.Common;

namespace Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Queries
{
	[DataContract]
	public class AgreementByNumberQuery : IRequest<ModelResponse<AgreementDto>>
	{
		[DataMember]
		public string Number { get; set; }
	}
}